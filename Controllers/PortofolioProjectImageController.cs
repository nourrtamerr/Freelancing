using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Freelancing.DTOs;
using Freelancing.Models;

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortofolioProjectImageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PortofolioProjectImageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PortofolioProjectImage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortofolioProjectImageDTO>>> GetPortofolioProjectImageDTO()
        {
            return await _context.PortofolioProjectImageDTO.ToListAsync();
        }

        // GET: api/PortofolioProjectImage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PortofolioProjectImageDTO>> GetPortofolioProjectImageDTO(int id)
        {
            var portofolioProjectImageDTO = await _context.PortofolioProjectImageDTO.FindAsync(id);

            if (portofolioProjectImageDTO == null)
            {
                return NotFound();
            }

            return portofolioProjectImageDTO;
        }

        // PUT: api/PortofolioProjectImage/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPortofolioProjectImageDTO(int id, PortofolioProjectImageDTO portofolioProjectImageDTO)
        {
            if (id != portofolioProjectImageDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(portofolioProjectImageDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PortofolioProjectImageDTOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
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

        private bool PortofolioProjectImageDTOExists(int id)
        {
            return _context.PortofolioProjectImageDTO.Any(e => e.Id == id);
        }
    }
}
