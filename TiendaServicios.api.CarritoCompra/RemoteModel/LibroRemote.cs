﻿namespace TiendaServicios.api.CarritoCompra.RemoteModel
{
    public class LibroRemote
    {
        public Guid? LibreriaMaterialId { get; set; }
        public string Titulo { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public double Precio { get; set; }
        public byte[] Img { get; set; }
        public int? CuponId { get; set; }
        public Guid? AutorLibro {  get; set; }
    }
}
