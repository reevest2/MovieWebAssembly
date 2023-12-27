using Business.Repository;
using DataAccess.Data;
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
builder.Services.AddTransient<IRequestHandler<ReadAll<HotelRoomDTO, HotelRoom>.ReadQuery, List<HotelRoomDTO>>, ReadAll<HotelRoomDTO, HotelRoom>.QueryHandler>();
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