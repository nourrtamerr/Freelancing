
using Freelancing.Helpers;
using Freelancing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Freelancing
{
    public class Program
    {
        public static async void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders(); ;

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


            app.MapControllers();

            app.Run();
        }
    }
}
