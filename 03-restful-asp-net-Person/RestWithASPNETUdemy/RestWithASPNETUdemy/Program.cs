using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Versiong API
builder.Services.AddApiVersioning();

//Dependency Injection
builder.Services.AddScoped<IPersonService, PersonServiceImplementation>();

var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
