using ProductosApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductosApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleProductoView : ContentPage
    {
        public DetalleProductoView(DetalleProductoViewModel vm)
        {
            InitializeComponent();

            this.BindingContext = vm;
        }
    }
}