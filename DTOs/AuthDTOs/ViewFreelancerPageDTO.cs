using System.ComponentModel.DataAnnotations.Schema;

namespace Freelancing.DTOs.AuthDTOs
{
	public class ViewFreelancerPageDTO
	{
		public DateOnly AccountCreationDate { set; get; }
		public string firstname { get; set; }
		public string lastname { get; set; }
		public string UserName { set; get; }
		public string Country { get; set; }
		public string? ProfilePicture { get; set; }
		public bool IsVerified { get; set; }
		public bool isAvailable { get; set; }

		[InverseProperty("Reviewee")]
		public virtual List<Review> Reviewed { get; set; }
		public List<Certificate> Certificates { get; set; }
		public List<UserSkill> UserSkills { get; set; }
		public Education Education { get; set; }
		public List<Experience> Experiences { get; set; }
		public List<FreelancerLanguage> Languages { get; set; }
		public List<PortofolioProject> Portofolio { get; set; }
	}
}
