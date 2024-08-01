using System.Text.Json;
using TiendaServicios.api.CarritoCompra.RemoteInterface;
using TiendaServicios.api.CarritoCompra.RemoteModel;

namespace TiendaServicios.api.CarritoCompra.RemoteServices
{
    public class AutorService : IAutorService
    {
        private readonly IHttpClientFactory httpClient;
        private readonly ILogger<AutorService> logger;

        public AutorService(IHttpClientFactory httpClient, ILogger<AutorService> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
        }

        public async Task<(bool resultado, AutorRemote Autor, string ErrorMessage)> getAutor(Guid? AutorId)
        {
            try
            {
                var cliente = httpClient.CreateClient("Autor");
                var response = await cliente.GetAsync($"api/Autor/{AutorId}");
                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

                    var respuesta = JsonSerializer.Deserialize<AutorResponse>(contenido, options);
                    var resultado = respuesta?.request;
                    return (true, resultado, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }

        }
    }
}
