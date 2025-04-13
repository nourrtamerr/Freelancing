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
        // GET: api/PortofolioProjectImage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PortofolioProjectImageDTO>> GetPortofolioProjectImageDTO(int id)
        {
            var portofolioProjectImageDTO = await context.GetByPortfolioProjectIdAsync(id);

            if (portofolioProjectImageDTO == null)
            {
                return NotFound();
            }

            return portofolioProjectImageDTO;
        }

        // POST: api/PortofolioProjectImage
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PortofolioProjectImageDTO>> PostPortofolioProjectImageDTO(PortofolioProjectImageDTO portofolioProjectImageDTO)
        {
            _context.PortofolioProjectImageDTO.Add(portofolioProjectImageDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPortofolioProjectImageDTO", new { id = portofolioProjectImageDTO.Id }, portofolioProjectImageDTO);
        }

        // DELETE: api/PortofolioProjectImage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePortofolioProjectImageDTO(int id)
        {
            var portofolioProjectImageDTO = await _context.PortofolioProjectImageDTO.FindAsync(id);
            if (portofolioProjectImageDTO == null)
            {
                return NotFound();
            }

            _context.PortofolioProjectImageDTO.Remove(portofolioProjectImageDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
