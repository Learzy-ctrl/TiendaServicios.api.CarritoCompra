namespace TiendaServicios.api.CarritoCompra.Aplicacion
{
    public class CarritoDetalleDdto
    {
        public Guid? LibroId { get; set; }
        public string TituloLibro { get; set; }
        public double Precio { get; set; }
        public byte[] Img { get; set; }
        public int? CuponId { get; set; }
        public string AutorLibro { get; set; }
        public DateTime? FechaPublicacion { get; set; }
    }
}
