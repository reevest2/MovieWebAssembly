using AutoMapper;
using Business.Repository;
using DataAccess.Data;
using MediatR;
using Models;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Data.Models;

namespace MovieWebAssembly_Api.Requests.HotelRooms
{
    public record DeleteHotelRoomCommand(int id) : IRequest<bool>
    {
        public class Handler : IRequestHandler<DeleteHotelRoomCommand, bool>
        {
            private readonly IHotelRoomRepository _hotelRoomRepository;
            private readonly IMapper _mapper;
            
            public Handler(IHotelRoomRepository hotelRoomRepository, IMapper mapper)
            {
                _hotelRoomRepository = hotelRoomRepository;
                _mapper = mapper;
            }
            
            public async Task<bool> Handle(DeleteHotelRoomCommand request, CancellationToken cancellationToken)
            {
                var result = await _hotelRoomRepository.DeleteResource(request.id);
                return result;
            }
        }
    }
}