using System.Net.Http;
using System.Text.Json;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;

using ProductosApp.Models;
using ProductosApp.Helpers;

namespace ProductosApp.Services
{
    public static class ServicioProductos
    {
        private static HttpClient CrearCliente(string url)
        {
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return cliente;
        }

        private static HttpClient cliente = CrearCliente(Constantes.ProductosUrl);

        public static async Task<List<Producto>> Obtener()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var respuesta = await cliente.GetAsync(Constantes.ProductosUrl);
                
                if (respuesta.IsSuccessStatusCode)
                {
                    var contenido = await respuesta.Content.ReadAsStringAsync();
                    var lista = JsonSerializer.Deserialize<List<Producto>>(contenido);
                    return lista;
                }
            }

            return default;
        }
    }
}
