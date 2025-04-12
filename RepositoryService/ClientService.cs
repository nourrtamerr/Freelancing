
using AutoMapper;
using Freelancing.DTOs.AuthDTOs;
using Microsoft.EntityFrameworkCore;

namespace Freelancing.RepositoryService
{
	public class ClientService(ApplicationDbContext _context,IMapper _mapper) : IClientService
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

		public async Task<IEnumerable<ViewClientDTO>> GetAllClients()
		{
			
			var clients = await _context.clients.Where(f => !f.isDeleted).ToListAsync();
			List<ViewClientDTO> clientsdtos = new();
			foreach (var client in clients)
			{
				clientsdtos.Add(_mapper.Map<ViewClientDTO>(client));
			}
			return clientsdtos;

		}

		public async Task<ViewClientDTO> GetClientById(string id)
		{
			var CLIENT = await _context.clients.Where(f => !f.isDeleted).FirstOrDefaultAsync(F => F.Id == id);

			return _mapper.Map<ViewClientDTO>(CLIENT);
		}

		public async Task<bool> UpdateClient(Client Client)
		{
			var client = _mapper.Map<Client>(await GetClientById(Client.Id));
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
