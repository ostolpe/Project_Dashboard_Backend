using Data.Contexts;
using Data.Entities;

namespace Data.Repositories
{
    public interface IClientAddressRepository : IBaseRepository<ClientAddressEntity>
    {
    }
    public class ClientAddressRepository(DataContext context) : BaseRepository<ClientAddressEntity>(context), IClientAddressRepository
    {
    }
}
