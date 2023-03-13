using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Microsoft.EntityFrameworkCore;
using pruebasEF.Entities;
using pruebasEF.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pruebasEF
{
    [Activity(Label = "inicioSesion")]
    public class inicioSesion : AppCompatActivity
    {
        private Button atras;
        private Button iniciarSesion;
        private EditText username;
        private EditText password;
        private TextView error;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState); 
            SetContentView(Resource.Layout.inicioSesion);
            // Create your application here
            username = FindViewById<EditText>(Resource.Id.nombreUsuario);
            password = FindViewById<EditText>(Resource.Id.contraseña);

            iniciarSesion = FindViewById<Button>(Resource.Id.inicioSesion);
            iniciarSesion.Click += IniciarSesion_Click;
            error = FindViewById<TextView>(Resource.Id.error);
            ImageButton atras = FindViewById<ImageButton>(Resource.Id.atras);
            atras.Click += Atras;
        }

        private void Atras(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(MainActivity));
            StartActivity(i);
        }

        private void IniciarSesion_Click(object sender, EventArgs e)
        {
            if (username.Text != null && password.Text != null) {
                    using (var bd = new SupabaseContext())
                    {
                    Usuario user = bd.User.FirstOrDefault(u => u.nombre == username.Text);
                    if (user != null)
                    {
                        if (user.contraseña == password.Text)
                        {
                            // inicia sesion
                            error.Text = "Todo ha ido correctamente";
                        }
                        else 
                        {
                            // contraseña incorrecta    
                            error.Text = "Contraseña Incorrecta";
                        }
                        
                    }
                    else 
                    {
                        // no existe, ofrecer registro
                        error.Text = "Usuario no existente";
                    }
                    
                    }
            }
        }
    }
}