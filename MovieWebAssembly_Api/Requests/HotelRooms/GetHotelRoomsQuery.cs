using AutoMapper;
using Business.Repository;
using DataAccess.Data;
using MediatR;
using Models;

namespace MovieWebAssembly_Api.Requests.HotelRooms;

public record GetHotelRoomsQuery() : IRequest<IEnumerable<HotelRoomDTO>>
{
    public class Handler : IRequestHandler<GetHotelRoomsQuery, IEnumerable<HotelRoomDTO>>
    {
        private readonly IHotelRoomRepository _hotelRoomRepository;
        private readonly IMapper _mapper;
        
        public Handler(IHotelRoomRepository hotelRoomRepository, IMapper mapper)
        {
            _hotelRoomRepository = hotelRoomRepository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<HotelRoomDTO>> Handle(GetHotelRoomsQuery request, CancellationToken cancellationToken)
        {
            var allRooms = await  _hotelRoomRepository.GetAllHotelRooms();
            //var result = _mapper.Map<IEnumerable<HotelRoom>>(allRooms);
            return allRooms;
        }
    }
}