namespace Freelancing.Models
{
	public class Freelancer : AppUser
	{
		public List<Certificate> Certificates { get; set; }
		public List<UserSkill> UserSkills { get; set; }
		public bool isAvailable { get; set; }
		public decimal Balance { get; set; } = 0;
		public Education Education { get; set; }
		public List<Experience> Experiences { get; set; }
		public List<FreelancerLanguage> Languages { get; set; }
		public List<PortofolioProject> Portofolio { get; set; }
        //public bool IsDeleted { get; set; } = false;

    }
	public enum Language
	{
		English,
		Spanish,
		French,
		German,
		Italian,
		Russian,
		Chinese,
		Japanese,
		Korean,
		Hindi
	}
	public enum FreelancerStatus
	{
		Available,
		Unavailable
	}
}
