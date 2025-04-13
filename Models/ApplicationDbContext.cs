using Freelancing.DTOs.AuthDTOs;
using Freelancing.Migrations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations.Schema;

namespace Freelancing.Models
{
	public class ApplicationDbContext:IdentityDbContext<AppUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			this.ChangeTracker.LazyLoadingEnabled = true;
		}
		public DbSet<Admin> Admins { get; set; }
		public DbSet<Client> clients { get; set; }
		public DbSet<Freelancer> freelancers { get; set; }
		public DbSet<Ban> Bans { get; set; }
		//public DbSet<Project> Projects { get; set; }
		public DbSet<BiddingProject> biddingProjects { get; set; }
		[NotMapped]
		//public DbSet<Project> project { get; set; }
		public DbSet<FixedPriceProject> fixedPriceProjects { get; set; }
		public DbSet<Category> categories { get; set; }
		public DbSet<Certificate> certificates { get; set; }
		public DbSet<Chat> Chats { set; get; }
		public DbSet<Education> Educations { set; get; }
		public DbSet<Experience> Experiences { set; get; }
		public DbSet<FreelancerLanguage> freelancerLanguages { set; get; }
		public DbSet<Milestone> Milestones { get; set; }
		public DbSet<SuggestedMilestone> suggestedMilestones { get; set; }
		public DbSet<Notification> Notifications { set; get; }

		//public DbSet<Payment> Payments { get; set; }
		public DbSet<MilestonePayment> MilestonePayments { get; set; }
		public DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }
		public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
		public DbSet<PortofolioProject> PortofolioProjects { get; set; }
		public DbSet<PortofolioProjectImage> PortofolioProjectImages { get; set; }
		public DbSet<Proposal> Proposals { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Skill> Skills { get; set; }
		public DbSet<Subcategory> Subcategories { get; set; }
		public DbSet<UserSkill> UserSkills { get; set; }
		public DbSet<ProjectSkill> ProjectSkills { get; set; }
        public DbSet<UserSubscriptionPlanPayment> UserSubscriptionPlanPayments { get; set; }

        public DbSet<UserConnection> UserConnections { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			{
				foreach (var foreignKey in entityType.GetForeignKeys())
				{
					// Set the delete behavior to NoAction for all foreign keys
					foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
				}
			}
			
			modelBuilder.Entity<BiddingProject>().ToTable("biddingProjects");
			modelBuilder.Entity<FixedPriceProject>().ToTable("fixedPriceProjects");
			modelBuilder.Entity<MilestonePayment>().ToTable("MilestonePayments");
			modelBuilder.Entity<SubscriptionPayment>().ToTable("SubscriptionPayments");
			modelBuilder.Entity<Admin>().ToTable("Admins");
			modelBuilder.Entity<Client>().ToTable("clients");
			modelBuilder.Entity<Freelancer>().ToTable("freelancers");

			

			var hasher = new PasswordHasher<Admin>();
			var admin = new Admin
			{
				Id = "1", // Use string ID for IdentityUser
				UserName = "admin",
				NormalizedUserName = "ADMIN",
				Email = "admin@example.com",
				NormalizedEmail = "ADMIN@EXAMPLE.COM",
				EmailConfirmed = true,
				SecurityStamp = Guid.NewGuid().ToString("D"),
				PasswordHash = hasher.HashPassword(null, "Admin@123"),
				City = "Admin City",
				Country = "Admin Country",
				firstname = "Admin",
				lastname = "User",
				RefreshToken = "",
				RefreshTokenExpiryDate = DateTime.Now
			};

			modelBuilder.Entity<Admin>().HasData(admin);


			modelBuilder.Entity<MilestonePayment>()
				.HasOne(mp => mp.Milestone)
				.WithOne(m => m.MilestonePayment)
				.HasForeignKey<MilestonePayment>(mp => mp.MilestoneId);


			modelBuilder.Entity<UserConnection>(entity =>
            {
                entity.HasOne(uc => uc.User)
                    .WithMany()
                    .HasForeignKey(uc => uc.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
	}
}
