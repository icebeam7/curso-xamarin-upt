using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ProductosApp.ViewModels;

namespace ProductosApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormularioProductoView : ContentPage
    {
        public FormularioProductoView(FormularioProductoViewModel vm)
        {
            InitializeComponent();

            this.BindingContext = vm;
        }
    }
}