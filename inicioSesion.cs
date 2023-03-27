using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.Persistencia;
using System;


namespace preguntaods
{
    [Activity(Label = "@string/app_name", Theme = "@style/HiddenTitleTheme", MainLauncher = true)]
    public class InicioSesion : AppCompatActivity
    {
        private Button iniciarSesion;
        private EditText correo;
        private EditText password;
        private TextView error;
        private SingletonConexion conexion;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.inicioSesion);
            conexion = SingletonConexion.getInstance();

            // Create your application here
            correo = FindViewById<EditText>(Resource.Id.correo);
            password = FindViewById<EditText>(Resource.Id.contraseña);

            iniciarSesion = FindViewById<Button>(Resource.Id.inicioSesion);
            iniciarSesion.Click += IniciarSesion_Click;

            error = FindViewById<TextView>(Resource.Id.error);

            ImageButton atras = FindViewById<ImageButton>(Resource.Id.atras);
            atras.Click += Atras;

            TextView registrar = FindViewById<TextView>(Resource.Id.registrar);
            registrar.Click += Registrar;
        }

        private void Atras(object sender, EventArgs e)
        {
            Sonido s = new Sonido();
            Android.Net.Uri uri = Android.Net.Uri.Parse("android.resource://" + PackageName + "/" + Resource.Raw.click);
            s.HacerSonido(this, uri);
            Intent i = new Intent(this, typeof(Menu));
            StartActivity(i);
        }

        private void Registrar(object sender, EventArgs e)
        {
            Sonido s = new Sonido();
            Android.Net.Uri uri = Android.Net.Uri.Parse("android.resource://" + PackageName + "/" + Resource.Raw.click);
            s.HacerSonido(this, uri);
            Intent i = new Intent(this, typeof(Registro));
            StartActivity(i);
        }

        private async void IniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                Sonido s = new Sonido();
                Android.Net.Uri uri = Android.Net.Uri.Parse("android.resource://" + PackageName + "/" + Resource.Raw.click);
                s.HacerSonido(this, uri);
                var session = await conexion.cliente.Auth.SignIn(correo.Text, password.Text);

                // inicia sesion
                Intent i = new Intent(this, typeof(Menu));
                StartActivity(i);
            }
            catch (Exception ex)
            {
                error.Text = "Correo electrónico o contraseña incorrecta";
            }
        }
    }
}