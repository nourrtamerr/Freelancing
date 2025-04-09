namespace Freelancing.Models
{
	public class PortofolioProject
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime CreatedAt { get; set; } // appwork m3mlhash
        public bool IsDeleted { get; set; } = false;
        public virtual List<PortofolioProjectImage> Images { get; set; } = new List<PortofolioProjectImage>();


	}
}
