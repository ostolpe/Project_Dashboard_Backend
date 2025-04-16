using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
    }

    public class UserRepository(DataContext context) : BaseRepository<UserEntity>(context), IUserRepository
    {
        public override async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            var entities = await _dbSet
                .Include(x => x.Address)
                .ToListAsync();

            return entities;
        }

        public override async Task<UserEntity?> GetAsync(Expression<Func<UserEntity, bool>> expression)
        {
            var entity = await _dbSet
                .Include(x => x.Address)
                .FirstOrDefaultAsync(expression);

            return entity;
        }
    }
}
