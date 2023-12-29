using DataAccess.Data.Abstractions;
using DataAccess.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models;
using MovieWebAssembly_Api.Requests.Generic;

namespace MovieWebAssembly_Api.Controllers.Abstractions;

public interface IReadController<TDto, TEntity>
    where TDto : class
    where TEntity : class
{
    Task<IActionResult> GetAll();
    Task<IActionResult> GetById(int id);
}

public interface IWriteController<TDto, TEntity>
    where TDto : class
    where TEntity : class
{
    Task<IActionResult> Create(TDto dto);
    Task<IActionResult> Update(int id, TDto dto);
    Task<IActionResult> Delete(int id);
}

public interface IReadWriteController<TDto, TEntity> : IReadController<TDto, TEntity>, IWriteController<TDto, TEntity> 
    where TDto : class 
    where TEntity : class
{

}

[ApiController]
public abstract class ReadController<TDto, TEntity> : Controller, IReadController<TDto, TEntity>
    where TDto : class
    where TEntity : ResourceBase
{
    protected readonly IMediator _mediator;

    protected ReadController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetResourceQuery<TDto, TEntity>.ReadAllQuery();
        var items = await _mediator.Send(query);
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetResourceQuery<TDto, TEntity>.ReadByIdQuery(id);
        var item = await _mediator.Send(query);
        return Ok(item);
    }
}

[ApiController]
public abstract class WriteController<TDto, TEntity> : Controller, IWriteController<TDto, TEntity>
    where TDto : class
    where TEntity :  ResourceBase
{
    protected readonly IMediator _mediator;

    protected WriteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(TDto dto)
    {
        var command = new WriteResourceCommand<TDto, TEntity>.CreateResourceCommand(dto);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TDto dto)
    {
        var command = new WriteResourceCommand<TDto, TEntity>.UpdateResourceCommand(dto, id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new WriteResourceCommand<TDto, TEntity>.DeleteResourceCommand(id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}

public abstract class ReadWriteController<TDto, TEntity> : Controller, IReadWriteController<TDto, TEntity>
    where TDto : class
    where TEntity : ResourceBase
{
    protected readonly IMediator _mediator;

    protected ReadWriteController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetResourceQuery<TDto, TEntity>.ReadAllQuery();
        var items = await _mediator.Send(query);
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetResourceQuery<TDto, TEntity>.ReadByIdQuery(id);
        var item = await _mediator.Send(query);
        return Ok(item);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(TDto dto)
    {
        var command = new WriteResourceCommand<TDto, TEntity>.CreateResourceCommand(dto);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TDto dto)
    {
        var command = new WriteResourceCommand<TDto, TEntity>.UpdateResourceCommand(dto, id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new WriteResourceCommand<TDto, TEntity>.DeleteResourceCommand(id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
}
