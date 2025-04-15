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

using System.Threading.Channels;

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortofolioProjectController : ControllerBase
    {
        private readonly IPortofolioProject portofolioProjectContext;
        private readonly IMapper mapper;

        public PortofolioProjectController(IPortofolioProject portofolioProject, IMapper mapper)
        {
            this.mapper = mapper;
            portofolioProjectContext = portofolioProject;
        }

        // GET: api/PortofolioProject
        [HttpGet]
        public async Task<IActionResult> GetPortofolioProjects()
        {
            var p = await portofolioProjectContext.GetAllAync();
            if (p == null)
            {
                return NotFound();
            }
            var DTO = mapper.Map<List<PortofolioProjectDTO>>(p);
            return Ok(DTO);
        }

        // GET: api/PortofolioProject/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPortofolioProjecBytId(int id)
        {
            var p = await portofolioProjectContext.GetByIdAsync(id);

            if (p == null)
            {
                return NotFound();
            }

            var DTO = mapper.Map<PortofolioProjectDTO>(p);
            return Ok(DTO);
        }

        // PUT: api/PortofolioProject/5
        //needs update bs kman shwaya
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PortofolioProjectDTO portofolioProjectDTO)
        {
            if (id != portofolioProjectDTO.Id)
            {
                return BadRequest();
            }

            //find the project
            var existingProject = await portofolioProjectContext.GetByIdAsync(id);
            if (existingProject == null)
            {
                return NotFound($"No project found with ID = {id}");
            }


            //existingProject.Title = portofolioProjectDTO.Title;
            //existingProject.Description=portofolioProjectDTO.Description;
            //existingProject.CreatedAt = DateTime.UtcNow;  
            //existingProject.FreelancerId=portofolioProjectDTO.FreelancerId;

            //prepare it to be updated (mapped it)
            mapper.Map<PortofolioProjectDTO>(existingProject);

            //update it
            var updatedProject = await portofolioProjectContext.UpdateAsync(portofolioProjectDTO);

            //to be sent back to respone as dto
            var updatedDTO = mapper.Map<PortofolioProjectDTO>(updatedProject);

            return Ok(portofolioProjectDTO);


        }

        //  POST: api/PortofolioProject
        [HttpPost]
        public async Task<ActionResult<PortofolioProjectDTO>> Create(PortofolioProjectDTO portofolioProjectDTO)
        {
            var createdProject = await portofolioProjectContext.AddAsync(portofolioProjectDTO);

            var dto = mapper.Map<PortofolioProjectDTO>(createdProject);

            return CreatedAtAction("GetPortofolioProjectDTO", new { id = dto.Id }, dto);
        }

        // DELETE: api/PortofolioProject/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePortofolioProject(int id)
        {
            var success = await portofolioProjectContext.DeleteAsync(id);
            if (!success)
            {
                return NotFound($"No project found with ID = {id}");
            }

            return NoContent();
        }

    }
}