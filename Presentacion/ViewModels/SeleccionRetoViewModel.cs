using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.BusinessLogic.EstrategiaSonido;
using preguntaods.BusinessLogic.Services;
using System;

namespace preguntaods.Presentacion.ViewModels
{
    [Activity(Label = "", Theme = "@style/AppTheme")]
    public class SeleccionRetoViewModel : AppCompatActivity
    {
        private Facade fachada;
        private Sonido sonido;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaSeleccionReto);
            sonido = new Sonido();
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);

            fachada = new Facade();

            var partida = FindViewById<Button>(Resource.Id.partidaB);
            if (partida != null) partida.Click += Partida_Click;

            var ahorcado = FindViewById<Button>(Resource.Id.ahorcadoB);
            if (ahorcado != null) ahorcado.Click += Ahorcado_Click;

            var fiesta = FindViewById<Button>(Resource.Id.fiestaB);
            if (fiesta != null) fiesta.Click += Fiesta_Click;

            var atras = FindViewById<ImageButton>(Resource.Id.button1);
            if (atras != null) atras.Click += Atras;
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
                        var i = new Intent(this, typeof(RankingViewModel));
                        StartActivity(i);
                        break;
                    }
                case Resource.Id.menuItem4:
                    {
                        _ = fachada.LogoutAsync();

                        var i = new Intent(this, typeof(InicioSesionViewModel));
                        StartActivity(i);

                        break;
                    }
            }

            return base.OnOptionsItemSelected(item);
        }

        private void Partida_Click(object sender, EventArgs e)
        {
            sonido.EjecutarSonido();
            var i = new Intent(this, typeof(VistaPartidaViewModel));
            i.PutExtra("BOTON_PULSADO", "1");
            StartActivity(i);
        }

        private void Ahorcado_Click(object sender, EventArgs e)
        {
            sonido.EjecutarSonido();
            var i = new Intent(this, typeof(VistaPartidaViewModel));
            i.PutExtra("BOTON_PULSADO", "2");
            StartActivity(i);
        }

        private void Fiesta_Click(object sender, EventArgs e)
        {
            sonido.EjecutarSonido();
            var i = new Intent(this, typeof(VistaPartidaViewModel));
            i.PutExtra("BOTON_PULSADO", "5");
            StartActivity(i);
        }

        private void Atras(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            var i = new Intent(this, typeof(MenuViewModel));
            StartActivity(i);
        }
    }
}