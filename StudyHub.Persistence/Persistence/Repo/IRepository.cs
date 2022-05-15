using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Infrastructure.Persistence.Repo
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Table { get; }
        Task<TEntity> GetEntity(object id);
        Task<List<TEntity>> GetEntities();
        Task<TEntity> InsertEntity(TEntity entity);
        Task<TEntity> UpdateEntity(TEntity entity);
        Task InsertEntities(List<TEntity> entities);
        Task DeleteEntity(TEntity entity);
        Task DeleteEntities(List<TEntity> entities);
        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities);
    }
}
