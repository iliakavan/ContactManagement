
using ContactManagementV2.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementV2.Repository;

public class Repository<T>(AppDbContext _contxet) : IRepository<T> where T : class
{
    private readonly AppDbContext contxet = _contxet;

    public void Add(T entity)
    {
        contxet.Set<T>().Add(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await contxet.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int Id)
    {
        return await contxet.Set<T>().FindAsync(Id);
    }

    public void Remove(T entity)
    {
        contxet.Set<T>().Remove(entity);
    }

    public void Update(T entity)
    {
        contxet.Set<T>().Update(entity);
    }
}