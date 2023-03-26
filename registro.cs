using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.Entities;
using preguntaods.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Hardware.Camera2;

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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.registro);

            usernameCorrect = false;
            passwordCorrect = false;
            emailCorrect = false;

            error = FindViewById<TextView>(Resource.Id.error);
            username = FindViewById<EditText>(Resource.Id.nombreUsuario);
            password = FindViewById<EditText>(Resource.Id.contraseña);
            password2 = FindViewById<EditText>(Resource.Id.contraseña2);
            email = FindViewById<EditText>(Resource.Id.correo);

            ImageButton atras = FindViewById<ImageButton>(Resource.Id.button1);
            atras.Click += Atras;
            registroB = FindViewById<Button>(Resource.Id.registroB);
            registroB.Click += Registrar;
            password2.TextChanged += Password_Click;
            email.TextChanged += Email_TextChanged;
            username.TextChanged += Username_TextChanged;
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

        private void Registrar(object sender, EventArgs e)
        {
            if (usernameCorrect && passwordCorrect && emailCorrect) {
                if (!email.Text.Contains("@gmail.com")) { error.Text = "Elija un correo electrónico válido"; emailCorrect = false; return; }
                if (password.Text != password2.Text) { error.Text = "Las contraseñas no coinciden"; passwordCorrect = false; return; }
                using (var bd = new SupabaseContext())
                {
                    if (bd.User.Any(u => u.nombre == username.Text))
                    {
                        error.Text = "Ese nombre de usuario ya existe";
                        usernameCorrect = false; return;
                    }
                }

                using (var bd = new SupabaseContext())
                {
                    Usuario almendro = new Usuario { nombre = username.Text, email = email.Text, contraseña = password.Text };
                    bd.User.Add(almendro);
                    bd.SaveChanges();
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