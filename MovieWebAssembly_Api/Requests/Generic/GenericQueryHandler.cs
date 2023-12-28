using AutoMapper;
using Business.Repository;
using DataAccess.Data.Abstractions;
using MediatR;

namespace MovieWebAssembly_Api.Requests.Generic
{
    public abstract class GetResourceQuery<TModel, TEntity> where TEntity : ResourceBase
    {
        public record ReadAllQuery : IRequest<List<TModel>>;
        public record ReadByIdQuery(int Id) : IRequest<TModel>;

        public class ReadAllQueryHandler : IRequestHandler<ReadAllQuery, List<TModel>>
        {
            private readonly IResourceRepository<TEntity> _repository;
            private readonly IMapper _mapper;

            public ReadAllQueryHandler(IResourceRepository<TEntity> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<List<TModel>> Handle(ReadAllQuery request, CancellationToken cancellationToken)
            {
                var entities = await _repository.GetAllAsync();

                return _mapper.Map<List<TModel>>(entities);
            }
        }
        
        public class ReadByIdQueryHandler : IRequestHandler<ReadByIdQuery, TModel>
        {
            private readonly IResourceRepository<TEntity> _repository;
            private readonly IMapper _mapper;

            public ReadByIdQueryHandler(IResourceRepository<TEntity> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<TModel> Handle(ReadByIdQuery request, CancellationToken cancellationToken)
            {
                var entities = await _repository.GetByIdAsync(request.Id);

                return _mapper.Map<TModel>(entities);
            }
        }
    }
}