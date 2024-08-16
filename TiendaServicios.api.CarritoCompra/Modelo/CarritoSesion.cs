using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TiendaServicios.api.CarritoCompra.Modelo
{
    public class CarritoSesion
    {
        [Key]
        public int CarritoSesionId { get; set; }
        public DateTime FechaCreacion {  get; set; }
        public ICollection<CarritoSesionDetalle> ListaDetalle {  get; set; }
    }
}
