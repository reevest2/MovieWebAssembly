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
        // Create a new ReadQuery instance
        var query = new ReadAll<HotelRoomDTO, HotelRoom>.ReadQuery();

        // Send the query to MediatR which will trigger the corresponding handler
        var hotelRooms = await _mediator.Send(query);

        // Return the list of hotel room models
        return Ok(hotelRooms);
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