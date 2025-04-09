using Freelancing.IRepositoryService;
using Freelancing.Models;
using Microsoft.EntityFrameworkCore;

namespace Freelancing.RepositoryService
{
    public class UserSkillService(ApplicationDbContext context) : IUserSkillService
    {
        public async Task<UserSkill> CreateUserSkillAsync(UserSkill userskill)
        {
            UserSkill NewUserSkill = new UserSkill()
            {
                FreelancerId = userskill.FreelancerId,
                SkillId = userskill.SkillId,
                IsDelete = false,

            };
            context.UserSkills.Add(NewUserSkill);

            await context.SaveChangesAsync();

            return NewUserSkill;
        }

        public async Task<bool> DeleteUserSkillAsync(int id)
        {
            var userSkill = await context.UserSkills.FirstOrDefaultAsync(u => u.id == id);
            if (userSkill == null) 
            { 
                return false;
            }
            userSkill.IsDelete = true; 
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserSkill>> GetAllUserSkillAsync()
        {
           return await context.UserSkills.Where(u=>!u.IsDelete).ToListAsync();
        }

        public async Task<UserSkill> GetUserSkillByIDAsync(int id)
        {
            return await context.UserSkills.FirstOrDefaultAsync(u => u.id == id && !u.IsDelete);
        }

        public async Task<UserSkill> UpdateUserSkillAsync(UserSkill userskill)
        {
            var existingUserSkill = await context.UserSkills.FirstOrDefaultAsync(u => u.id == userskill.id && !u.IsDelete);
            if (existingUserSkill == null)
            {
                return null;
            }
            existingUserSkill.FreelancerId = userskill.FreelancerId;
            existingUserSkill.SkillId = userskill.SkillId;

            await context.SaveChangesAsync();
            return existingUserSkill;

        }
    }
}
