using AutoMapper;
using Freelancing.DTOs.AuthDTOs;
using Freelancing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Freelancing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController(IWebHostEnvironment _env, IMapper _mapper, RoleManager<IdentityRole> _roleManager, UserManager<AppUser> _userManager, IConfiguration _configuration, SignInManager<AppUser> signInManager) : ControllerBase
	{
		[HttpGet("test")]
		[Authorize]
		public async Task<IActionResult> test()
		{
			return Ok(new {str= "hh" });
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register([FromForm] RegisterDTO dto)
		{

			#region ExistingAccount
			if ((await _userManager.FindByEmailAsync(dto.Email)) is not null)
			{
				return BadRequest("Email already exists");
			}
			if ((await _userManager.FindByNameAsync(dto.UserName)) is not null)
			{
				return BadRequest("User Name already exists");
			}

			#endregion
			string savedProfilePicturePath = null;
			if (dto.ProfilePicture != null)
			{
				savedProfilePicturePath = dto.ProfilePicture.Save(_env);  // Save the image
				dto.ProfilePicture = null;  // Set ProfilePicture to null after saving
			}

			IdentityResult result;
			AppUser newuser;
			if(dto.Role==userRole.Freelancer)
			{
				Freelancer freelancer = _mapper.Map<Freelancer>(dto);
				freelancer.ProfilePicture = savedProfilePicturePath;
				result =await _userManager.CreateAsync(freelancer, dto.Password);
				await _userManager.AddToRoleAsync(freelancer, RoleSeeder.freelancer);
				newuser = freelancer;
			}
			else
			{
				Client client = _mapper.Map<Client>(dto);
				client.ProfilePicture = savedProfilePicturePath;
				result = await _userManager.CreateAsync(client, dto.Password);
				await _userManager.AddToRoleAsync(client, RoleSeeder.client);
				newuser = client;
			}
			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
				var validationErrors = ModelState
		.Where(ms => ms.Value.Errors.Count > 0)  // Only include fields with errors
		.ToDictionary(
			kv => kv.Key, // Field name
			kv => kv.Value.Errors.Select(e => new { errorMessage = e.ErrorMessage }).ToList() // List of error messages
		);

				var errorResponse = new
				{
					title = "One or more validation errors occurred.",
					status = 400,
					errors = validationErrors,
				};
				return BadRequest(errorResponse);
			}
			newuser.AccountCreationDate = DateOnly.FromDateTime(DateTime.Now);
		
				return Ok(new { token=await GenerateToken(newuser) });
			
			
		}	


		private async Task<string> GenerateToken(AppUser user)
		{
			
			var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var signcred = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
			//var _userfromdb = _context.AppUsers.iNCL.FirstOrDefault(u => u.Id == user.Id);

			var userClaims = await _userManager.GetClaimsAsync(user);
			var roles = await _userManager.GetRolesAsync(user);
			var roleClaims = new List<Claim>();
			foreach (var role in roles)
			{
				roleClaims.Add(new Claim(ClaimTypes.Role, role));
			}
			var claims = new[]
			 {
				new  Claim(ClaimTypes.Name,user.UserName),
				new  Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
				new  Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
				new  Claim(JwtRegisteredClaimNames.Email,user.Email),
			}.Union(userClaims).Union(roleClaims);


			var expiry = DateTime.UtcNow.AddMinutes(25);

			var token =
				new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"]
				, claims, expires: expiry,
				signingCredentials: signcred,
				notBefore: DateTime.UtcNow);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
