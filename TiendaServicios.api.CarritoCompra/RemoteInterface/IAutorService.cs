using TiendaServicios.api.CarritoCompra.RemoteModel;

namespace TiendaServicios.api.CarritoCompra.RemoteInterface
{
    public interface IAutorService
    {
        Task<(bool resultado, AutorRemote Autor, string ErrorMessage)> getAutor(Guid? AutorId);
    }
}
