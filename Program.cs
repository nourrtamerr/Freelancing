
using Freelancing.IRepositoryService;
using Freelancing.Models;
using Freelancing.Helpers;
using Freelancing.RepositoryService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
//using AutoMapper

using AutoMapper;
using Freelancing.Middlewares;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Freelancing.Middlewares;
//using AutoMapper.Extensions.Microsoft.DependencyInjection;


namespace Freelancing
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            //builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

			// Configure Identity
			builder.Services.AddIdentity<AppUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();
			//JWT
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
			});
			// Add services to the container.
			builder.Services.AddAuthorization();


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

            #endregion

            //builder.Services.AddAutoMapper(typeof(MappingProfiles));

            //        AutoMapper.Extensions.Microsoft.DependencyInjection.ServiceCollectionExtensions.AddAutoMapper(
            //builder.Services, typeof(MappingProfile));

            builder.Services.AddAutoMapper(typeof(ReviewProfile), typeof(BanProfile));

            //AutoMapperServiceCollectionExtensions.AddAutoMapper(builder.Services, typeof(MappingProfiles));


            //builder.Services.AddAuthentication(op => {
            //	op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //	op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //	op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;


            //})
            //	.AddJwtBearer(op =>
            //	{
            //		op.TokenValidationParameters = new()
            //		{
            //			ValidateIssuer = true,
            //			ValidateAudience = true,
            //			ValidateLifetime = true,
            //			ValidateIssuerSigningKey = true,
            //			ValidIssuer = builder.Configuration["Jwt:Issuer"],
            //			ValidAudience = builder.Configuration["Jwt:Audience"],
            //			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            //			ClockSkew = TimeSpan.Zero
            //		};
            //	})
            //	;
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
			var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "MyPrivateUploads");
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


			using (var scope = app.Services.CreateScope())
			{
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

				await RoleSeeder.SeedRolesAsync(roleManager);
			}
			#region Services

			#endregion
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseMiddleware<BanCheckMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}
