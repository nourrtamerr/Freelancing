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
    public class PortofolioProjectDTOesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PortofolioProjectDTOesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PortofolioProjectDTOes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortofolioProjectDTO>>> GetPortofolioProjectDTO()
        {
            return await _context.PortofolioProjectDTO.ToListAsync();
        }

        // GET: api/PortofolioProjectDTOes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PortofolioProjectDTO>> GetPortofolioProjectDTO(int id)
        {
            var portofolioProjectDTO = await _context.PortofolioProjectDTO.FindAsync(id);

            if (portofolioProjectDTO == null)
            {
                return NotFound();
            }

            return portofolioProjectDTO;
        }

        // PUT: api/PortofolioProjectDTOes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPortofolioProjectDTO(int id, PortofolioProjectDTO portofolioProjectDTO)
        {
            if (id != portofolioProjectDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(portofolioProjectDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PortofolioProjectDTOExists(id))
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

        // POST: api/PortofolioProjectDTOes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PortofolioProjectDTO>> PostPortofolioProjectDTO(PortofolioProjectDTO portofolioProjectDTO)
        {
            _context.PortofolioProjectDTO.Add(portofolioProjectDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPortofolioProjectDTO", new { id = portofolioProjectDTO.Id }, portofolioProjectDTO);
        }

        // DELETE: api/PortofolioProjectDTOes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePortofolioProjectDTO(int id)
        {
            var portofolioProjectDTO = await _context.PortofolioProjectDTO.FindAsync(id);
            if (portofolioProjectDTO == null)
            {
                return NotFound();
            }

            _context.PortofolioProjectDTO.Remove(portofolioProjectDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PortofolioProjectDTOExists(int id)
        {
            return _context.PortofolioProjectDTO.Any(e => e.Id == id);
        }
    }
}
