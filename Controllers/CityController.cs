using Freelancing.DTOs;
using Freelancing.DTOs.AuthDTOs;
using Freelancing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freelancing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CityController : ControllerBase
	{
		private readonly ICityService _cityService;
		private readonly ICountryService _countryservice;

		public CityController(ICityService cityService, ICountryService countryService)
		{
			_cityService = cityService;
			_countryservice = countryService;
		}
		[HttpGet]
		public IActionResult Get()
		{
			return Ok(_cityService.GetAll().Select(c=>new {c.Name,countryname=c.Country.Name,c.Id}));
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var city = _cityService.GetById(id);
			if (city == null)
			{
				return NotFound();
			}

			return Ok(new { city.Name, countryname = city.Country.Name, city.Id });
		}


		[HttpPost]
		[Authorize(Roles = "Admin")]
		public IActionResult Create(CityViewModel vm)
		{
			if(vm.Id !=null && vm.Id!=0)
			{
				return BadRequest("cannot be created dont insert an id");
			}
			if (_countryservice.GetById(vm.CountryId) == null)
			{
				return BadRequest("Invalid Country");
			}
			_cityService.Create(vm);
			return Ok(new { Message = "Created" });
		}

		//// GET: Countries/Edit/5
		[HttpPut]
		//[Authorize(Roles = "Admin")]
		public IActionResult Edit(CityViewModel vm)
		{

			if (vm.Id is null)
			{
				return NotFound();
			}
			var country = _cityService.GetById((int)vm.Id);


			if (country == null)
			{
				return NotFound();
			}
			_cityService.Update(vm);
			return Ok(new { Message = "Updated" });
		}



		//// POST: Countries/Delete/5
		[HttpDelete]
		[Authorize(Roles = "Admin")]
		public IActionResult Delete(int id)
		{
			_cityService.Delete(id);

			return Ok(new { Message = "deleted" });
		}

	}
}
