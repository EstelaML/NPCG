using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.Services;
using System;


namespace preguntaods
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class InicioSesion : AppCompatActivity
    {
        private Button iniciarSesion;
        private EditText correo;
        private EditText password;
        private TextView error;
        private Facade fachada;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.vistaInicioSesion);
            fachada = new Facade();

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
            fachada.EjecutarSonido(this, new EstrategiaSonidoClick());

            Intent i = new Intent(this, typeof(Menu));
            StartActivity(i);
        }

        private void NavigateRegistro(object sender, EventArgs e)
        {
            fachada.EjecutarSonido(this, new EstrategiaSonidoClick());

            Intent i = new Intent(this, typeof(Registro));
            StartActivity(i);
        }

        private async void IniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                fachada.EjecutarSonido(this, new EstrategiaSonidoClick());

                await fachada.LoginAsync(correo.Text, password.Text);

                // inicia sesion
                Intent i = new Intent(this, typeof(Menu));
                StartActivity(i);
            }
            catch (Exception)
            {
                error.Text = "Correo electrónico o contraseña incorrecta";
            }
        }
    }
}