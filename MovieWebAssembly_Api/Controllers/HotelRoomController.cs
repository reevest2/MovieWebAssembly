using AutoMapper;
using Business.Repository;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Data;
using DataAccess.Data.Models;
using MediatR;
using Models;
using MovieWebAssembly_Api.Controllers.Abstractions;
using MovieWebAssembly_Api.Requests.Generic;
using MovieWebAssembly_Api.Requests.HotelRooms;

namespace MovieWebAssembly_Api.Controllers;

[ApiController]
[Route("[controller]")]
public partial class HotelRoomController : ReadWriteController<HotelRoomDTO,HotelRoom>
{
    public HotelRoomController(IMediator mediator) : base(mediator)
    {
    }

}
