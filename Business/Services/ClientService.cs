using Business.Dtos;
using Business.Factories;
using Business.Models;
using Data.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Business.Services
{
    public interface IClientService
    {
        Task<bool> CreateClientAsync(AddClientForm clientForm);
        Task<bool> DeleteClientAsync(string clientId);
        Task<Client?> GetClientByIdAsync(string clientId);
        Task<Client?> GetClientByNameAsync(string clientName);
        Task<IEnumerable<Client>?> GetClientsAsync();
        Task<bool> UpdateClientAsync(UpdateClientForm clientForm);
    }

    public class ClientService(IClientRepository clientRepository, IMemoryCache cache) : IClientService
    {
        private readonly IClientRepository _clientRepository = clientRepository;
        private readonly IMemoryCache _cache = cache;
        private const string _cacheKey_All = "Client_All";

        public async Task<bool> CreateClientAsync(AddClientForm clientForm)
        {
            if (clientForm == null)
                return false;

            var entity = ClientFactory.CreateClientEntity(clientForm);

            var result = await _clientRepository.AddAsync(entity);

            if (result)
            {
                _cache.Remove(_cacheKey_All);
                return true;
            }

            return result;
        }

        public async Task<IEnumerable<Client>?> GetClientsAsync()
        {
            if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<Client>? cachedItems))
                return cachedItems;

            var clients = await SetCache();
            return clients;
        }

        public async Task<Client?> GetClientByNameAsync(string clientName)
        {
            var client = new Client();

            if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<Client>? cachedItems))
            {
                client = cachedItems?.FirstOrDefault(x => x.Name == clientName);
                if (client != null)
                    return client;
            }

            var entity = await _clientRepository.GetAsync(x => x.Name == clientName);
            if (entity == null)
                return null;

            await SetCache();

            client = ClientFactory.CreateClient(entity);

            return client;
        }

        public async Task<Client?> GetClientByIdAsync(string clientId)
        {
            var client = new Client();

            if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<Client>? cachedItems))
            {
                client = cachedItems?.FirstOrDefault(x => x.Id == clientId);
                if (client != null)
                    return client;
            }

            var entity = await _clientRepository.GetAsync(x => x.Id == clientId);
            if (entity == null)
                return null;

            await SetCache();

            client = ClientFactory.CreateClient(entity);
            return client;
        }

        public async Task<bool> UpdateClientAsync(UpdateClientForm clientForm)
        {
            if (clientForm == null)
                return false;

            var entity = await _clientRepository.GetAsync(x => x.Id == clientForm.Id);
            if (entity == null)
                return false;

            ClientFactory.UpdateClient(entity, clientForm);

            var result = await _clientRepository.UpdateAsync(entity);

            if (result)
                _cache.Remove(_cacheKey_All);

            return result;
        }
        public async Task<bool> DeleteClientAsync(string clientId)
        {
            if (string.IsNullOrEmpty(clientId))
                return false;

            var result = await _clientRepository.DeleteAsync(x => x.Id == clientId);
            if (result)
                _cache.Remove(_cacheKey_All);

            return result;
        }

        public async Task<IEnumerable<Client>> SetCache()
        {
            _cache.Remove(_cacheKey_All);
            var entities = await _clientRepository.GetAllAsync();
            var clients = entities.Select(ClientFactory.CreateClient);

            clients = clients.OrderBy(x => x.Id);
            _cache.Set(_cacheKey_All, clients, TimeSpan.FromMinutes(10));

            return clients;
        }
    }
}
