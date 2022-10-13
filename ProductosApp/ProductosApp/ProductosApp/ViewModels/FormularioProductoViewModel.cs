using System.IO;
using System.Windows.Input;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Essentials;

using ProductosApp.Models;

namespace ProductosApp.ViewModels
{
    public class FormularioProductoViewModel : BaseViewModel
    {
        private Producto producto;

        public Producto Producto
        {
            get { return producto; }
            set { SetProperty(ref producto, value); }
        }

        public ICommand ComandoTomarFoto { get; private set; }
        public ICommand ComandoGuardar { get; private set; }
        public ICommand ComandoEliminar { get; private set; }

        public bool NoEsNuevo => producto.Id > 0;

        private async Task TomarFoto()
        {
            var fotoCamara = await MediaPicker.CapturePhotoAsync();

            // canceled
            if (fotoCamara == null)
            {
                Producto.PictureUrl = null;
                return;
            }

            var rutaFoto = Path.Combine(
                FileSystem.CacheDirectory, fotoCamara.FileName);

            using (var contenidoFotoCamara = await fotoCamara.OpenReadAsync())
            {
                using (var contenidoFoto = File.OpenWrite(rutaFoto))
                {
                    await contenidoFotoCamara.CopyToAsync(contenidoFoto);
                }
            }

            Producto.PictureUrl = rutaFoto;
            OnPropertyChanged("Producto");
        }

        private async Task Guardar()
        {
            IsBusy = true;

            var op = producto.Id == 0 
                ? await App.ContextoBDLocal.Agregar(Producto)
                : await App.ContextoBDLocal.Actualizar(Producto);

            if (op)
                await App.Current.MainPage.DisplayAlert("¡Excelente!", "¡Dato registrado!", "OK");
            else
                await App.Current.MainPage.DisplayAlert("¡Error!", "¡Dato NO registrado!", "OK");

            await App.Current.MainPage.Navigation.PopToRootAsync();

            IsBusy = false;
        }

        private async Task Eliminar()
        {
            var confirmar = await App.Current.MainPage.DisplayAlert("Confirmar operación", 
                "¿Estás seguro de que deseas ELIMINAR el registro?", "Sí", "No");

            if (confirmar)
            {
                IsBusy = true;

                var op = await App.ContextoBDLocal.Eliminar(Producto);

                if (op)
                    await App.Current.MainPage.DisplayAlert("¡Excelente!", "¡Dato eliminado!", "OK");
                else
                    await App.Current.MainPage.DisplayAlert("¡Error!", "¡Dato NO eliminado!", "OK");

                await App.Current.MainPage.Navigation.PopToRootAsync();

                IsBusy = false;
            }
        }

        public FormularioProductoViewModel(Producto producto)
        {
            Producto = producto;

            ComandoTomarFoto = new Command(async () => await TomarFoto());
            ComandoGuardar = new Command(async () => await Guardar());
            ComandoEliminar = new Command(async () => await Eliminar());
        }
    }
}
