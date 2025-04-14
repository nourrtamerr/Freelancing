namespace Freelancing.DTOs
{
    public class UserSkillDto
    {
        public int Id { get; set; }
        public string FreelancerId { get; set; }
        public int SkillId { get; set; }
        public string SkillName { get; set; } = string.Empty;
    }
    public class UserSkillCreateDto
    {
        public string FreelancerId { get; set; }
        public int SkillId { get; set; }
    }
    public class UserSkillUpdateDto
    {
        public string FreelancerId { get; set; }
        public int SkillId { get; set; }
    }
}
