using System.ComponentModel.DataAnnotations.Schema;

namespace Freelancing.DTOs.AuthDTOs
{
	public class ViewFreelancersDTO
	{
		public DateOnly AccountCreationDate { set; get; }
		public string firstname { get; set; }
		public string lastname { get; set; }
		public string UserName { set; get; }
		public string Country { get; set; }
		public string? ProfilePicture { get; set; }
		public string? NationalId { get; set; }
		public bool IsVerified { get; set; } = false;
		public bool isDeleted { get; set; } = false;

		[InverseProperty("Reviewee")]
		public virtual List<Review> Reviewed { get; set; }
		public List<Certificate> Certificates { get; set; }
		public List<UserSkill> UserSkills { get; set; }
		public bool isAvailable { get; set; }
		public decimal Balance { get; set; } = 0;
		public Education Education { get; set; }
		public List<Experience> Experiences { get; set; }
		public List<FreelancerLanguage> Languages { get; set; }
		public List<PortofolioProject> Portofolio { get; set; }
	}
}
