using System.Reflection;
using Business.Repository;
using DataAccess.Data;
using DataAccess.Data.Abstractions;
using DataAccess.Data.Initizlier;
using DataAccess.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
builder.Services.AddScoped<IDbInitializer, ApplicationDbInitizlier>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", x =>
    {
        x.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var dtosAssembly = Assembly.Load("Models");
var dataAccessAssembly = Assembly.Load("DataAccess");

// Assuming your entity types inherit from ResourceBase
var entityTypes = dataAccessAssembly.GetTypes()
    .Where(t => t.IsClass && !t.IsAbstract && t.IsPublic && typeof(ResourceBase).IsAssignableFrom(t));

// Register GetResourceQuery for ReadAll
foreach (var entityType in entityTypes)
{
    var dtoName = $"Models.{entityType.Name}DTO";
    var dtoType = dtosAssembly.GetType($"{dtoName}", throwOnError: false);
    if (dtoType != null)
    {
        var handlerClosedType = typeof(GetResourceQuery<,>.ReadAllQueryHandler).MakeGenericType(dtoType, entityType);
        var requestClosedType = typeof(GetResourceQuery<,>.ReadAllQuery).MakeGenericType(dtoType, entityType);
        
        var iRequestHandlerClosedType = typeof(IRequestHandler<,>).MakeGenericType(requestClosedType, typeof(List<>).MakeGenericType(dtoType));
        
        builder.Services.AddTransient(iRequestHandlerClosedType, handlerClosedType);
    }
}

// Register GetResourceQuery for ReadById
foreach (var entityType in entityTypes)
{
    var dtoName = $"Models.{entityType.Name}DTO";
    var dtoType = dtosAssembly.GetType($"{dtoName}", throwOnError: false);
    if (dtoType != null)
    {
        var handlerClosedType = typeof(GetResourceQuery<,>.ReadByIdQueryHandler).MakeGenericType(dtoType, entityType);
        var requestClosedType = typeof(GetResourceQuery<,>.ReadByIdQuery).MakeGenericType(dtoType, entityType);
        
        var iRequestHandlerClosedType = typeof(IRequestHandler<,>).MakeGenericType(requestClosedType, dtoType);
        
        builder.Services.AddTransient(iRequestHandlerClosedType, handlerClosedType);
    }
}

//Register WriteResourceCommand for Create
foreach (var entityType in entityTypes)
{
    var dtoName = $"Models.{entityType.Name}DTO";
    var dtoType = dtosAssembly.GetType($"{dtoName}", throwOnError: false);
    if (dtoType != null)
    {
        var handlerClosedType = typeof(WriteResourceCommand<,>.CreateResourceCommandHandler).MakeGenericType(dtoType, entityType);
        var requestClosedType = typeof(WriteResourceCommand<,>.CreateResourceCommand).MakeGenericType(dtoType, entityType);
        
        var iRequestHandlerClosedType = typeof(IRequestHandler<,>).MakeGenericType(requestClosedType, dtoType);
        
        builder.Services.AddTransient(iRequestHandlerClosedType, handlerClosedType);
    }
}

//Register WriteResourceCommand for Update
foreach (var entityType in entityTypes)
{
    var dtoName = $"Models.{entityType.Name}DTO";
    var dtoType = dtosAssembly.GetType($"{dtoName}", throwOnError: false);
    if (dtoType != null)
    {
        var handlerClosedType = typeof(WriteResourceCommand<,>.UpdateResourceCommandHandler).MakeGenericType(dtoType, entityType);
        var requestClosedType = typeof(WriteResourceCommand<,>.UpdateResourceCommand).MakeGenericType(dtoType, entityType);
        
        var iRequestHandlerClosedType = typeof(IRequestHandler<,>).MakeGenericType(requestClosedType, dtoType);
        
        builder.Services.AddTransient(iRequestHandlerClosedType, handlerClosedType);
    }
}

//Register WriteResourceCommand for Delete
foreach (var entityType in entityTypes)
{
    var dtoName = $"Models.{entityType.Name}DTO";
    var dtoType = dtosAssembly.GetType($"{dtoName}", throwOnError: false);
    if (dtoType != null)
    {
        var handlerClosedType = typeof(WriteResourceCommand<,>.DeleteResourceCommandHandler).MakeGenericType(dtoType, entityType);
        var requestClosedType = typeof(WriteResourceCommand<,>.DeleteResourceCommand).MakeGenericType(dtoType, entityType);
        
        var iRequestHandlerClosedType = typeof(IRequestHandler<,>).MakeGenericType(requestClosedType, dtoType);
        
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


// Initialize Db
using (var scope = app.Services.CreateScope())
{
    var scopedServices = scope.ServiceProvider;
    var dbInitializer = scopedServices.GetRequiredService<IDbInitializer>();
    dbInitializer.Initialize();
}

app.Run();