using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ProductosApp.ViewModels;

namespace ProductosApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaProductosView : ContentPage
    {
        ListaProductosViewModel vm;

        public ListaProductosView()
        {
            InitializeComponent();

            vm = new ListaProductosViewModel();
            BindingContext = vm;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await Task.Run(() => vm.ComandoCargarDatos.Execute(null));
            vm.ProductoSeleccionado = null;
        }
    }
}