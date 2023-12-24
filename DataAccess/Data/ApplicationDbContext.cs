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

    public DbSet<Resource<HotelRoom>> HotelRooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        AddResourceConversion<HotelRoom>();

        void AddResourceConversion<TResource>() where TResource : ResourceBase
        {
            modelBuilder
                .Entity<Resource<TResource>>()
                .Property(r => r.Data)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<TResource>(v));
        }
    }
}