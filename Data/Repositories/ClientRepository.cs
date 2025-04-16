using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public interface IClientRepository : IBaseRepository<ClientEntity>
    {
    }

    public class ClientRepository(DataContext context) : BaseRepository<ClientEntity>(context), IClientRepository
    {
        public override async Task<IEnumerable<ClientEntity>> GetAllAsync()
        {
            var entities = await _dbSet
                .Include(x => x.Address)
                .ToListAsync();

            return entities;
        }

        public override async Task<ClientEntity?> GetAsync(Expression<Func<ClientEntity, bool>> expression)
        {
            var entity = await _dbSet
                .Include(x => x.Address)
                .FirstOrDefaultAsync(expression);

            return entity;
        }
    }
}
