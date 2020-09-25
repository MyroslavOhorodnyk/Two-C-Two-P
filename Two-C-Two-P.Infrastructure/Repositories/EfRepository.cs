using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Two_C_Two_P.Core.Interfaces;
using Two_C_Two_P.Core.Interfaces.Repositories;
using Two_C_Two_P.Infrastructure.Models;

namespace Two_C_Two_P.Infrastructure.Repositories
{
    public class EfRepository<TEntity, TKey> : IAsyncRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        protected TwoCTwoPContext Context { get; }

        protected DbSet<TEntity> DbSet { get; }

        public EfRepository(TwoCTwoPContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
            Context.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IReadOnlyList<TEntity>> ListAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);

            return entity;
        }

        public async Task<IReadOnlyList<TEntity>> AddRangeAsync(params TEntity[] entities)
        {
            await DbSet.AddRangeAsync(entities);

            return entities;
        }
    }
}
