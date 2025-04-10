using Freelancing.DTOs;
using Freelancing.IRepositoryService;
using Freelancing.Models;

namespace Freelancing.RepositoryService
{
    public class PortofolioProgectImageService(ApplicationDbContext context) : IPortofolioProjectImage
    {

        public async Task<List<PortofolioProjectImage>> GetByPortfolioProjectIdAsync(int id)
        {
            var project = context.PortofolioProjects.SingleOrDefault(p => p.Id == id);
            var images = context.PortofolioProjectImages.Where(i => i.PreviousProjectId == project.Id).ToList();
            return images;
        }



        public async Task<PortofolioProjectImage> AddAsync(PortofolioProjectImageDTO portofolioProjectImage)
        {
            PortofolioProjectImage p = new PortofolioProjectImage()
            {
                Image = portofolioProjectImage.Image,
                PreviousProjectId = portofolioProjectImage.PreviousProjectId
            };
            context.PortofolioProjectImages.Add(p);
            await context.SaveChangesAsync();
            return p;
        }



        public async Task<bool> DeleteAsync(int id)
        {
            var image = context.PortofolioProjectImages.SingleOrDefault(p => p.Id == id && !p.IsDeleted);
            if(image is not null)
            {
                image.IsDeleted = true;
                context.Update(image);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }


    }
}
