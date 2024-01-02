using DataAccess.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using MovieWebAssembly_Api.Controllers.Abstractions;

namespace MovieWebAssembly_Api.Controllers.Resources;

[ApiController]
[Route("[controller]")]
public partial class HotelRoomController : ReadWriteController<HotelRoomDTO, HotelRoom>
{
    public HotelRoomController(IMediator mediator) : base(mediator)
    {
}

    [HttpGet]
    [Authorize(Roles = "admin")]
    public override async Task<IActionResult> GetAll()
    {
        return await base.GetAll();
    }

}