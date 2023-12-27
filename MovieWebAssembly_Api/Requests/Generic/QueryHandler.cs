using Business.Repository;
using DataAccess.Data.Abstractions;
using MediatR;

namespace MovieWebAssembly_Api.Requests.Generic;

public abstract class ReadAll
{
    public record ReadQuery<TEntity> : IRequest<List<TEntity>> where TEntity : ResourceBase;
    
    public class QueryHandler<TEntity> : IRequestHandler<ReadQuery<TEntity>, List<TEntity>> 
        where TEntity : ResourceBase
    {
        private readonly IResourceRepository<TEntity> _repository;

        public QueryHandler(IResourceRepository<TEntity> repository)
        {
            _repository = repository;
        }
        
        public async Task<List<TEntity>> Handle(ReadQuery<TEntity> request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}