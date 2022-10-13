using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

using ProductosApp.Views;
using ProductosApp.Models;
using ProductosApp.Services;

namespace ProductosApp.ViewModels
{
    public class ListaProductosViewModel : BaseViewModel
    {
        private List<Producto> listaProductos;

        public ObservableCollection<Producto> ColeccionProductos { get; } = 
            new ObservableCollection<Producto>();

        private Producto productoSeleccionado;

        public Producto ProductoSeleccionado
        {
            get { return productoSeleccionado; }
            set { SetProperty(ref productoSeleccionado, value); }
        }

        public ICommand ComandoCargarDatos { get; private set; }
        public ICommand ComandoVerDetalleProducto { get; private set; }
        public ICommand ComandoAgregarNuevoProducto { get; private set; }

        private async Task CargarDatos()
        {
            //if (ColeccionProductos.Count == 0)
            {
                IsBusy = true;

                //listaProductos = await ServicioProductos.Obtener();
                listaProductos = await App.ContextoBDLocal.Obtener<Producto>();

                ColeccionProductos.Clear();

                foreach (var p in listaProductos)
                {
                    ColeccionProductos.Add(p);
                }

                IsBusy = false;
            }
        }

        private async Task VerDetalle()
        {
            if (productoSeleccionado != null)
            {
                var vm = new DetalleProductoViewModel(productoSeleccionado);
                var pagina = new DetalleProductoView(vm);

                await App.Current.MainPage.Navigation.PushAsync(pagina);
            }
        }

        private async Task AgregarNuevoProducto()
        {
            var nuevo = new Producto();
            var vm = new FormularioProductoViewModel(nuevo);
            var pagina = new FormularioProductoView(vm);

            await App.Current.MainPage.Navigation.PushAsync(pagina);
        }

        public ListaProductosViewModel()
        {
            ProductoSeleccionado = null;
            ComandoCargarDatos = new Command(async () => await CargarDatos());
            ComandoVerDetalleProducto = new Command(async () => await VerDetalle());
            ComandoAgregarNuevoProducto = new Command(async () => await AgregarNuevoProducto());
        }
    }
}
