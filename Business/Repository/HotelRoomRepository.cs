using AutoMapper;
using DataAccess.Data;
using DataAccess.Data.Models;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Business.Repository;

public interface IHotelRoomRepository : IResourceRepository<HotelRoom>
{
    
}

public class HotelRoomRepository : ResourceRepository<HotelRoom>, IHotelRoomRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public HotelRoomRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    
}

