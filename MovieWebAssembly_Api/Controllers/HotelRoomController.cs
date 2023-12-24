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

    [HttpGet]
    public async Task<IEnumerable<HotelRoomDTO>> GetHotelRooms()
    {
        var result = await _mediator.Send(new GetHotelRoomsQuery());
        return result;
    }
}