using Data.Contexts;
using Data.Entities;

namespace Data.Repositories
{
    public interface IUserAddressRepository : IBaseRepository<UserAddressEntity>
    {
    }
    public class UserAddressRepository(DataContext context) : BaseRepository<UserAddressEntity>(context), IUserAddressRepository
    {
    }
}
