using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using pruebasEF.Entities;
using pruebasEF.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pruebasEF.Persistencia;

namespace pruebasEF
{
    [Activity(Label = "Activity2")]
    public class registro : AppCompatActivity
    {
        private EditText username;
        private EditText password;
        private EditText email;
        private EditText password2;
        private bool usernameCorrect;
        private bool passwordCorrect;
        private bool emailCorrect;
        private Button registroB;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.registro);

            usernameCorrect = false;
            passwordCorrect = false;
            emailCorrect = false;

            username = FindViewById<EditText>(Resource.Id.nombreUsuario);
            password = FindViewById<EditText>(Resource.Id.contraseña);
            password2 = FindViewById<EditText>(Resource.Id.contraseña2);
            email = FindViewById<EditText>(Resource.Id.correo);

            Button atras = FindViewById<Button>(Resource.Id.button1);
            atras.Click += Atras;
            registroB = FindViewById<Button>(Resource.Id.registroB);
            registroB.Click += Registrar;
            password2.TextChanged += Password_Click;
            email.TextChanged += Email_TextChanged;
            username.TextChanged += Username_TextChanged;
        }

        private void Email_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            emailCorrect = (email.Text.Contains("@gmail.com"));
        }

        private void Password_Click(object sender, EventArgs e)
        {
            passwordCorrect = (password.Text == password.Text);
        }

        private void Username_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            usernameCorrect = !(username.Text == null);
        }

        private void Registrar(object sender, EventArgs e)
        {
            if (usernameCorrect && passwordCorrect && emailCorrect) {
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
            Intent i = new Intent(this, typeof(MainActivity));
            StartActivity(i);
        }
    }
}