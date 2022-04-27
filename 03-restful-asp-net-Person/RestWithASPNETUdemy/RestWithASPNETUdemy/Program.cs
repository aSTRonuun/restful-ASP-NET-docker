using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Business.Implementations;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Repository.Implementations;
using Serilog;
using RestWithASPNETUdemy.Repository.Generic;
using RestWithASPNETUdemy.Hypermedia.Filters;
using RestWithASPNETUdemy.Hypermedia.Enricher;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
}));

// Add suported for formatters
builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;

    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", "application/json");
})
.AddXmlSerializerFormatters();

var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());

builder.Services.AddSingleton(filterOptions);

// Versiong API
builder.Services.AddApiVersioning();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "REST API's From 0 to Azure with ASP.NET Core 5 and Docker",
        Version = "v1",
        Description = "API RESTful developed in course 'REST API's From 0 to Azure with ASP.NET Core 5 and Docker'",
        Contact = new OpenApiContact
        {
            Name = "Vitor Alves",
            Url = new Uri("https://github.com/aSTRonuun")
        }
    });
});

//Dependency Injection
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));


var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    MigrationDatabase(connection);
}

void MigrationDatabase(string connection)
{
    try
    {
        var evolveConnection = new MySqlConnector.MySqlConnection(connection);
        var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
        {
            Locations = new List<string> { "db/migrations", "db/dataset" },
            IsEraseDisabled = true,
        };
        evolve.Migrate();
    } 
    catch (Exception ex)
    {
        Log.Error("Database migration failed", ex);
        throw;
    }
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "REST API's From 0 to Azure with ASP.NET Core 5 and Docker - v1");
});

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute("DefaultApi", "{controller=Values}/{id?}");
});

app.Run();



