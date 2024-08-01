using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Http.Headers;
using TiendaServicios.api.CarritoCompra.Aplicacion;
using TiendaServicios.api.CarritoCompra.Persistencia;
using TiendaServicios.api.CarritoCompra.RemoteInterface;
using TiendaServicios.api.CarritoCompra.RemoteServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());
builder.Services.AddDbContext<CarritoContexto>(options =>
{
    string connection = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connection);
});
builder.Services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
builder.Services.AddAutoMapper(typeof(Consulta.Manejador));
builder.Services.AddTransient<ILibroService, LibrosService>();
builder.Services.AddTransient<IAutorService, AutorService>();
builder.Services.AddHttpClient("Libros", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:Libros"]);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});
builder.Services.AddHttpClient("Autor", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:Autores"]);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();