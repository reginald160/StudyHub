using StudyHub.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StudyHub.Infrastructure.Persistence.Repo
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity> _entities;
        private ApplicationDbContext dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<TEntity> Table => Entities;

        private DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = dbContext.Set<TEntity>();
                }
                return _entities;
            }
        }

        public async Task DeleteEntities(List<TEntity> entities)
        {
            if (entities == null || entities.Count == 0)
                throw new ArgumentNullException("entity");

            Entities.RemoveRange(entities);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteEntity(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Entities.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetEntities()
        {
            return await Entities.ToListAsync();
        }

        public async Task<TEntity> GetEntity(object id)
        {
            return await Entities.FindAsync(id);
        }


        public async Task InsertEntities(List<TEntity> entities)
        {
            if (entities == null || entities.Count == 0)
                throw new ArgumentNullException("entities");

            await Entities.AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> InsertEntity(TEntity entity)
        {
            //if (entity == null)
            //    throw new ArgumentNullException("entity");
            //await Entities.AddAsync(entity);
            //await dbContext.SaveChangesAsync();
            //return entity;
            try
            {
                await Entities.AddAsync(entity);
                await dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception exp)
            {
                throw new ArgumentNullException("entity");
            }
        }

        public async Task<TEntity> UpdateEntity(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Entities.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entity");

            if (dbContext.Entry(entities).State.Equals(EntityState.Detached))
                dbContext.Set<TEntity>().AttachRange(entities);

            dbContext.Entry(entities).State = EntityState.Modified;
            return entities;
        }
    }
}
