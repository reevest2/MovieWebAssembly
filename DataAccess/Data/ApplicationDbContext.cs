using DataAccess.Data.Abstractions;
using DataAccess.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DataAccess.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    { }

    public DbSet<HotelRoom> HotelRooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        AddResourceConversion<HotelRoom>();

        void AddResourceConversion<TResource>() where TResource : ResourceBase
        {
            modelBuilder
                .Entity<TResource>();
        }
    }
}