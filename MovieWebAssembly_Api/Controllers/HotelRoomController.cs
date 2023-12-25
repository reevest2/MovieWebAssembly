using AutoMapper;
using Business.Repository;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Data;
using MediatR;
using Models;
using MovieWebAssembly_Api.Requests.HotelRooms;

namespace MovieWebAssembly_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelRoomController : Controller
{
    private readonly IHotelRoomRepository _hotelRoomRepository;
    private readonly IMediator _mediator;

    public HotelRoomController(IHotelRoomRepository hotelRoomRepository, IMediator mediator)
    {
        _hotelRoomRepository = hotelRoomRepository;
        _mediator = mediator;
    }

    [HttpGet("GetAllHotelRooms")]
    public async Task<IEnumerable<HotelRoomDTO>> GetHotelRooms()
    {
        var result = await _mediator.Send(new GetHotelRoomsQuery());
        return result;
    }

   
    [HttpGet("GetHotelRoomById/{id}")]
    public async Task<HotelRoomDTO> GetHotelRoomById(int id)
    {
        var result = await _mediator.Send(new GetHotelRoomByIdQuery(id));
        return result;
    }

    [HttpPost("CreateHotelRoom")]
    public async Task<bool> CreateHotelRoom(HotelRoomDTO hotelRoomDto)
    {
        var result = await _mediator.Send(new CreateHotelRoomCommand(hotelRoomDto));
        return result;
    }
    
    [HttpPost("UpdateHotelRoom")]
    public async Task<bool> UpdateHotelRoom(HotelRoomDTO hotelRoomDto)
    {
        var result = await _mediator.Send(new UpdateHotelRoomCommand(hotelRoomDto));
        return result;
    }
    
    [HttpDelete("DeleteHotelRoom")]
    public async Task<bool> UpdateHotelRoom(int id)
    {
        var result = await _mediator.Send(new DeleteHotelRoomCommand(id));
        return result;
    }
}