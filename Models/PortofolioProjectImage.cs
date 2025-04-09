namespace Freelancing.Models
{
    public class PortofolioProjectImage
    {
		public int Id { get; set; }
		public string Image { get; set; }
		public int PreviousProjectId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public PortofolioProject PreviousProject { get; set; }
	}
}
