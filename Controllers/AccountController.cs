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
	public class AccountController(IHttpContextAccessor _httpContextAccessor,IFreelancerService _freelancersmanager,IClientService _clientsmanager, INotificationRepositoryService _notifications,IConfiguration configuration,IWebHostEnvironment _env, SignInManager<AppUser> _signinManager, IEmailSettings _emailSettings, IMapper _mapper, RoleManager<IdentityRole> _roleManager, UserManager<AppUser> _userManager, IConfiguration _configuration, SignInManager<AppUser> signInManager) : ControllerBase
	{

		[HttpGet("test")]
		[Authorize]
		public async Task<IActionResult> test()
		{
			return Ok(new { str = "hh" });
		}

		[HttpGet("getIdByUserName/{username}")]
	
		public async Task<IActionResult> getIdByUserName(string username)
		{
			var user = await _userManager.FindByNameAsync(username);
			if (user is null)
			{
				return BadRequest("not found");
			}
			return Ok(new { id=user.Id});
		}
		[HttpGet("FreeAgents")]
		public async Task<IActionResult> getAllFreelancers()
		{
			return Ok(await _freelancersmanager.GetAllAsync());
		}

		[HttpGet("ToggleAvailability")]
		[Authorize(Roles ="Freelancer")]
		public async Task<IActionResult> ToggleAvailability()
		{
			var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
			if(user is Freelancer x)
			{
				x.isAvailable = !x.isAvailable;
				await _userManager.UpdateAsync(x);
			}
			return Ok();
		}
		[HttpGet("FilteredFreeAgents")]
		public async Task<IActionResult> getAllFreelancersFiltered([FromQuery]FreelancerFilterationDTO dto)
		{
			FilterationResponseModel response = new FilterationResponseModel();
			var result = await _freelancersmanager.GetAllFiltered(dto);
			response.NextPageLink = GetNextPageLink(dto, result.Count());
			response.PreviousPageLink = GetPreviousPageLink(dto);
			response.numofpages = dto.numofpages;
			return Ok(new { result, response });
		}
		[HttpGet("FilteredClients")]
		public async Task<IActionResult> getAllClientsFiltered([FromQuery] ClientFilterationDTO dto)
		{
			FilterationResponseModel response = new FilterationResponseModel();
			var result = await _clientsmanager.GetAllFiltered(dto);
			response.NextPageLink = GetNextPageLink(dto, result.Count());
			response.PreviousPageLink = GetPreviousPageLink(dto);
			response.numofpages = dto.numofpages;
			return Ok(new {result, response });
		}

		#region Private Functions
		private string GetNextPageLink(FilterationDTO dto, int totalItems)
		{
			if ( dto.pagesize == totalItems)
			{
				var nextPageNum = dto.pageNum + 1;
				return GeneratePageLink(nextPageNum, dto);
			}
			return string.Empty; // No next page
		}

		private string GetPreviousPageLink(FilterationDTO dto)
		{
			if (dto.pageNum > 1)
			{
				var prevPageNum = dto.pageNum - 1;
				return GeneratePageLink(prevPageNum, dto);
			}
			return string.Empty; // No previous page
		}

		private string GeneratePageLink(int pageNum, FilterationDTO dto)
		{
			var queryString = new List<string>
	{
		$"pageNum={pageNum}",
		$"pagesize={dto.pagesize}"
	};

	

			if (!string.IsNullOrEmpty(dto.name))
				queryString.Add($"name={dto.name}");

			if (dto.AccountCreationDate.HasValue)
				queryString.Add($"AccountCreationDate={dto.AccountCreationDate.Value}");

			

			if (dto.IsVerified.HasValue)
				queryString.Add($"IsVerified={dto.IsVerified.Value}");

			if (dto.Paymentverified.HasValue)
				queryString.Add($"Paymentverified={dto.Paymentverified.Value}");

			if (dto.CountryIDs is { Count: > 0 })
			{
				foreach (var countryId in dto.CountryIDs)
				{
					queryString.Add($"CountryIDs={countryId}");
				}
			}

			// Check if it's a FreelancerFilterationDTO and add freelancer-specific filters
			if (dto is FreelancerFilterationDTO freelancerDto)
			{
				if (freelancerDto.isAvailable.HasValue)
					queryString.Add($"isAvailable={freelancerDto.isAvailable.Value}");

				if (freelancerDto.Languages != null && freelancerDto.Languages.Count > 0)
				{
					foreach (var language in freelancerDto.Languages)
					{
						queryString.Add($"Languages={language}");
					}
				}

				if (freelancerDto.ranks != null && freelancerDto.ranks.Count > 0)
				{
					foreach (var rank in freelancerDto.ranks)
					{
						queryString.Add($"ranks={rank}");
					}
				}
			}
			else if (dto is ClientFilterationDTO clientDto)
			{
				if (clientDto.ranks != null && clientDto.ranks.Count > 0)
				{
					foreach (var rank in clientDto.ranks)
					{
						queryString.Add($"ranks={rank}");
					}
				}
			}

			var queryStringResult = string.Join("&", queryString);
			return $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.Path}?{queryStringResult}";
		}
		#endregion
		[HttpGet("FreeAgent/{username}")]
		public async Task<IActionResult> getFreelancerById(string username)
		{
			var user = (await _userManager.FindByNameAsync(username));

			if (user==null)
			{
				return BadRequest("not found");
			}
			return Ok(await _freelancersmanager.GetByIDAsync(user.Id));
		}
		[HttpGet("usernameById")]
		public async Task<IActionResult> getUserNameById(string id)
		{
			var user = (await _userManager.FindByIdAsync(id));
			if (user == null)
			{
				return BadRequest("not found");
			}
			return Ok(new { userName = user.UserName });
		}
		[HttpGet("Clients/{username}")]
		public async Task<IActionResult> getClientsById(string username)
		{
			var user = (await _userManager.FindByNameAsync(username));
			if (user == null)
			{
				return BadRequest("not found");
			}
			return Ok(await _clientsmanager.GetClientById(user.Id));
		}
		[HttpGet("Clients")]
		public async Task<IActionResult> getAllClients()
		{
			return Ok(await _clientsmanager.GetAllClients());
		}

		[HttpGet("GetAllUsers")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> AllUsers()
		{
			var users = await _userManager.Users.ToListAsync();
			if (users.Count == 0)
			{
				return NotFound("No users found");
			}
			var userDtos = new List<UsersViewDTO>();
			foreach (var user in users)
			{
				userDtos.Add(_mapper.Map<UsersViewDTO>(user));
			}
			return Ok(userDtos);
		}


		[HttpGet("MyProfile")]
		[Authorize]
		public async Task<IActionResult> MyProfile()
		{
			var user = await _userManager.Users.Include(u=>u.City).ThenInclude(c=>c.Country).FirstOrDefaultAsync(U=>U.Id==User.FindFirstValue(ClaimTypes.NameIdentifier));
			if (user is null)
			{
				return NotFound("No users found");
			}
			
			return Ok(_mapper.Map<UsersViewDTO>(user));
		}

		[HttpGet("getUserIdentityPicture")]
		[Authorize(Roles ="Admin")]
		public async Task<IActionResult> getUserIdentityPicture(string userid)
		{
			var user =await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
			return Ok(new { user.NationalId });
		}
		[HttpGet("getUsersRequestingVerifications")]
		[Authorize(Roles ="Admin")]
		public async Task<IActionResult> getUsersRequestingVerifications()
		{
			var users = _userManager.Users.Include(u=>u.City).ThenInclude(c=>c.Country).Where(u => u.NationalId != null&&!u.IsVerified).ToList();
			if (users is null)
			{
				return NotFound("No users are requesting verification");
			}
			var userDtos = new List<UsersRequestingVerificationViewDTO>();
			foreach (var user in users)
			{
				userDtos.Add(_mapper.Map<UsersRequestingVerificationViewDTO>(user));
			}
			return Ok(userDtos);
		}
		
		[HttpPost("RequestIdentityVerification")]
		[Authorize(Roles ="Freelancer,Client")]
		public async Task<IActionResult> RequestIdentityVerification([FromForm] RequestVerificationDTO dto)
		{
			var currentuser =await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
			if(currentuser.GetType()==typeof(Admin))
			{
				return BadRequest("Admins cant be verified");
			}
			if (currentuser.IsVerified)
			{
				return BadRequest("User is already verified");
			}
			if(currentuser.NationalId is not null)
			{
				return BadRequest("Please wait till you get response for the first verification request");
			}
			currentuser.NationalId = dto.IdPicture.Save();
			await _userManager.UpdateAsync(currentuser);
			foreach (var admin in _userManager.Users.OfType<Admin>().ToList())
			{
				await _notifications.CreateNotificationAsync(new Notification()
				{
					isRead = false,
					Message = $"User{currentuser.UserName} added a national id picture and its pending verification" +
					$"National id is : {dto.nationalId}, Full name is {dto.fullName}",
					UserId= admin.Id
				});
			}
			return Ok(new { Message = "Pending Verification" });
		}
		[HttpPost("ManageVerificationRequest")]
		[Authorize(Roles ="Admin")]
		public async Task<IActionResult> VerifyIdentity(VerificationDecision dto)
		{
			var user=await _userManager.FindByIdAsync(dto.userId);
			if(user is null)
			{
				return BadRequest("Wrong user");
			}
			if(user.IsVerified)
			{
				return BadRequest("user is already verified");
			}
			if(user.NationalId is null)
			{
				return BadRequest("user doesnt have an active verification request");
			}
			if(dto.isAccepted)
			{
				user.IsVerified = true;
				await _notifications.CreateNotificationAsync(new Notification()
				{
					isRead = false,
					Message = $"Admin {User.FindFirstValue(ClaimTypes.Name)} Accepted your request for verification because of" +
				$"{dto.Reason}, You are now verified",
					UserId = dto.userId
				});
			}
			else
			{
				user.NationalId = null;
				await _notifications.CreateNotificationAsync(new Notification()
				{
					isRead = false,
					Message = $"Admin {User.FindFirstValue(ClaimTypes.Name)} rejected your request for verification because of" +
				$"{dto.Reason}, please try again",
					UserId = dto.userId
				});
			}
			return Ok(new { Message = "Response sent" });
		}
		[HttpGet("IsAuthenticated")]
		public IActionResult IsAuthenticated()
		{
			return Ok(new { User.Identity.IsAuthenticated, UserName = User.FindFirstValue(ClaimTypes.Name) });
		}

		
		[HttpPut("EditProfile")]
		[Authorize]
		public async Task<IActionResult> editProfile([FromForm]EditProfileDTO dto)
		{
			var newuser = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));


			IdentityResult result;


			if (!(await _userManager.CheckPasswordAsync(newuser, dto.Password)))
			{
				return BadRequest("Invalid password");
			}
			var user2 = await _userManager.FindByNameAsync(dto.UserName);
			if (user2 is not null && user2.Id!=newuser.Id)
			{
				return BadRequest("user name is taken");
			}
			_mapper.Map(dto, newuser);
			if (dto.ProfilePicture is not null)
			{
				if (newuser.ProfilePicture != null)
				{
					SaveImage.Delete(newuser.ProfilePicture);
				}
				newuser.ProfilePicture = dto.ProfilePicture.Save();
			}
			if(dto.Description is not null)
			{
				newuser.Description=dto.Description;
			}
			result = await _userManager.UpdateAsync(newuser);
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
			return Ok(new
			{
				message = "Account Updated successfully"
			});
		}


		[HttpPost("MakeAdmin")]
		[Authorize(Roles ="Admin")]
		public async Task<IActionResult> MakeAdmin([FromBody]string userId)
		{
			var user=await _userManager.FindByIdAsync(userId);
			if(user is null)
			{
				return BadRequest("user not found");
			}
			if(await _userManager.IsInRoleAsync(user,"Admin"))
			{
				return BadRequest("Already an admin");
			}
			var result = await _userManager.AddToRoleAsync(user, "Admin");
			if(!result.Succeeded)
			{
				return BadRequest("error occured");
			}
			return Ok("Added to Admin");
		}
		[HttpPost("RemoveAdmin")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> RemoveAdmin([FromBody] string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user is null)
			{
				return BadRequest("user not found");
			}
			if (!(await _userManager.IsInRoleAsync(user, "Admin")))
			{
				return BadRequest("user is not an admin");
			}
			var result = await _userManager.RemoveFromRoleAsync(user, "Admin");
			if (!result.Succeeded)
			{
				return BadRequest("error occured");
			}
			return Ok("Removed from Admin");
		}
		[HttpPost("CreateAdminAccount")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateAdminAccount(CreateAdminDTO dto)
		{
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
			Admin admin = _mapper.Map<Admin>(dto);
			admin.RefreshToken = JWTHelpers.CreateRefreshToken();
			admin.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(7);
			var result = await _userManager.CreateAsync(admin, dto.Password);
			//await _userManager.AddToRoleAsync(freelancer, RoleSeeder.freelancer);
			var newuser = admin;


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
			
				await _userManager.AddToRoleAsync(admin, RoleSeeder.admin);
			
			if (dto.ProfilePicture is not null)
			{
				admin.ProfilePicture = dto.ProfilePicture.Save();
			}
			admin.AccountCreationDate = DateOnly.FromDateTime(DateTime.Now);
			admin.EmailConfirmed = true;
			await _userManager.UpdateAsync(admin);
			return Ok(new
			{
				Message="Admin Created"
			});
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
						return BadRequest(new { message = "Email Already exists." });
					}
					else
					{
						return BadRequest(new { Message = "Email already exists but not confirmed" });
					}
				}
			}
			if ((await _userManager.FindByNameAsync(dto.UserName)) is not null)
			{
				return BadRequest(new { Message = "User Name already exists" });
			}

			#endregion


			IdentityResult result;
			AppUser newuser;
			if (dto.Role == userRole.Freelancer)
			{
				Freelancer freelancer = _mapper.Map<Freelancer>(dto);
				freelancer.RefreshToken = JWTHelpers.CreateRefreshToken();
				freelancer.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(7);
				freelancer.subscriptionPlanId = 1;
				result = await _userManager.CreateAsync(freelancer, dto.Password);
				//await _userManager.AddToRoleAsync(freelancer, RoleSeeder.freelancer);
				newuser = freelancer;
			}
			else
			{
				Client client = _mapper.Map<Client>(dto);
				client.RefreshToken = JWTHelpers.CreateRefreshToken();
				client.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(7);
				client.subscriptionPlanId = 1;
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
				await _userManager.AddToRoleAsync(newuser, dto.Role == userRole.Client ? RoleSeeder.client : RoleSeeder.freelancer);
			}
			if (dto.ProfilePicture is not null)
			{
				newuser.ProfilePicture = dto.ProfilePicture.Save();
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
				return BadRequest(new{ Message= "Ineligible for refresh token" });
			}
			user.RefreshToken = JWTHelpers.CreateRefreshToken();
			user.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(7);
			await _userManager.UpdateAsync(user);
			var token = await GenerateToken(user);
			return Ok(new { token, user.RefreshToken });
		}
        [Route("ForgotPassword")]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO dto, string reseturl)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is not null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var encodedToken = WebUtility.UrlEncode(token);

                var resetPasswordLink = Url.Action("ResetPassword", "Account", new
                {
                    email = user.Email,
                    token = encodedToken,
                    reseturl = reseturl
                }, Request.Scheme);

                var email = new Email2()
                {
                    Subject = "Reset Password",
                    To = user.Email,
                    Body = resetPasswordLink
                };

                _emailSettings.SendEmail(email);
                return Ok(new { Message = "Forget password link was sent to your email" });
            }
            else
            {
                return BadRequest("Email is invalid");
            }
        }

        [Route("ResetPassword")]
        [HttpGet]
        public IActionResult ResetPasswordPage(string email, string token, string reseturl)
        {
            return Redirect($"{reseturl}?token={token}&email={email}");
        }

        [Route("ResetPassword")]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {


                return BadRequest(new { message = "Invalid user." });
            }

            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
            if (result.Succeeded)
            {
                return Ok(new { Message = "Password changed successfully" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            var validationErrors = ModelState
                .Where(ms => ms.Value.Errors.Count > 0)
                .ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value.Errors.Select(e => new { errorMessage = e.ErrorMessage }).ToList()
                );

            var errorResponse = new
            {
                title = "One or more validation errors occurred.",
                status = 400,
                errors = validationErrors,
            };

            return BadRequest(errorResponse);
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
				var url = configuration["AppSettings:AngularAppUrl"] + "/login?pleaseLogin";
				await _notifications.CreateNotificationAsync(new Notification()
				{
					isRead = false,
					Message = $"Welcome to Worktern, {user.UserName} , We hope you have a nice stay",
					UserId = user.Id
				});
				return Redirect(url);


			}
			else
			{
				return BadRequest(new { message = "Email confirmation failed." });
			}
		}
        [HttpGet("External-login")]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, userRole? role = null, string returnUrl = null, string errorurl = null)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl, errorurl, role });
            var properties = _signinManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            properties.Items["LoginProvider"] = provider;
            return Challenge(properties, provider);
        }

        [HttpGet("external-login-callback")]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(userRole? role = null, string returnUrl = null, string remoteError = null, string errorurl = null)
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

            if (info.LoginProvider != "Google" && info.LoginProvider != "Facebook")
            {
                return Redirect($"{errorurl}?error={Uri.EscapeDataString("Only Google and Facebook login are supported.")}");
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
            {
                return Redirect($"{errorurl}?error={Uri.EscapeDataString("Email not provided by the external provider.")}");
            }

            var user = await _userManager.FindByEmailAsync(email);

            // If user not found, redirect to registration page with role
            if (user == null)
            {
             
                // Redirect to Angular registration page
             return Redirect($"{_configuration["AppSettings:AngularAppUrl"]}/register?externalLoginFailed=true");
            }

            // Remove existing logins (clean state)
            var existingLogins = await _userManager.GetLoginsAsync(user);
            foreach (var login in existingLogins)
            {
                await _userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
            }

            // Add the new external login
            var addLoginResult = await _userManager.AddLoginAsync(user, info);
            if (!addLoginResult.Succeeded)
            {
                return Redirect($"{errorurl}?error={Uri.EscapeDataString("Failed to add external login.")}");
            }

           

            // Refresh claims
            await _userManager.RemoveClaimsAsync(user, await _userManager.GetClaimsAsync(user));
            await _signinManager.RefreshSignInAsync(user);

            // Generate token
            var token = await GenerateToken(user);

            // Set cookie
            Response.Cookies.Append("user_Token", token, new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            return Redirect(returnUrl ?? "/home2/profile");
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


			var expiry = DateTime.UtcNow.AddHours(2);

			var token =
				new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"]
				, claims, expires: expiry,
				signingCredentials: signcred,
				notBefore: DateTime.UtcNow);
            Console.WriteLine(" Token being generated for user: " + user.UserName);

            return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}

	public class TestDto
	{
		public string? Test { get; set; }
	}
}
