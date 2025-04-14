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

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortofolioProjectImageController(IPortofolioProjectImage context, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetImageById(int id)
        {
            var image = await context.GetByPortfolioProjectIdAsync(id);
            return Ok(image);
        }

        // POST: api/PortofolioProjectImage
        [HttpPost]
        public async Task<ActionResult<PortofolioProjectImageDTO>> AddImage(PortofolioProjectImageDTO portofolioProjectImageDTO)
        {
            var addedImage = await context.AddAsync(portofolioProjectImageDTO);
           

            return CreatedAtAction("GetImageById", new { id = addedImage.Id }, addedImage);
          
        }

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
