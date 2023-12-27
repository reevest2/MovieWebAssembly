using DataAccess.Data.Abstractions;
using MediatR;

namespace MovieWebAssembly_Api.Requests.Generic;

public partial class ReadAll
{
    public record ReadQuery<TEntity> : IRequest<List<TEntity>> where TEntity : ResourceBase;
}