using AutoMapper;
using Business.Repository;
using DataAccess.Data.Abstractions;
using MediatR;

namespace MovieWebAssembly_Api.Requests.Generic
{
    // TModel could represent DTO or ViewModel or any other model type
    public abstract class ReadAll<TModel, TEntity> where TEntity : ResourceBase
    {
        // This ReadQuery now returns a List of TModel instead of TEntity
        public record ReadQuery : IRequest<List<TModel>>;

        public class QueryHandler : IRequestHandler<ReadQuery, List<TModel>>
        {
            private readonly IResourceRepository<TEntity> _repository;
            private readonly IMapper _mapper;

            public QueryHandler(IResourceRepository<TEntity> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<List<TModel>> Handle(ReadQuery request, CancellationToken cancellationToken)
            {
                // Get all entities 
                var entities = await _repository.GetAllAsync();

                // Map the list of entities to a list of models before returning
                return _mapper.Map<List<TModel>>(entities);
            }
        }
    }
    
}
