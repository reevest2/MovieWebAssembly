using DataAccess.Data.Abstractions;
using DataAccess.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DataAccess.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<HotelRoom> HotelRooms { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //Define Schema being used
        builder.Entity<IdentityUser>(entity =>
        {
            entity.ToTable(name: "AspNetUsers", schema: "Identity");
        });
        builder.Entity<IdentityRole>(entity =>
        {
            entity.ToTable(name: "AspNetRoles", schema: "Identity"); 
        });
        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable(name: "AspNetUserRoles", schema: "Identity");
        });
        builder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable(name: "AspNetUserClaims", schema: "Identity");
        });
        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable(name: "AspNetUserLogins", schema: "Identity");
        });
        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable(name: "AspNetUserTokens", schema: "Identity");
        });
        builder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.ToTable(name: "AspNetRoleClaims", schema: "Identity");
        });
        
        
        AddResourceConversion<HotelRoom>();

        void AddResourceConversion<TResource>() where TResource : ResourceBase
        {
            builder
                .Entity<TResource>();
        }
    }
}