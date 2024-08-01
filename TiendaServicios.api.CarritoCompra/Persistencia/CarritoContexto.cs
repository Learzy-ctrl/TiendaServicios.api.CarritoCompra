using Microsoft.EntityFrameworkCore;
using TiendaServicios.api.CarritoCompra.Modelo;

namespace TiendaServicios.api.CarritoCompra.Persistencia
{
    public class CarritoContexto :DbContext
    {
        public CarritoContexto(DbContextOptions<CarritoContexto> options) : base(options) { }

        public DbSet<CarritoSesion> carritoSesions { get; set; }
        public DbSet<CarritoSesionDetalle> carritoSesionDetalles { get; set; }
    }
}
