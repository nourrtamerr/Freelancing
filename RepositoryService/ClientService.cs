
using Microsoft.EntityFrameworkCore;

namespace Freelancing.RepositoryService
{
	public class ClientService(ApplicationDbContext _context) : IClientService
	{
		public async Task<bool> AddClient(Client Client)
		{


			await _context.clients.AddAsync(Client);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> ClientExists(string id)
		{
			return await _context.clients.AnyAsync(c => c.Id == id);
		}

		public async Task<bool> DeleteClient(string id)
		{
			var client= await _context.clients.FirstOrDefaultAsync(c => c.Id == id);
			if (client == null)
			{
				return false;
			}
			client.isDeleted = true;
			_context.clients.Update(client);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<IEnumerable<Client>> GetAllClients()
		{
			return await _context.clients.ToListAsync();
		}

		public async Task<Client> GetClientById(string id)
		{
			return await _context.clients.FirstOrDefaultAsync(c=>c.Id==id);
		}

		public async Task<bool> UpdateClient(Client Client)
		{
			var client = await GetClientById(Client.Id);
			if (client == null)
			{
				return false;
			}
			else
			{
				_context.clients.Update(client);
				return await _context.SaveChangesAsync() > 0;
			}
			
		}
	}
}
