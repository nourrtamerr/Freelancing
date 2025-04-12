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
using Humanizer;
using System.Threading.Channels;

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortofolioProjectController : ControllerBase
    {
        private readonly IPortofolioProject portofolioProject;
        private readonly IMapper mapper;

        public PortofolioProjectController(IPortofolioProject portofolioProject, IMapper mapper)
        {
            this.mapper = mapper;
            this.portofolioProject = portofolioProject;
        }

        // GET: api/PortofolioProject
        [HttpGet]
        public async Task<ActionResult<PortofolioProjectDTO>> GetPortofolioProject(int id)
        {
            var p = await portofolioProject.GetByIdAsync(id);
            if (p == null)
            {
                return NotFound();
            }
            var DTO = mapper.Map<PortofolioProjectDTO>(p); 
            return Ok(DTO);
        }

        // GET: api/PortofolioProject/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PortofolioProjectDTO>> GetPortofolioProjecBytId(int id)
        {
            var p = await portofolioProject.GetByIdAsync(id); 

            if (p == null)
            {
                return NotFound();
            }

            var DTO = mapper.Map<PortofolioProjectDTO>(p);
            return Ok(DTO);
        }

        // PUT: api/PortofolioProject/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PortofolioProjectDTO portofolioProjectDTO)
        {
            if (id != portofolioProjectDTO.Id)
            {
                return BadRequest();
            }
            var existingProject = await portofolioProject.GetByIdAsync(id);
            if (existingProject == null)
            {
                return NotFound($"No project found with ID = {id}");
            }


            //existingProject.Title = portofolioProjectDTO.Title;
            //existingProject.Description=portofolioProjectDTO.Description;
            //existingProject.CreatedAt = DateTime.UtcNow;  
            //existingProject.FreelancerId=portofolioProjectDTO.FreelancerId;

            mapper.Map(portofolioProjectDTO,existingProject);

            await portofolioProject.UpdateAsync(portofolioProjectDTO);
            return Ok(portofolioProjectDTO);

            
        }

        // POST: api/PortofolioProjectDTOes
        //[HttpPost]
        //public async Task<ActionResult<PortofolioProjectDTO>> Create(PortofolioProjectDTO portofolioProjectDTO)
        //{
        //    _context.PortofolioProjectDTO.Add(portofolioProjectDTO);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetPortofolioProjectDTO", new { id = portofolioProjectDTO.Id }, portofolioProjectDTO);
        //}

        //// DELETE: api/PortofolioProjectDTOes/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePortofolioProject(int id)
        //{
        //    var portofolioProjectDTO = await _context.PortofolioProjectDTO.FindAsync(id);
        //    if (portofolioProjectDTO == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.PortofolioProjectDTO.Remove(portofolioProjectDTO);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
