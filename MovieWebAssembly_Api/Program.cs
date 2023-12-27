using System.Reflection;
using Business.Repository;
using DataAccess.Data;
using DataAccess.Data.Abstractions;
using DataAccess.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using MovieWebAssembly_Api.Requests.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IHotelRoomRepository, HotelRoomRepository>();
builder.Services.AddScoped<IResourceRepository<HotelRoom>, HotelRoomRepository>();

var dtosAssembly = Assembly.Load("Models");
var dataAccessAssembly = Assembly.Load("DataAccess");

// Assuming your entity types inherit from ResourceBase
var entityTypes = dataAccessAssembly.GetTypes()
    .Where(t => t.IsClass && !t.IsAbstract && t.IsPublic && typeof(ResourceBase).IsAssignableFrom(t));

// Register each query handler for the found entity types
foreach (var entityType in entityTypes)
{
    // Assume DTOs follow naming convention of EntityName + "DTO"
    var dtoName = $"Models.{entityType.Name}DTO";
    // Find corresponding DTO type in DTOs assembly
    var dtoType = dtosAssembly.GetType($"{dtoName}", throwOnError: false);
    // If corresponding DTO type is found
    if (dtoType != null)
    {
        // Find or create the closed types for the handler and request
        var handlerClosedType = typeof(ReadAll<,>.QueryHandler).MakeGenericType(dtoType, entityType);
        var requestClosedType = typeof(ReadAll<,>.ReadQuery).MakeGenericType(dtoType, entityType);
        
        // IRequestHandler interface type closed with request and response types
        var iRequestHandlerClosedType = typeof(IRequestHandler<,>).MakeGenericType(requestClosedType, typeof(List<>).MakeGenericType(dtoType));
        
        // Register handler in the service collection
        builder.Services.AddTransient(iRequestHandlerClosedType, handlerClosedType);
    }
}

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(option => option.LowercaseUrls = true);
var app = builder.Build();
app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();


app.Run();