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
    public async Task<IActionResult> GetHotelRooms()
    {
        var result = await _mediator.Send(new ReadAll.ReadQuery<HotelRoom>());
        return Ok(result);
    }

   
    [HttpGet("{id}")]
    public async Task<IActionResult> GetHotelRoomById(int id)
    {
        var result = await _mediator.Send(new GetHotelRoomByIdQuery(id));
        return Ok(result);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateHotelRoom(HotelRoomDTO hotelRoomDto)
    {
        var result = await _mediator.Send(new CreateHotelRoomCommand(hotelRoomDto));
        return Ok(result);
    }

    [HttpPost("Update")]
    public async Task<IActionResult> UpdateHotelRoom(HotelRoomDTO hotelRoomDto)
    {
        var result = await _mediator.Send(new UpdateHotelRoomCommand(hotelRoomDto));
        return Ok(result);
    }
    
    [HttpDelete]
    public async Task<IActionResult> UpdateHotelRoom(int id)
    {
        var result = await _mediator.Send(new DeleteHotelRoomCommand(id));
        return Ok(result);
    }
}