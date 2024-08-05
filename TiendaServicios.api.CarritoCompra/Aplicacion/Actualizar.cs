using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.api.CarritoCompra.Modelo;
using TiendaServicios.api.CarritoCompra.Persistencia;
using TiendaServicios.api.CarritoCompra.RemoteInterface;

namespace TiendaServicios.api.CarritoCompra.Aplicacion
{
    public class Actualizar
    {
        public class Ejecuta : IRequest
        {
            public int CarritoSessionId { get; set; }
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
                int id = request.CarritoSessionId;
                foreach (var p in request.ProductoLista)
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
                if (value > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el detalle del carrito de compras");
            }
        }
    }
}
