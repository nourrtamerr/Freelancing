using Freelancing.Models;

namespace Freelancing.IRepositoryService
{
	public interface IClientService
	{
		Task<bool> AddClient(Client Client);
		Task<bool> UpdateClient(Client Client);
		Task<bool> DeleteClient(string id);
		Task<Client> GetClientById(string id);
		Task<IEnumerable<Client>> GetAllClients();
		Task<bool> ClientExists(string id);

	}
}
