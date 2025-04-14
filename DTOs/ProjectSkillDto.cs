namespace Freelancing.DTOs
{
    public class ProjectSkillDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int SkillId { get; set; }
        public string SkillName { get; set; } = string.Empty;
    }
    public class ProjectSkillCreateDto
    {
        public int ProjectId { get; set; }
        public int SkillId { get; set; }
    }
    public class ProjectSkillUpdateDto
    {
        public int ProjectId { get; set; }
        public int SkillId { get; set; }
    }
}
