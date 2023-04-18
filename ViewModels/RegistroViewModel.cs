using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using Java.Util;
using preguntaods.Entities;
using preguntaods.Services;
using System;

namespace preguntaods
{
    [Activity(Label = "Activity2")]
    public class RegistroViewModel : AppCompatActivity
    {
        private EditText username;
        private EditText password;
        private EditText email;
        private EditText password2;
        private bool usernameCorrect;
        private bool passwordCorrect;
        private bool emailCorrect;
        private Button registroB;
        private TextView error;

        private Facade fachada;
        private Sonido sonido;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaRegistro);
            sonido = new Sonido();
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);

            fachada = new Facade();

            passwordCorrect = false;
            emailCorrect = false;

            ImageButton atras = FindViewById<ImageButton>(Resource.Id.button1);
            atras.Click += Atras;

            username = FindViewById<EditText>(Resource.Id.nombreUsuario);

            email = FindViewById<EditText>(Resource.Id.correo);
            email.TextChanged += Email_TextChanged;

            password = FindViewById<EditText>(Resource.Id.contraseña);

            password2 = FindViewById<EditText>(Resource.Id.contraseña2);
            password2.TextChanged += Password_Click;

            error = FindViewById<TextView>(Resource.Id.error);

            registroB = FindViewById<Button>(Resource.Id.registroB);
            registroB.Click += Registrar;
        }

        private void Email_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            emailCorrect = true;
            error.Text = "";
        }

        private void Password_Click(object sender, EventArgs e)
        {
            if (password.Text != null && password2.Text != null) { passwordCorrect = true; }
            error.Text = "";
        }

        private async void Registrar(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            if (passwordCorrect && emailCorrect)
            {
                if (!email.Text.Contains("@gmail.com")) { error.Text = "Elija un correo electrónico válido"; emailCorrect = false; return; }
                if (password.Text != password2.Text) { error.Text = "Las contraseñas no coinciden"; passwordCorrect = false; return; }
                try
                {
                    var userAux = await fachada.SignUpAsync(email.Text, password.Text);
                    if (userAux != null)
                    {
                        UUID id = UUID.FromString(userAux.Id);
                        Usuario user = new Usuario(userAux.Id, username.Text, true, 0, 100, null);
                        await fachada.newUsuario(user);

                        // se registra
                        // que inicie sesión
                        Intent i = new Intent(this, typeof(InicioSesionViewModel));
                        StartActivity(i);
                    }
                    else {
                        error.Text = "Ese correo ya está en uso, utiliza otro o inicia sesión";
                    }
                }
                catch (Supabase.Gotrue.RequestException)
                {
                    error.Text = "La contraseña debe estar formada como mínimo de 6 caráceteres";
                }
                catch (Exception)
                {
                    //await fachada.LogoutAsync();
                    error.Text = "Ese correo ya está en uso, utiliza otro o inicia sesión";
                }
            }
        }

        private void Atras(object sender, EventArgs e)
        {
            sonido.EjecutarSonido();

            Intent i = new Intent(this, typeof(InicioSesionViewModel));
            StartActivity(i);
        }
    }
}