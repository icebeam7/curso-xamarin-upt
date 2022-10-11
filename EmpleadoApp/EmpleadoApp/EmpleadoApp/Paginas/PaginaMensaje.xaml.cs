using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Essentials;

namespace EmpleadoApp.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaMensaje : ContentPage
    {
        public PaginaMensaje()
        {
            InitializeComponent();
        }

        private async void BotonEnviarSms_Clicked(object sender, EventArgs e)
        {
            var mensaje = EditorMensaje.Text;
            var destinatario = EntryDestinatario.Text;

            var mensajeSms = new SmsMessage()
            {
                Body = mensaje,
                Recipients = new List<string>() { destinatario }
            };

            await Sms.ComposeAsync(mensajeSms);
        }

        private async void BotonEnviarCorreo_Clicked(object sender, EventArgs e)
        {
            var mensaje = EditorMensaje.Text;
            var destinatario = EntryDestinatario.Text;
            var asunto = EntryAsunto.Text;

            var mensajeEmail = new EmailMessage()
            {
                Subject = asunto,
                To = new List<string>() { destinatario },
                Body = mensaje
            };

            await Email.ComposeAsync(mensajeEmail);
        }
    }
}