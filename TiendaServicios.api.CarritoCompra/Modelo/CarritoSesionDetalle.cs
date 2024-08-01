using System.ComponentModel.DataAnnotations;

namespace TiendaServicios.api.CarritoCompra.Modelo
{
    public class CarritoSesionDetalle
    {
        [Key]
        public int CarritoSessionDetalleId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string ProductoSeleccionado { get; set; }
        public int CarritoSesionId { get; set; }
        public CarritoSesion carritoSesion { get; set; }
    }
}
