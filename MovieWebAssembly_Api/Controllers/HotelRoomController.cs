using Business.Repository;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Data;
using MediatR;
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
    public async Task<IEnumerable<HotelRoom>> GetHotelRooms()
    {
        //var allRooms = await _hotelRoomRepository.GetAllHotelRooms();
        //return Ok(allRooms);

        var result = await _mediator.Send(new GetHotelRoomsQuery());
        return result;
    }
}