using AutoMapper;
using Business.Repository;
using DataAccess.Data.Abstractions;
using MediatR;

namespace MovieWebAssembly_Api.Requests.Generic;

public abstract class WriteResourceCommand<TModel, TEntity> where TEntity : ResourceBase
{
    public record CreateResourceCommand(TModel Dto) : IRequest<TModel>;
    public record UpdateResourceCommand(TModel Dto, int Id) : IRequest<TModel>;
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
            var entityToCreate = _mapper.Map<TModel, TEntity>(request.Dto);
            var createdEntity = await _repository.CreateResource(entityToCreate);
            var model = _mapper.Map<TModel>(createdEntity);
            return model;
        }
    }
    
    public class UpdateResourceCommandHandler : IRequestHandler<UpdateResourceCommand, TModel>
    {
        private readonly IResourceRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public UpdateResourceCommandHandler(IResourceRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TModel> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
        {
            var entityToUpdate = _mapper.Map<TModel, TEntity>(request.Dto);
            var updatedEntity = await _repository.UpdateResource(entityToUpdate, request.Id);
            var model = _mapper.Map<TModel>(updatedEntity);
            return model;
        }
    }
}