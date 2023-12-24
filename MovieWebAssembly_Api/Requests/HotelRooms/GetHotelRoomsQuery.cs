using AutoMapper;
using Business.Repository;
using DataAccess.Data;
using MediatR;

namespace MovieWebAssembly_Api.Requests.HotelRooms;

public record GetHotelRoomsQuery() : IRequest<IEnumerable<HotelRoom>>
{
    public class Handler : IRequestHandler<GetHotelRoomsQuery, IEnumerable<HotelRoom>>
    {
        private readonly IHotelRoomRepository _hotelRoomRepository;
        private readonly IMapper _mapper;
        
        public Handler(IHotelRoomRepository hotelRoomRepository, IMapper mapper)
        {
            _hotelRoomRepository = hotelRoomRepository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<HotelRoom>> Handle(GetHotelRoomsQuery request, CancellationToken cancellationToken)
        {
            var allRooms = await  _hotelRoomRepository.GetAllHotelRooms();
            var result = _mapper.Map<IEnumerable<HotelRoom>>(allRooms);
            return result;
        }
    }
}