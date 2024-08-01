using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.api.CarritoCompra.Persistencia;
using TiendaServicios.api.CarritoCompra.RemoteInterface;

namespace TiendaServicios.api.CarritoCompra.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<CarritoDto>
        {
            public int CarritoSessionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDto>
        {
            private readonly CarritoContexto carritoContexto;
            private readonly ILibroService libroService;
            private readonly IAutorService autorService;

            public Manejador(CarritoContexto _carritoContexto,  ILibroService _libroService, IAutorService _autorService)
            {
                carritoContexto = _carritoContexto;
                libroService = _libroService;
                autorService = _autorService;
            }

            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = await carritoContexto.carritoSesions.FirstOrDefaultAsync(x => x.CarritoSesionId ==
                    request.CarritoSessionId);
                var carritoSessionDetalle = await carritoContexto.carritoSesionDetalles.Where(x => x.CarritoSesionId ==
                    request.CarritoSessionId).ToListAsync();

                var listaCarritoDto = new List<CarritoDetalleDdto>();

                foreach (var libro in carritoSessionDetalle)
                {
                        var response = await libroService.
                        getLibro(new System.Guid(libro.ProductoSeleccionado));
                        var responseAutor = await autorService.getAutor(response.Libro.AutorLibro);
                        if (response.resultado && responseAutor.resultado)
                        {
                            var objetoLibro = response.Libro;
                            var objetoAutor = responseAutor.Autor;
                            var carritoDetalle = new CarritoDetalleDdto
                            {
                                TituloLibro = objetoLibro.Titulo,
                                FechaPublicacion = objetoLibro.FechaPublicacion,
                                LibroId = objetoLibro.LibreriaMaterialId,
                                Img = objetoLibro.Img,
                                Precio = objetoLibro.Precio,
                                CuponId = objetoLibro.CuponId,
                                AutorLibro = objetoAutor.nombre + " " + objetoAutor.apellido
                            };
                            listaCarritoDto.Add(carritoDetalle);
                        }
                }
                var carritoSessionDto = new CarritoDto
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion,
                    ListaDeProductos = listaCarritoDto
                };
                return carritoSessionDto;
            }
        }
    }

}
