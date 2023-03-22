using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Microsoft.EntityFrameworkCore;
using preguntaods.Entities;
using preguntaods.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace preguntaods
{
    [Activity(Label = "@string/app_name", Theme = "@style/HiddenTitleTheme", MainLauncher = true)]
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
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

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
            Intent i = new Intent(this, typeof(menu));
            StartActivity(i);
        }


        private async void IniciarSesion_Click(object sender, EventArgs e)
        {
            if (username.Text != null && password.Text != null) {
                    using (var bd = new SupabaseContext())
                    {
                    var usuarioRepositorio = new UsuarioRepositorio(bd);
                    // usando repositorio 
                    Usuario user = usuarioRepositorio.GetByUsername(username.Text);

                    // Usando entity framework sin ningun tipo de servicio
                    //Usuario user = bd.User.FirstOrDefault(u => u.nombre == username.Text);
                    if (user != null)
                    {
                        if (user.contraseña == password.Text)
                        {
                            // inicia sesion
                            Intent i = new Intent(this, typeof(menu));
                            StartActivity(i);
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