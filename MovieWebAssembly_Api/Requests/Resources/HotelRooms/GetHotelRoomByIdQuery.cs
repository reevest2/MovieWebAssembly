using AutoMapper;
using Business.Repository;
using MediatR;
using Models;

namespace MovieWebAssembly_Api.Requests.Resources.HotelRooms;

public record GetHotelRoomByIdQuery(int id) : IRequest<HotelRoomDTO>
{
    public class Handler : IRequestHandler<GetHotelRoomByIdQuery, HotelRoomDTO>
    {
        private readonly IHotelRoomRepository _hotelRoomRepository;
        private readonly IMapper _mapper;
        
        public Handler(IHotelRoomRepository hotelRoomRepository, IMapper mapper)
        {
            _hotelRoomRepository = hotelRoomRepository;
            _mapper = mapper;
        }
        
        public async Task<HotelRoomDTO> Handle(GetHotelRoomByIdQuery request, CancellationToken cancellationToken)
        {
            var room = await  _hotelRoomRepository.GetByIdAsync(request.id);
            var result = _mapper.Map<HotelRoomDTO>(room);
            return result;
        }
    }
}
