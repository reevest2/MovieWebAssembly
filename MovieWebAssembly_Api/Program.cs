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

var dataAccessAssembly = Assembly.Load("DataAccess");
var modelsAssembly = Assembly.Load("Models");

// Scan for entity types in the DataAccess assembly
var entityTypes = dataAccessAssembly.GetTypes()
    .Where(t => t.IsClass && !t.IsAbstract && t.IsPublic && t.IsSubclassOf(typeof(ResourceBase))); // Assuming all entities inherit from ResourceBase

// Register each query handler for the found entity types
foreach (var entityType in entityTypes)
{
    // Assume DTOs follow naming convention of EntityName + "DTO"
    var dtoName = $"{entityType.Name}DTO";
    // Find DTO types in Models assembly
    var dtoType = modelsAssembly.GetType($"Models.{dtoName}", throwOnError: false);
    if (dtoType != null)
    {
        var handlerGenericType = typeof(ReadAll<,>).MakeGenericType(dtoType, entityType).GetNestedType("QueryHandler");
        var requestGenericType = typeof(ReadAll<,>).MakeGenericType(dtoType, entityType).GetNestedType("ReadQuery");
        var iRequestHandlerType = typeof(IRequestHandler<,>).MakeGenericType(requestGenericType, typeof(List<>).MakeGenericType(dtoType));

        builder.Services.AddTransient(iRequestHandlerType, handlerGenericType);
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