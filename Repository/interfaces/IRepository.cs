using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagementV2.Repository;

public interface IRepository<TEntity>  where TEntity : class
{
    void Add(TEntity entity);
    void Remove(TEntity entity);
    void Update(TEntity entity);
    Task<TEntity?> GetByIdAsync(int Id);
    Task<IEnumerable<TEntity>> GetAllAsync();
}
