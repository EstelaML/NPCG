using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.Persistencia;
using System;

namespace preguntaods
{
    [Activity(Label = "Activity2")]
    public class Registro : AppCompatActivity
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

        private SingletonConexion conexion;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.registro);
            conexion = SingletonConexion.getInstance();

            usernameCorrect = false;
            passwordCorrect = false;
            emailCorrect = false;

            ImageButton atras = FindViewById<ImageButton>(Resource.Id.button1);
            atras.Click += Atras;

            username = FindViewById<EditText>(Resource.Id.nombreUsuario);
            username.TextChanged += Username_TextChanged;

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

        private void Username_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (username.Text != null) { usernameCorrect = true; }
            error.Text = "";
            
        }

        private async void Registrar(object sender, EventArgs e)
        {
            if (usernameCorrect && passwordCorrect && emailCorrect) {
                if (!email.Text.Contains("@gmail.com")) { error.Text = "Elija un correo electrónico válido"; emailCorrect = false; return; }
                if (password.Text != password2.Text) { error.Text = "Las contraseñas no coinciden"; passwordCorrect = false; return; }

                    try
                    {
                        var session = await conexion.cliente.Auth.SignUp(email.Text, password.Text);

                        // se registra
                        Intent i = new Intent(this, typeof(Menu));
                        StartActivity(i);
                    }
                    catch (Exception ex)
                    {
                        error.Text = "Ese nombre de usuario ya existe";
                    }
            }
        }

        private void Atras(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(InicioSesion));
            StartActivity(i);
        }
    }
}