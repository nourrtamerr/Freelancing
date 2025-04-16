using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Freelancing.DTOs;
using Freelancing.Models;
using AutoMapper;
using Microsoft.CodeAnalysis;

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortofolioProjectImageController(IPortofolioProjectImage context, IMapper mapper, IPortofolioProject projectRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetImageByProjectId(int id)
        {
            var images = await context.GetByPortfolioProjectIdAsync(id);
            if (images == null)
            {
                return NotFound($"Image with ID {id} not found.");
            }

            var dto = mapper.Map<List<PortofolioProjectImageDTO>>(images);
            return Ok(dto);
        }


        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<PortofolioProjectImageDTO>> UploadImage([FromForm] UploadImageRequest request)
        {
            if (request.ImageFile == null || request.ImageFile.Length == 0)
                return BadRequest("No image uploaded");

            var project = await projectRepository.GetByIdAsync(request.ProjectId);
            if (project == null)
            {
                return NotFound($"Project with ID {request.ProjectId} not found.");

            }
               

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.ImageFile.FileName);
            var filePath = Path.Combine("wwwroot/images", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!); // Ensure folder exists

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.ImageFile.CopyToAsync(stream);
            }

            var image = new PortofolioProjectImage
            {
                Image = "/images/" + fileName,
                //PreviousProjectId = request.ProjectId
            };

            await context.AddAsync(new PortofolioProjectImageDTO
            {
                Image = image.Image,
                PreviousProjectId = image.PreviousProjectId
            });

            return Ok(image);
        }



        // POST: api/PortofolioProjectImage
        //[HttpPost]
        //public async Task<ActionResult<PortofolioProjectImageDTO>> AddImage(PortofolioProjectImageDTO portofolioProjectImageDTO)
        //{
        //    var addedImage = await context.AddAsync(portofolioProjectImageDTO);


        //    return CreatedAtAction("GetImageById", new { id = addedImage.Id }, addedImage);

        //}

        // DELETE: api/PortofolioProjectImage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePortofolioProjectImage(int id)
        {
            var portofolioProjectImageDTO = await context.DeleteAsync(id);
            if (portofolioProjectImageDTO == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
