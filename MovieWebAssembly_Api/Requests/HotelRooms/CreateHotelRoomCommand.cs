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
    public record CreateHotelRoomCommand(HotelRoomDTO HotelRoomDto) : IRequest<bool>
    {
        public class Handler : IRequestHandler<CreateHotelRoomCommand, bool>
        {
            private readonly IHotelRoomRepository _hotelRoomRepository;
            private readonly IMapper _mapper;
            
            public Handler(IHotelRoomRepository hotelRoomRepository, IMapper mapper)
            {
                _hotelRoomRepository = hotelRoomRepository;
                _mapper = mapper;
            }
            
            public async Task<bool> Handle(CreateHotelRoomCommand request, CancellationToken cancellationToken)
            {
                var room = _mapper.Map<HotelRoom>(request.HotelRoomDto);
                var result = await _hotelRoomRepository.CreateResource(room);
                return result;
            }
        }
    }
}