using System;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Essentials;

namespace DemoEssentials.ViewModels
{
    public class MensajesViewModel : BaseViewModel
    {
        private Contact contacto;

        public Contact Contacto
        {
            get { return contacto; }
            set { SetProperty(ref contacto, value); }
        }

        private string telefono;

        public string Telefono
        {
            get { return telefono; }
            set { SetProperty(ref telefono, value); }
        }

        private string correo;

        public string Correo
        {
            get { return correo; }
            set { SetProperty(ref correo, value); }
        }

        private string mensaje;

        public string Mensaje
        {
            get { return mensaje; }
            set { SetProperty(ref mensaje, value); }
        }

        private string asunto;

        public string Asunto
        {
            get { return asunto; }
            set { SetProperty(ref asunto, value); }
        }

        private string documento;

        public string Documento
        {
            get { return documento; }
            set { SetProperty(ref documento, value); }
        }

        private ImageSource imagen;

        public ImageSource Imagen
        {
            get { return imagen; }
            set { SetProperty(ref imagen, value); }
        }

        public ICommand ComandoSeleccionarContacto { get; set; }
        public ICommand ComandoLlamar { get; set; }
        public ICommand ComandoEnviarSMS { get; set; }
        public ICommand ComandoEnviarCorreo { get; set; }
        public ICommand ComandoSeleccionarArchivo { get; set; }
        public ICommand ComandoLeerMensaje { get; set; }
        public ICommand ComandoCompartirArchivo { get; set; }
        public ICommand ComandoAbrirArchivo { get; set; }
        public ICommand ComandoEnviarWhatsApp { get; set; }
        public ICommand ComandoTomarFoto { get; set; }

        private async Task SeleccionarComando()
        {
            try
            {
                var contacto = await Contacts.PickContactAsync();

                if (contacto == null)
                    return;

                Contacto = contacto;
                Telefono = Contacto.Phones?.FirstOrDefault()?.PhoneNumber;
                Correo = Contacto.Emails?.FirstOrDefault()?.EmailAddress;
            }
            catch (Exception ex)
            {

            }
        }

        private async Task Llamar()
        {
            try
            {
                PhoneDialer.Open(Telefono);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Info", "Hubo un error.", "OK");
            }
        }

        private async Task EnviarSMS()
        {
            try
            {
                var telefono = new List<string>() { Telefono };

                var mensaje = new SmsMessage(Mensaje, telefono);
                await Sms.ComposeAsync(mensaje);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Info", "Hubo un error.", "OK");
            }
        }

        private async Task EnviarCorreo()
        {
            try
            {
                var correo = new List<string>() { Correo };

                var mensaje = new EmailMessage
                {
                    Subject = Asunto,
                    Body = Mensaje,
                    To = correo
                };

                if (!string.IsNullOrWhiteSpace(Documento))
                    mensaje.Attachments.Add(new EmailAttachment(Documento));

                await Email.ComposeAsync(mensaje);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Info", "Hubo un error.", "OK");
            }
        }

        private async Task<FileResult> ElegirArchivo()
        {
            try
            {
                var opciones = new PickOptions
                {
                    PickerTitle = "Elige un archivo"
                };

                var resultado = await FilePicker.PickAsync(opciones);

                if (resultado != null)
                {
                    Documento = resultado.FullPath;

                    if (resultado.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                        resultado.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                    {
                        var stream = await resultado.OpenReadAsync();
                        Imagen = ImageSource.FromStream(() => stream);
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }

            return null;
        }

        private async Task LeerMensaje()
        {
            if (!string.IsNullOrWhiteSpace(Mensaje))
            {
                var configuracion = new SpeechOptions()
                {
                    Volume = 0.75f,
                    Pitch = 1.0f
                };

                await TextToSpeech.SpeakAsync(Mensaje, configuracion);
            }
        }

        private async Task CompartirArchivo()
        {
            if (!string.IsNullOrWhiteSpace(Documento))
            {
                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Info",
                    File = new ShareFile(Documento)
                });
            }
        }

        private async Task AbrirArchivo()
        {
            if (!string.IsNullOrWhiteSpace(Documento))
            {
                await Launcher.OpenAsync(new OpenFileRequest
                {
                    Title = "Info",
                    File = new ReadOnlyFile(Documento)
                });
            }
        }

        private async Task EnviarWhatsApp()
        {
            try
            {
                var url = $"https://wa.me/{Telefono}?text={Mensaje}";
                await Launcher.OpenAsync(url);
            }
            catch (Exception ex)
            {
            }
        }

        private async Task TomarFoto()
        {
            try
            {
                var foto = await MediaPicker.CapturePhotoAsync();

                var stream = await foto.OpenReadAsync();
                Imagen = ImageSource.FromStream(() => stream);

                await CargarFoto(foto);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Info", "Camera error", "OK");
            }
        }

        private async Task CargarFoto(FileResult foto)
        {
            if (foto == null)
                return;

            var nuevoArchivo = Path.Combine(FileSystem.CacheDirectory,
                foto.FileName);

            using (var stream = await foto.OpenReadAsync())
            {
                using (var copiaFoto = File.OpenWrite(nuevoArchivo))
                {
                    await stream.CopyToAsync(copiaFoto);
                }
            }

            Documento = nuevoArchivo;
        }

        public MensajesViewModel()
        {
            ComandoSeleccionarContacto = new Command(async () => await SeleccionarComando());
            ComandoLlamar = new Command(async () => await Llamar());
            ComandoEnviarSMS = new Command(async () => await EnviarSMS());
            ComandoEnviarCorreo = new Command(async () => await EnviarCorreo());
            ComandoSeleccionarArchivo = new Command(async () => await ElegirArchivo());
            ComandoLeerMensaje = new Command(async () => await LeerMensaje());
            ComandoCompartirArchivo = new Command(async () => await CompartirArchivo());
            ComandoAbrirArchivo = new Command(async () => await AbrirArchivo());
            ComandoEnviarWhatsApp = new Command(async () => await EnviarWhatsApp());
            ComandoTomarFoto = new Command(async () => await TomarFoto());

            Contacto = new Contact();
        }
    }
}
