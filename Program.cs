using Freelancing.Middlewares;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text.Json.Serialization;
using Freelancing.Filters;
using Freelancing.SignalR;


namespace Freelancing
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddDbContext<ApplicationDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
			builder.Services.AddSignalR();
			#region AddCors
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("ChatPolicy", builder =>
				{
					builder.WithOrigins("http://127.0.0.1:5500")
						   .AllowAnyMethod()
						   .AllowAnyHeader()
						   .AllowCredentials();
				});
			});
			#endregion
			#region Configuring identity
			builder.Services.AddIdentity<AppUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();
			builder.Services.AddAuthentication((options) =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer((options) =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["Jwt:Issuer"],
					ValidAudience = builder.Configuration["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
					ClockSkew = TimeSpan.Zero


				};
			}).AddCookie(

				options => options.Events = new CookieAuthenticationEvents
				{
					OnRedirectToLogin = context =>
					{
						if (context.Request.Path.StartsWithSegments("/api"))
						{
							context.Response.StatusCode = 401;
							context.Response.ContentType = "application/json";
							return Task.CompletedTask;
						}

						context.Response.Redirect(context.RedirectUri);
						return Task.CompletedTask;
					}
				})
				.AddGoogle(OP =>
				{
					OP.ClientId = builder.Configuration["Authentication:Google:ClientId"];
					OP.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];

				}).AddFacebook(op =>
				{
					op.AppId = "1341703253547842";
					op.AppSecret = "9b2593ba5d2a3d86957ec93831791298";
				});



			builder.Services.Configure<IdentityOptions>(options =>
			{
				options.SignIn.RequireConfirmedEmail = true; 
			});
			builder.Services.AddAuthorization();
			#endregion
			#region services
			builder.Services.AddScoped<IReviewRepositoryService, ReviewRepositoryService>();
			builder.Services.AddScoped<IChatRepositoryService, ChatRepositoryService>();
			builder.Services.AddScoped<IBanRepositoryService, BanRepositoryService>();
			builder.Services.AddScoped<INotificationRepositoryService, NotificationRepositoryService>();
			builder.Services.AddScoped<IMilestoneService, MilestoneService>();
			builder.Services.AddScoped<ICategoryService, CategoryService>();
			builder.Services.AddScoped<ISubcategoryService, SubcategoryService>();
			builder.Services.AddScoped<IEducationService, EducationService>();
			builder.Services.AddScoped<IExperienceService, ExperienceService>();
			builder.Services.AddScoped<IEmailSettings, EmailSettings>();
			builder.Services.AddScoped<ICertificatesService, CertificateService>();
			builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
			builder.Services.AddScoped<INotificationRepositoryService, NotificationRepositoryService>();
			builder.Services.AddScoped<IFreelancerService, FreelancerService>();
			builder.Services.AddScoped<IClientService, ClientService>();
			builder.Services.AddScoped<IFreelancerLanguageService, FreelancerLanguageService>();
			builder.Services.AddScoped<IBiddingProjectService, BiddingProjectService>();
			#endregion

			#region Filters
			builder.Services.AddScoped<ReviewAuthorizationFilter>();
			#endregion


			builder.Services.AddAutoMapper(typeof(ReviewProfile), typeof(BanProfile), typeof(NotificationProfile), typeof(ChatProfile));

			builder.Services.AddControllers()
			.AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			});

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();
			var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), ImageSettings.ImagesPath);
			if (!Directory.Exists(uploadsPath))
			{
				Directory.CreateDirectory(uploadsPath); // Ensure the directory exists
			}
			app.UseStaticFiles();
			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(uploadsPath),
				RequestPath = "/files"
			}); //enabling wwwroot with the localhost/files url 


			using (var scope = app.Services.CreateScope())//seeding roles and a user
			{
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
				var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

				await RoleSeeder.SeedRolesAsync(roleManager, userManager);
			}


			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseHttpsRedirection();
			app.UseCors("ChatPolicy");
			app.UseAuthentication();
			app.UseAuthorization();


			app.UseMiddleware<BanCheckMiddleware>();
			app.MapControllers();
			app.MapHub<ChatHub>("/chathub");
			app.Run();
		}
	}
}
