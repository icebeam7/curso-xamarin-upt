using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmpleadoApp.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaLogin : ContentPage
    {
        public PaginaLogin()
        {
            InitializeComponent();
        }

        private async void BotonIngresar_Clicked(object sender, EventArgs e)
        {
            string usuario = EntryUsuario.Text;
            string password = EntryPassword.Text;

            if (usuario == "user" && password == "pass")
                await Navigation.PushAsync(new PaginaDatos());
        }
    }
}