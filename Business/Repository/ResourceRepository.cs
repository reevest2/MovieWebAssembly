using DataAccess.Data;
using DataAccess.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository;

public interface IResourceRepository<T> where T : ResourceBase
{
    Task<List<T>> GetAllAsync();
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
        return await _dbContext.Set<Resource<TResource>>()
            .Select(r => r.Data).ToListAsync();
    }
}