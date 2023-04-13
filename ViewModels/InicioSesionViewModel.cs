using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.Entities;
using preguntaods.Services;
using System;


namespace preguntaods
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class InicioSesionViewModel : AppCompatActivity
    {
        private Button iniciarSesion;
        private EditText correo;
        private EditText password;
        private TextView error;
        private Sonido sonido;
        private Facade fachada;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.vistaInicioSesion);
            sonido = new Sonido();

            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);

            // Create your application here
            correo = FindViewById<EditText>(Resource.Id.correo);
            password = FindViewById<EditText>(Resource.Id.contraseña);

            iniciarSesion = FindViewById<Button>(Resource.Id.inicioSesion);
            iniciarSesion.Click += IniciarSesion_Click;

            error = FindViewById<TextView>(Resource.Id.error);

            ImageButton atras = FindViewById<ImageButton>(Resource.Id.atras);
            atras.Click += SaltarMenu;

            TextView registrar = FindViewById<TextView>(Resource.Id.registrar);
            registrar.Click += NavigateRegistro;
        }

        private void SaltarMenu(object sender, EventArgs e)
        {
            sonido.EjecutarSonido();

            Intent i = new Intent(this, typeof(MenuViewModel));
            StartActivity(i);
        }

        private void NavigateRegistro(object sender, EventArgs e)
        {
            sonido.EjecutarSonido();

            Intent i = new Intent(this, typeof(RegistroViewModel));
            StartActivity(i);
        }

        private async void IniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                sonido.EjecutarSonido();

                await fachada.LoginAsync(correo.Text, password.Text);

                // inicia sesion
                Intent i = new Intent(this, typeof(MenuViewModel));
                StartActivity(i);
            }
            catch (Exception)
            {
                error.Text = "Correo electrónico o contraseña incorrecta";
            }
        }
    }
}