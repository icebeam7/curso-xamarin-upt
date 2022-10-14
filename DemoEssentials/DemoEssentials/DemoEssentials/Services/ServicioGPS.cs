using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Essentials;

namespace DemoEssentials.Services
{
    public class ServicioGPS
    {
        public static async Task<Location> ObtenerUbicacionActual(CancellationTokenSource cts)
        {
            try
            {
                cts = new CancellationTokenSource();

                var solicitud = new GeolocationRequest(
                    GeolocationAccuracy.Medium,
                    TimeSpan.FromSeconds(10));

                var ubicacion = await Geolocation.GetLocationAsync(solicitud, cts.Token);
                return ubicacion;
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

            return default;
        }

        public static async Task<Location> ObtenerUbicacionDeDireccion(string direccion)
        {
            try
            {
                var ubicaciones = await Geocoding.GetLocationsAsync(direccion);
                return ubicaciones?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
            }

            return default;
        }

        public static async Task<Placemark> ObtenerDireccionDeUbicacion(double latitud, double longitud)
        {
            try
            {
                var direcciones = await Geocoding.GetPlacemarksAsync(latitud, longitud);
                return direcciones?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
            }

            return default;
        }

        public static void LiberarToken(CancellationTokenSource cts)
        {
            if (cts != null && !cts.IsCancellationRequested)
                cts.Cancel();
        }
    }
}
