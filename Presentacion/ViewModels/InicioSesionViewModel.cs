using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.BusinessLogic.EstrategiaSonido;
using preguntaods.BusinessLogic.Services;
using System;

namespace preguntaods.Presentacion.ViewModels
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class InicioSesionViewModel : AppCompatActivity
    {
        private Button iniciarSesion;
        private EditText correo;
        private EditText password;
        private TextView error;
        private TextView registrar;
        private Facade fachada;
        private Sonido sonido;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.vistaInicioSesion);
            fachada = new Facade();

            sonido = new Sonido();
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);

            UserDialogs.Init(this);

            // Create your application here
            correo = FindViewById<EditText>(Resource.Id.correo);
            password = FindViewById<EditText>(Resource.Id.contraseña);

            iniciarSesion = FindViewById<Button>(Resource.Id.inicioSesion);
            if (iniciarSesion != null) iniciarSesion.Click += IniciarSesion_Click;

            error = FindViewById<TextView>(Resource.Id.error);

            registrar = FindViewById<TextView>(Resource.Id.registrar);
            if (registrar != null) registrar.Click += NavigateRegistro;
        }

        private void NavigateRegistro(object sender, EventArgs e)
        {
            sonido.EjecutarSonido();

            var i = new Intent(this, typeof(RegistroViewModel));
            StartActivity(i);
        }

        private async void IniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
                sonido.EjecutarSonido();

                UserDialogs.Instance.ShowLoading("Comprobando...", MaskType.Clear);

                await fachada.LoginAsync(correo.Text, password.Text);

                UserDialogs.Instance.HideLoading();

                // inicia sesion
                Intent i = new Intent(this, typeof(MenuViewModel));
                StartActivity(i);
            }
            catch (Exception)
            {
                UserDialogs.Instance.HideLoading();

                await fachada.LogoutAsync();
                error.Text = "Correo electrónico o contraseña incorrecta";
            }
        }
    }
}