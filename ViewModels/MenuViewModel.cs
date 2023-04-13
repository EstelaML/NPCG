using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.Entities;
using preguntaods.Services;
using System;

namespace preguntaods
{
    [Activity(Label = "", Theme = "@style/AppTheme")]
    public class MenuViewModel : AppCompatActivity
    {
        private Facade fachada;
        private Sonido sonido;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaMenu);
            sonido = new Sonido();
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);

            fachada = new Facade();

            Button partida = FindViewById<Button>(Resource.Id.partidaB);
            partida.Click += Partida_Click;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.mainMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuItem1:
                    {
                        // add your code  
                        break;
                    }
                case Resource.Id.menuItem2:
                    {
                        // add your code  
                        break;
                    }
                case Resource.Id.menuItem3:
                    {
                        fachada.LogoutAsync();

                        Intent i = new Intent(this, typeof(InicioSesionViewModel));
                        StartActivity(i);
                        
                        break;
                    }
            }

            return base.OnOptionsItemSelected(item);
        }

        private void Partida_Click(object sender, EventArgs e)
        {
            sonido.EjecutarSonido();

            Intent i = new Intent(this, typeof(VistaPartidaViewModel));
            StartActivity(i);
        }
    }
}