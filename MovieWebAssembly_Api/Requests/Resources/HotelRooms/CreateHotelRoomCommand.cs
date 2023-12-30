using AutoMapper;
using Business.Repository;
using DataAccess.Data.Models;
using MediatR;
using Models;

namespace MovieWebAssembly_Api.Requests.Resources.HotelRooms
{
    public record CreateHotelRoomCommand(HotelRoomDTO HotelRoomDto) : IRequest<HotelRoom>
    {
        public class Handler : IRequestHandler<CreateHotelRoomCommand, HotelRoom>
        {
            private readonly IHotelRoomRepository _hotelRoomRepository;
            private readonly IMapper _mapper;
            
            public Handler(IHotelRoomRepository hotelRoomRepository, IMapper mapper)
            {
                _hotelRoomRepository = hotelRoomRepository;
                _mapper = mapper;
            }
            
            public async Task<HotelRoom> Handle(CreateHotelRoomCommand request, CancellationToken cancellationToken)
            {
                var room = _mapper.Map<HotelRoom>(request.HotelRoomDto);
                var result = await _hotelRoomRepository.CreateResource(room);
                return room;
            }
        }
    }
}