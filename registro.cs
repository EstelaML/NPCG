using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pruebasEF
{
    [Activity(Label = "Activity2")]
    public class registro : AppCompatActivity
    {
        private EditText username;
        private EditText password;
        private EditText email;
        private EditText password2;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.registro);
            
            username = FindViewById<EditText>(Resource.Id.nombreUsuario);
            password = FindViewById<EditText>(Resource.Id.contraseña);
            password2 = FindViewById<EditText>(Resource.Id.contraseña2);
            email = FindViewById<EditText>(Resource.Id.correo);

            Button atras = FindViewById<Button>(Resource.Id.button1);
            atras.Click += Atras;
            Button registro = FindViewById<Button>(Resource.Id.registroB);
            registro.Click += Registrar;
        }

        private void Registrar(object sender, EventArgs e)
        {
            throw new NotImplementedException();


        }

        private void Atras(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(MainActivity));
            StartActivity(i);
        }
    }
}