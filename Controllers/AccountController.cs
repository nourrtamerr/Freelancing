using AutoMapper;
using Freelancing.DTOs.AuthDTOs;
using Freelancing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using PharmaTechBE.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Freelancing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController(IWebHostEnvironment _env, SignInManager<AppUser> _signinManager, IEmailSettings _emailSettings, IMapper _mapper, RoleManager<IdentityRole> _roleManager, UserManager<AppUser> _userManager, IConfiguration _configuration, SignInManager<AppUser> signInManager) : ControllerBase
	{
		[HttpGet("test")]
		[Authorize]
		public async Task<IActionResult> test()
		{
			return Ok(new { str = "hh" });
		}


		[HttpGet("IsAuthenticated")]
		public IActionResult IsAuthenticated()
		{
			return Ok(new { User.Identity.IsAuthenticated, UserName = User.FindFirstValue(ClaimTypes.Name) });
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDTO LoginUser)
		{


			var user = await _userManager.FindByEmailAsync(LoginUser.Usernameoremail) ??
					   await _userManager.FindByNameAsync(LoginUser.Usernameoremail);

			if (user == null || !await _userManager.CheckPasswordAsync(user, LoginUser.loginPassword))
			{
				return Unauthorized("Invalid email or password.");
			}
			if (!await _userManager.IsEmailConfirmedAsync(user))
			{
				return BadRequest("You must confirm your email before logging in");
			}

			user.RefreshToken = JWTHelpers.CreateRefreshToken();
			user.RefreshTokenExpiryDate = DateTime.Now.AddDays(7);
			var token = await GenerateToken(user);
			return Ok(new { token, user.RefreshToken });
		}

		[Route("Refresh-Token")]
		[HttpPost]
		public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDTO dto)
		{
			var user = (await _userManager.FindByNameAsync(dto.UserName));
			if (user is null || dto.RefreshToken != user.RefreshToken || user.RefreshTokenExpiryDate < DateTime.UtcNow)
			{
				return BadRequest("Ineligible for refresh token");
			}
			user.RefreshToken = JWTHelpers.CreateRefreshToken();
			user.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(7);
			await _userManager.UpdateAsync(user);
			var token = await GenerateToken(user);
			return Ok(new { token, user.RefreshToken });
		}
		[HttpPost("Register")]
		public async Task<IActionResult> Register([FromForm] RegisterDTO dto)
		{

			#region ExistingAccount

			if ((await _userManager.FindByEmailAsync(dto.Email)) is AppUser myuser)
			{
				if (myuser is not null)
				{
					if (myuser.EmailConfirmed == true)
					{
						return BadRequest("Email already exists");
					}
					else
					{
						return BadRequest("Email already exists but not confirmed");
					}
				}
			}
			if ((await _userManager.FindByNameAsync(dto.UserName)) is not null)
			{
				return BadRequest("User Name already exists");
			}

			#endregion


			IdentityResult result;
			AppUser newuser;
			if (dto.Role == userRole.Freelancer)
			{
				Freelancer freelancer = _mapper.Map<Freelancer>(dto);
				freelancer.RefreshToken = JWTHelpers.CreateRefreshToken();
				freelancer.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(7);
				result = await _userManager.CreateAsync(freelancer, dto.Password);
				//await _userManager.AddToRoleAsync(freelancer, RoleSeeder.freelancer);
				newuser = freelancer;
			}
			else
			{
				Client client = _mapper.Map<Client>(dto);
				client.RefreshToken = JWTHelpers.CreateRefreshToken();
				client.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(7);
				result = await _userManager.CreateAsync(client, dto.Password);
				//await _userManager.AddToRoleAsync(client, RoleSeeder.client);
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
			else
			{
				await _userManager.AddToRoleAsync(newuser, dto.Role==userRole.Client? RoleSeeder.client:RoleSeeder.freelancer);
			}
			if (dto.ProfilePicture is not null)
			{
				newuser.ProfilePicture = dto.ProfilePicture.Save(_env);
			}
			newuser.AccountCreationDate = DateOnly.FromDateTime(DateTime.Now);

			await _userManager.UpdateAsync(newuser);


			var token = await _userManager.GenerateEmailConfirmationTokenAsync(newuser);
			var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = newuser.Id, token = token }, Request.Scheme);

			// Send confirmation email
			var email = new Email2
			{
				To = newuser.Email,
				Subject = "Confirm Email",
				Body = $"Please confirm your email by clicking on this link: {confirmationLink}"
			};
			_emailSettings.SendEmail(email);
			return Ok(new
			{
				message = "Account Created Successfully" +
					". Please check your email to confirm your account."
			});


		}
		[HttpGet("ResendEmailConfirmation")]
		public async Task<IActionResult> ResendEmailConfirmation(string emailToBeCONFIRMED)
		{
			var newuser = await _userManager.FindByEmailAsync(emailToBeCONFIRMED);
			if (newuser is null)
			{
				return BadRequest("Email is not registered");
			}
			if (newuser.EmailConfirmed)
			{
				return BadRequest("Email is already confirmed");
			}
			var token = await _userManager.GenerateEmailConfirmationTokenAsync(newuser);
			var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = newuser.Id, token = token }, Request.Scheme);
			var email = new Email2
			{
				To = newuser.Email,
				Subject = "Confirm Email",
				Body = $"Please confirm your email by clicking on this link: {confirmationLink}"
			};
			_emailSettings.SendEmail(email);
			return Ok(new
			{
				message = "New confirmation email was sent"
			});
		}
		[HttpGet("ConfirmEmail")]
		public async Task<IActionResult> ConfirmEmail(string userId, string token)
		{
			if (userId == null || token == null)
			{
				return BadRequest("Invalid email confirmation request.");
			}

			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return NotFound("User not found.");
			}

			var result = await _userManager.ConfirmEmailAsync(user, token);
			if (result.Succeeded)
			{
				return Ok(new { message = "EmailConfirmed" });


			}
			else
			{
				return BadRequest(new { message = "Email confirmation failed." });
			}
		}




		[HttpGet("External-login")]
		[AllowAnonymous]
		public IActionResult ExternalLogin(string provider, userRole role, string returnUrl = null, string errorurl = null)
		{

			var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl, errorurl, role });
			var properties = _signinManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
			properties.Items["LoginProvider"] = provider;
			return Challenge(properties, provider);
		}

		[HttpGet("external-login-callback")]
		[AllowAnonymous]
		public async Task<IActionResult> ExternalLoginCallback(userRole role, string returnUrl = null, string remoteError = null, string errorurl = null)
		{

			if (remoteError != null)
			{
				return Redirect($"{errorurl}?error={Uri.EscapeDataString($"External authentication error: {remoteError}")}");



			}

			var info = await _signinManager.GetExternalLoginInfoAsync();
			if (info == null)
			{
				return Redirect($"{errorurl}?error={Uri.EscapeDataString("Error loading external login information.")}");

			}

			var email = info.Principal.FindFirstValue(ClaimTypes.Email);
			var name = info.Principal.FindFirstValue(ClaimTypes.Name);

			if (string.IsNullOrEmpty(email))
			{
				return Redirect($"{errorurl}?error={Uri.EscapeDataString("Email not provided by the external provider.")}");
			}
			var userbyname = await _userManager.FindByNameAsync(name);
			var userbyemail = await _userManager.FindByEmailAsync(email);
			if ((userbyname != null && userbyemail != null) && (userbyemail.UserName != userbyname.UserName || userbyname.Email != userbyemail.Email))
			{
				return Redirect($"{errorurl}?error={Uri.EscapeDataString("Email is already associated with another account.")}");
			}
			var user = userbyemail;
			if (user == null)
			{
				if (role == userRole.Client)
				{
					user = new Client
					{
						UserName = Regex.Replace(name, "[^a-zA-Z0-9]", ""),
						Email = email,
						firstname = name,
						lastname = "",
						City = "",
						Country = "",
						EmailConfirmed = true
					};
				}
				else
				{
					user = new Freelancer
					{
						UserName = Regex.Replace(name, "[^a-zA-Z0-9]", ""),
						Email = email,
						firstname = name,
						lastname = "",
						City = "",
						Country = "",
						EmailConfirmed = true
					};
				}

				var result = await _userManager.CreateAsync(user);
				if (!result.Succeeded)
				{
					//return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });
					var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
					return Redirect($"{errorurl}?error={Uri.EscapeDataString(errorMessages)}");

				}
				user.EmailConfirmed = true;
				var result2 = await _userManager.UpdateAsync(user);
				if (!result2.Succeeded)
				{
					var errorMessages = string.Join(", ", result2.Errors.Select(e => e.Description));
					return Redirect($"{errorurl}?error={Uri.EscapeDataString(errorMessages)}");

				}
				await _userManager.AddLoginAsync(user, info);
			}
			else
			{
				await _userManager.AddLoginAsync(user, info);
			}
			var token = await GenerateToken(user);
			return Redirect($"{returnUrl}?token={token}");
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


			var expiry = DateTime.UtcNow.AddMinutes(2);

			var token =
				new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"]
				, claims, expires: expiry,
				signingCredentials: signcred,
				notBefore: DateTime.UtcNow);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
