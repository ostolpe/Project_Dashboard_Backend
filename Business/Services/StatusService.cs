using Business.Models;
using Data.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Business.Services
{
    public interface IStatusService
    {
        Task<Status?> GetStatusByIdAsync(int id);
        Task<IEnumerable<Status>?> GetStatusesAsync();
    }

    public class StatusService(IStatusRepository statusRepository, IMemoryCache cache) : IStatusService
    {
        private readonly IStatusRepository _statusRepository = statusRepository;
        private readonly IMemoryCache _cache = cache;
        private const string _cacheKey_All = "Status_All";

        public async Task<IEnumerable<Status>?> GetStatusesAsync()
        {
            if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<Status>? cachedItems))
                return cachedItems;

            var statuses = await SetCache();
            return statuses;
        }

        public async Task<Status?> GetStatusByIdAsync(int id)
        {
            var status = new Status();

            if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<Status>? cachedItems))
            {
                status = cachedItems?.FirstOrDefault(x => x.Id == id);
                if (status != null)
                    return status;
            }

            var entity = await _statusRepository.GetAsync(x => x.Id == id);
            if (entity == null)
                return null;

            await SetCache();

            status = new Status { Id = entity.Id, Name = entity.Name };
            return status;
        }


        public async Task<IEnumerable<Status>> SetCache()
        {
            _cache.Remove(_cacheKey_All);
            var entities = await _statusRepository.GetAllAsync();
            var statuses = entities.Select(x => new Status { Id = x.Id, Name = x.Name });
            statuses = statuses.OrderBy(x => x.Id);
            _cache.Set(_cacheKey_All, statuses, TimeSpan.FromMinutes(10));

            return statuses;
        }
    }
}
