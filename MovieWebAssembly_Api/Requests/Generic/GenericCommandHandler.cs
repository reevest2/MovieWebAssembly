using AutoMapper;
using Business.Repository;
using DataAccess.Data.Abstractions;
using MediatR;

namespace MovieWebAssembly_Api.Requests.Generic;

public abstract class WriteResourceCommand<TModel, TEntity> where TEntity : ResourceBase
{
    public record CreateResourceCommand(TModel data) : IRequest<TModel>;
    public class CreateResourceCommandHandler : IRequestHandler<CreateResourceCommand, TModel>
    {
        private readonly IResourceRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public CreateResourceCommandHandler(IResourceRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TModel> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
        {
            var entityToCreate = _mapper.Map<TModel, TEntity>(request.data);
            var createdEntity = await _repository.CreateResource(entityToCreate);
            var model = _mapper.Map<TModel>(createdEntity);
            return model;
        }
    }
}