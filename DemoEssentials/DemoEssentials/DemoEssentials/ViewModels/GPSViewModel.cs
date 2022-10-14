using DemoEssentials.Services;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DemoEssentials.ViewModels
{
    public class GPSViewModel : BaseViewModel
    {
        private double latitud;

        public double Latitud
        {
            get { return latitud; }
            set { SetProperty(ref latitud, value); }
        }

        private double longitud;

        public double Longitud
        {
            get { return longitud; }
            set { SetProperty(ref longitud, value); }
        }

        private string direccion;

        public string Direccion
        {
            get { return direccion; }
            set { SetProperty(ref direccion, value); }
        }

        CancellationTokenSource cts;

        public ICommand ComandoObtenerUbicacionActual { get; private set; }
        public ICommand ComandoMostrarMapaExterno { get; private set; }
        public ICommand ComandoObtenerDireccionDeUbicacion { get; private set; }
        public ICommand ComandoObtenerUbicacionDeDireccion { get; private set; }
        public ICommand ComandoLiberarToken { get; private set; }

        private async Task ObtenerUbicacionActual()
        {
            var ubicacion = await ServicioGPS.ObtenerUbicacionActual(cts);

            if (ubicacion != null)
            {
                Latitud = ubicacion.Latitude;
                Longitud = ubicacion.Longitude;
            }
        }

        private async Task MostrarMapaExterno()
        {
            var opciones = new MapLaunchOptions()
            {
                Name = "Ubicacion actual",
                NavigationMode = NavigationMode.None
            };

            await Map.OpenAsync(Latitud, Longitud, opciones);
        }

        private async Task ObtenerDireccionDeUbicacion()
        {
            var ubicacion = await ServicioGPS.ObtenerDireccionDeUbicacion(Latitud, Longitud);

            if (ubicacion != null)
            {
                Direccion =
                    $"AdminArea:       {ubicacion.AdminArea}\n" +
                    $"CountryCode:     {ubicacion.CountryCode}\n" +
                    $"CountryName:     {ubicacion.CountryName}\n" +
                    $"FeatureName:     {ubicacion.FeatureName}\n" +
                    $"Locality:        {ubicacion.Locality}\n" +
                    $"PostalCode:      {ubicacion.PostalCode}\n" +
                    $"SubAdminArea:    {ubicacion.SubAdminArea}\n" +
                    $"SubLocality:     {ubicacion.SubLocality}\n" +
                    $"SubThoroughfare: {ubicacion.SubThoroughfare}\n" +
                    $"Thoroughfare:    {ubicacion.Thoroughfare}\n";
            }
        }

        private async Task ObtenerUbicacionDeDireccion()
        {
            var ubicacion = await ServicioGPS.ObtenerUbicacionDeDireccion(Direccion);

            if (ubicacion != null)
            {
                Latitud = ubicacion.Latitude;
                Longitud = ubicacion.Longitude;
            }
        }

        private void LiberarToken()
        {
            ServicioGPS.LiberarToken(cts);
        }

        public GPSViewModel()
        {
            ComandoObtenerUbicacionActual = new Command(async () => await ObtenerUbicacionActual());
            ComandoMostrarMapaExterno = new Command(async () => await MostrarMapaExterno());
            ComandoObtenerUbicacionDeDireccion = new Command(async () => await ObtenerUbicacionDeDireccion());
            ComandoObtenerDireccionDeUbicacion = new Command(async () => await ObtenerDireccionDeUbicacion());
            ComandoLiberarToken = new Command(LiberarToken);
        }
    }
}
