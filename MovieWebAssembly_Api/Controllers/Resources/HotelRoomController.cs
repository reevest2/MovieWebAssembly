using DataAccess.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models;
using MovieWebAssembly_Api.Controllers.Abstractions;

namespace MovieWebAssembly_Api.Controllers.Resources;

[ApiController]
[Route("[controller]")]
public partial class HotelRoomController : ReadWriteController<HotelRoomDTO,HotelRoom>
{
    public HotelRoomController(IMediator mediator) : base(mediator)
    {
    }

}
