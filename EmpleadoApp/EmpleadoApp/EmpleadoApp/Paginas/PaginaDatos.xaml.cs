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
    public partial class PaginaDatos : ContentPage
    {
        public PaginaDatos()
        {
            InitializeComponent();
        }

        private async void BotonObtenerDatos_Clicked(object sender, EventArgs e)
        {
            string nombre = EntryNombre.Text;

            DateTime fechaNacimiento = PickerFechaNacimiento.Date;
            double edad = Math.Floor(DateTime.Now.Subtract(fechaNacimiento).TotalDays / 365);

            string pais = PickerPais.SelectedItem.ToString();
            string contintente = string.Empty;

            switch (pais)
            {
                case "Mexico":
                case "Peru":
                    contintente = "America";
                    break;
                case "Republica Checa":
                    contintente = "Europa";
                    break;
                case "Corea del Sur":
                    contintente = "Asia";
                    break;
                default:
                    contintente = "Desconocido";
                    break;
            }

            string mensaje = $"Hola {nombre}, tu edad es {edad} y eres del continente {contintente}";

            await DisplayAlert("Datos", mensaje, "OK");
        }
    }
}