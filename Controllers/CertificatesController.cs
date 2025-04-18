﻿using Freelancing.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        private readonly ICertificatesService _certificatesService;

        public CertificatesController(ICertificatesService certificatesService)
        {
            _certificatesService = certificatesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCertificates()
        {
            var certificateslist = await _certificatesService.GetAllUserCertificatesAsync();
            if (certificateslist == null || certificateslist.Count() == 0)
            {
                return BadRequest(new { msg = "there is no certificates found" });
            }
            var certificatesDTOlist = certificateslist.Select(e => new CertificateDTO
            {
                Id = e.Id,
                Name = e.Name,
                IssueDate = e.IssueDate,
                IsDeleted = e.IsDeleted,
                FreelancerName = e.Freelancer.UserName
            });
            return Ok(certificatesDTOlist);
        }
        
        [HttpGet("freelancer/{username}")]
        public async Task<IActionResult> GetAllCertificatesByFreelancerUserName(string username)
        {
            var certificateslist = await _certificatesService.GetAllCertificatesByFreelancerUserName(username);
            if (certificateslist == null)
            {
                return BadRequest(new {msg = "there is no certificates for this freelancer"});
            }
            var certificatesDTOlist = certificateslist.Select(e => new CertificateDTO
            {
                Id = e.Id,
                Name = e.Name,
                IssueDate = e.IssueDate,
                IsDeleted = e.IsDeleted,
                FreelancerName = e.Freelancer.UserName
            });
            return Ok(certificatesDTOlist);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCertificateByID(int id) {
            var selectedcertificate =await _certificatesService.GetCertificateByIDAsync(id);
            if (selectedcertificate == null)
            {
                return BadRequest(new {msg="can't find a certificate has this id"});
            }
            var certificateDTO = new CertificateDTO {
                Name = selectedcertificate.Name,
                IsDeleted = selectedcertificate.IsDeleted,
                IssueDate = selectedcertificate.IssueDate,
                FreelancerName = selectedcertificate.Freelancer.UserName,
            };
            return Ok(certificateDTO);
        }


        [HttpPost]
        [Authorize(Roles = "Freelancer")]
        public async Task<IActionResult> CreateCertificate([FromBody] CreateCertificateDTO certificateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(certificateDTO);
            }
            var certificate = new Certificate
            {
                Name = certificateDTO.Name,
                IssueDate = certificateDTO.IssueDate,
                IsDeleted = false,
                FreelancerId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };
            var created = await _certificatesService.CreateCertificateAsync(certificate);
            if (created)
            {
                var createdCertificate = await _certificatesService.GetCertificateByIDAsync(certificate.Id);
                return CreatedAtAction(nameof(GetCertificateByID), new { id = createdCertificate.Id },
                    new
                    {
                        createdCertificate.Id,
                        createdCertificate.Name,
                        createdCertificate.IssueDate,
                        createdCertificate.IsDeleted,
                        createdCertificate.Freelancer.UserName                                                
                    }
                    );
            }
            return BadRequest(new { msg = "failed to create Certificate" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCertificateById(int id,[FromBody]CreateCertificateDTO certificateDTO)
        {
            var selected = await _certificatesService.GetCertificateByIDAsync(id);
            if (selected != null)
            {
                selected.IssueDate = certificateDTO.IssueDate;
                selected.Name = certificateDTO.Name;
                var updated = await _certificatesService.UpdateCertificateAsync(selected);
                if (updated)
                {
                return Ok(new { msg = "certificate updated successfully"});
                }
                return BadRequest(new { msg = "failed to update certificate" });
            }
            return BadRequest(new { msg = "no certificate found has this id" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertificateById (int id)
        {
             var selected =await _certificatesService.GetCertificateByIDAsync(id);
            if (selected != null) {
                var deleted = await _certificatesService.DeleteCertificateAsync(id);
                if (!deleted)
                {
                    return BadRequest(new { msg = $"Unable to delete certificate {id}" });
                }
                return Ok(new { msg = "certificate marked as deleted successfully"});
            }
            return BadRequest(new { msg = "Unable to find certificate by this id " });
        }
    }
}
