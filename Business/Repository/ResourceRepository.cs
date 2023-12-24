using DataAccess.Data;
using DataAccess.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository;

public interface IResourceRepository<T> where T : ResourceBase
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
}

public class ResourceRepository<TResource> : IResourceRepository<TResource> where TResource : ResourceBase
{
    protected readonly ApplicationDbContext _dbContext;

    public ResourceRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<List<TResource>> GetAllAsync()
    {
        return await _dbContext.Set<TResource>().ToListAsync();
    }

    public virtual async Task<TResource> GetByIdAsync(int id)
    {
        return await _dbContext.Set<TResource>().FindAsync(id);
    }
}