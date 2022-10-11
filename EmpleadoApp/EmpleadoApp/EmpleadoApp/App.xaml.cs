using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmpleadoApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Paginas.PaginaMensaje());
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
