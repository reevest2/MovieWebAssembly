using AutoMapper;
using Business.Repository;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Data;
using DataAccess.Data.Models;
using MediatR;
using Models;
using MovieWebAssembly_Api.Requests.Generic;
using MovieWebAssembly_Api.Requests.HotelRooms;

namespace MovieWebAssembly_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HotelRoomController : Controller
{
    private readonly IMediator _mediator;

    public HotelRoomController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllHotelRooms()
    {
        var query = new GetResourceQuery<HotelRoomDTO,HotelRoom>.ReadAllQuery();
        var hotelRooms = await _mediator.Send(query);
        return Ok(hotelRooms);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetHotelRoomById(int id)
    {
        var query = new GetResourceQuery<HotelRoomDTO,HotelRoom>.ReadByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateHotelRoom(HotelRoomDTO hotelRoomDto)
    {
        var command = new WriteResourceCommand<HotelRoomDTO, HotelRoom>.CreateResourceCommand(hotelRoomDto);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("Update/{Id}")]
    public async Task<IActionResult> UpdateHotelRoom(HotelRoomDTO hotelRoomDto,int Id)
    {
        var command = new WriteResourceCommand<HotelRoomDTO, HotelRoom>.UpdateResourceCommand(hotelRoomDto, Id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("Delete/{Id}")]
    public async Task<IActionResult> UpdateHotelRoom(int Id)
    {
        var command = new WriteResourceCommand<HotelRoomDTO,HotelRoom>.DeleteResourceCommand(Id);
        var result = await _mediator.Send(new DeleteHotelRoomCommand(Id));
        return Ok(command);
    }
}