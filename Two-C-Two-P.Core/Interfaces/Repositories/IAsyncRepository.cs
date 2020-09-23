using System.Collections.Generic;
using System.Threading.Tasks;

namespace Two_C_Two_P.Core.Interfaces.Repositories
{
    public interface IAsyncRepository<TEntity, in TKey> where TEntity : class, IEntity<TKey>
    {
        Task<TEntity> GetByIdAsync(TKey id);

        Task<IReadOnlyList<TEntity>> ListAllAsync();
    }
}
