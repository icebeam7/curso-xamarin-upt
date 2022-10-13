using System.Windows.Input;
using System.Threading.Tasks;

using ProductosApp.Models;
using ProductosApp.Views;

using Xamarin.Forms;

namespace ProductosApp.ViewModels
{
    public class DetalleProductoViewModel : BaseViewModel
    {
        private Producto producto;

        public Producto Producto
        {
            get { return producto; }
            set { SetProperty(ref producto, value); }
        }

        public ICommand ComandoEditarProducto { get; private set; }

        private async Task EditarProducto()
        {
            var vm = new FormularioProductoViewModel(Producto);
            var pagina = new FormularioProductoView(vm);

            await App.Current.MainPage.Navigation.PushAsync(pagina);
        }

        public DetalleProductoViewModel(Producto producto)
        {
            this.producto = producto;
            ComandoEditarProducto = new Command(async () => await EditarProducto());
        }
    }
}
