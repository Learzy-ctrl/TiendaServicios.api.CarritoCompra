using MediatR;
using TiendaServicios.api.CarritoCompra.Modelo;
using TiendaServicios.api.CarritoCompra.Persistencia;

namespace TiendaServicios.api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public DateTime FechaCreacionSesion { get; set; }
            public List<string> ProductoLista { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CarritoContexto _contexto;

            public Manejador(CarritoContexto contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //se crea sesion de carrito al querer comprar los libros
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacionSesion
                };

                _contexto.carritoSesions.Add(carritoSesion);
                var result = await _contexto.SaveChangesAsync();
                if(result == 0)
                {
                    throw new Exception("No se pudo insertar en el carrito de compras");
                }
                //

                //de acuerdo con la sesion creada, se crea los detalles de dicha sesion
                int id = carritoSesion.CarritoSesionId;
                //cada detalle es de un libro, su referencia es el guid del libro
                foreach(var p in request.ProductoLista)
                {
                    var detalleSesion = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id,
                        ProductoSeleccionado = p
                    };
                    _contexto.carritoSesionDetalles.Add(detalleSesion);
                }
                var value = await _contexto.SaveChangesAsync();
                if(value > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el detalle del carrito de compras");
            }
        }
    }

}
