using AutoMapper;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Business.Repository;

public interface IHotelRoomRepository
{
    public Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO);
    public Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO);   
    public Task<HotelRoomDTO> GetHotelRoom(int roomId);   
    public Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms();
    public Task<HotelRoomDTO> DoesRoomNameExist(string name);
    public Task<int> DeleteHotelRoom(int id);
}

public class HotelRoomRepository : IHotelRoomRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public HotelRoomRepository(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
    {
        HotelRoom hotelRoom = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO);
        hotelRoom.CreatedDate = DateTime.Now;
        hotelRoom.CreatedBy = "";
        var addedHotelRoom = _dbContext.HotelRooms.Add(hotelRoom);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<HotelRoom, HotelRoomDTO>(addedHotelRoom.Entity);
    }

    public async Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO)
    {
        try
        {
            if (roomId == hotelRoomDTO.Id)
            {
                HotelRoom roomDetails = await _dbContext.HotelRooms.FindAsync(roomId);
                HotelRoom room = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO, roomDetails);
                room.UpdatedBy = "";
                room.UpdatedDate = DateTime.Now;
                var updatedRoom = _dbContext.HotelRooms.Update(room);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<HotelRoom, HotelRoomDTO>(updatedRoom.Entity);
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<HotelRoomDTO> GetHotelRoom(int roomId)
    {
        try
        {
            HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                await _dbContext.HotelRooms.FirstOrDefaultAsync(x => x.Id == roomId));

            return hotelRoom;
        }
        catch (Exception exception)
        {
            return null;
        }
    }

    public async Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms()
    {
        try
        {
            IEnumerable<HotelRoomDTO> hotelRoomDTOs =
                _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>(_dbContext.HotelRooms);
            return hotelRoomDTOs;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<HotelRoomDTO> DoesRoomNameExist(string name)
    {
        try
        {
            HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                await _dbContext.HotelRooms.FirstOrDefaultAsync(x => x.Name == name));

            return hotelRoom;
        }
        catch (Exception exception)
        {
            return null;
        }
    }

    public async Task<int> DeleteHotelRoom(int roomId)
    {
        var roomDetails = await _dbContext.HotelRooms.FindAsync(roomId);
        if (roomDetails != null)
        {
            _dbContext.HotelRooms.Remove(roomDetails);
            return await _dbContext.SaveChangesAsync();

        }

        return 0;
    }
}

