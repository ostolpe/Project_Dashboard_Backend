using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public interface IProjectRepository : IBaseRepository<ProjectEntity>
    {
        //Task<IEnumerable<ProjectEntity>> GetAllAsync();
        //Task<ProjectEntity?> GetAsync(Expression<Func<ProjectEntity, bool>> expression);
    }

    public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
    {
        public override async Task<IEnumerable<ProjectEntity>> GetAllAsync()
        {
            var entities = await _dbSet
                .Include(x => x.Client)
                .Include(x => x.Status)
                .Include(x => x.User)
                .ToListAsync();

            return entities;
        }

        public override async Task<ProjectEntity?> GetAsync(Expression<Func<ProjectEntity, bool>> expression)
        {
            var entity = await _dbSet
                .Include(x => x.Client)
                .Include(x => x.Status)
                .Include(x => x.User)
                .FirstOrDefaultAsync(expression);
            return entity;
        }
    }
}
