using Freelancing.DTOs;

namespace Freelancing.RepositoryService
{
	public class CityService(ApplicationDbContext context):ICityService
	{
		public List<City> GetAll()
		{
			return context.Cities.Include(c => c.Country).Where(c => !c.isDeleted).ToList();
		}

		public City GetById(int id)
		{
			return context.Cities.Include(c => c.Country).SingleOrDefault(c => c.Id == id);
		}

		public void Create(CityViewModel vm)
		{
			City city = new City()
			{
				Name = vm.Name,
				CountryId = vm.CountryId
			};
			context.Cities.Add(city);
			context.SaveChanges();
		}

		public void Delete(int id)
		{
			var cty = context.Cities.SingleOrDefault(c => c.Id == id);
			cty.isDeleted = true;
			context.Cities.Update(cty);
			context.SaveChanges();
		}



		public void Update(CityViewModel vm)
		{
			var cty = context.Cities.SingleOrDefault(c => c.Id == vm.Id);
			cty.Name = vm.Name;
			cty.CountryId = vm.CountryId;
			
			context.SaveChanges();
		}
	}
}
