using System;
using System.IO;

using Xamarin.Forms;

using ProductosApp.Helpers;
using ProductosApp.Services;

namespace ProductosApp
{
    public partial class App : Application
    {
        private static ServicioBaseDatosLocal contextoBDLocal;

        public static ServicioBaseDatosLocal ContextoBDLocal
        {
            get
            {
                if (contextoBDLocal == null)
                {
                    var rutaBD = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                        Constantes.ArchivoBaseDatosLocal);

                    contextoBDLocal = new ServicioBaseDatosLocal(rutaBD);
                }

                return contextoBDLocal;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.ListaProductosView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
