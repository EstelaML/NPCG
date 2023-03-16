using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using pruebasEF.Entities;
using pruebasEF.Persistencia;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace pruebasEF
{
    [Activity(Label = "@string/app_name", Theme = "@style/HiddenTitleTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Button registro = FindViewById<Button>(Resource.Id.registroB);
            registro.Click += Registro_Click;

            Button partida = FindViewById<Button>(Resource.Id.partidaB);
            partida.Click += Partida_Click;

            Button inicio = FindViewById<Button>(Resource.Id.sesionB);
            inicio.Click += inicioSesion;
        }

        private void Partida_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(pregunta));
            StartActivity(i);
        }

        private void inicioSesion(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(inicioSesion));
            StartActivity(i);
        }

        private void Registro_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(registro));
            StartActivity(i);
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void probarAsync() {
            using (var bd = new SupabaseContext()) {
                Usuario e = new Usuario { nombre = "probando", email = "naa", contraseña = "nada" };
                bd.User.Add(e);
                bd.SaveChanges();


                var lista = bd.User.ToList();
                
            }
        }

    }
}