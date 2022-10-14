using DemoEssentials.Models;
using Xamarin.Essentials;

namespace DemoEssentials.ViewModels
{
    public class DispositivoViewModel : BaseViewModel
    {
        public string Modelo => DeviceInfo.Model; 
        public string Fabricante => DeviceInfo.Manufacturer; 
        public string Nombre => DeviceInfo.Name; 
        public string Version => DeviceInfo.VersionString; 
        public string Plataforma => DeviceInfo.Platform.ToString(); 
        public string Dispositivo => DeviceInfo.Idiom.ToString(); 
        public string Tipo => DeviceInfo.DeviceType.ToString();

        private DatosDispositivo datosDispositivo;

        public DatosDispositivo DatosDispositivo
        {
            get { return datosDispositivo; }
            set { SetProperty(ref datosDispositivo, value); }
        }

        public DispositivoViewModel()
        {
            var info = DeviceDisplay.MainDisplayInfo;

            DatosDispositivo = new DatosDispositivo()
            {
                Orientacion = info.Orientation,
                Rotacion = info.Rotation,
                Alto = info.Height,
                Ancho = info.Width
            };

            DeviceDisplay.MainDisplayInfoChanged -= DeviceDisplay_MainDisplayInfoChanged;
            DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
        }

        private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            var info = e.DisplayInfo;

            DatosDispositivo = new DatosDispositivo()
            {
                Orientacion = info.Orientation,
                Rotacion = info.Rotation,
                Alto = info.Height,
                Ancho = info.Width
            };
        }
    }
}
