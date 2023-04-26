using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.BusinessLogic.EstrategiaSonido;
using preguntaods.BusinessLogic.Services;
using preguntaods.Entities;

namespace preguntaods.ViewModels
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
            Button ahorcado = FindViewById<Button>(Resource.Id.ahorcadoB);
            ahorcado.Click += Ahorcado_Click;
        }

        private void Ahorcado_Click(object sender, EventArgs e)
        {
            sonido.EjecutarSonido();
            Android.Content.Intent i = new Android.Content.Intent(this, typeof(VistaPartidaViewModel));
            i.PutExtra("BOTON_PULSADO", "2");
            StartActivity(i);
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
                        Android.Content.Intent i = new Android.Content.Intent(this, typeof(RankingViewModel));
                        StartActivity(i);
                        break;
                    }
                case Resource.Id.menuItem4:
                    {
                        _ = fachada.LogoutAsync();

                        Android.Content.Intent i = new Android.Content.Intent(this, typeof(InicioSesionViewModel));
                        StartActivity(i);

                        break;
                    }
            }

            return base.OnOptionsItemSelected(item);
        }

        private void Partida_Click(object sender, EventArgs e)
        {
            sonido.EjecutarSonido();
            Android.Content.Intent i = new Android.Content.Intent(this, typeof(VistaPartidaViewModel));
            i.PutExtra("BOTON_PULSADO", "1");
            StartActivity(i);
        }
    }
}