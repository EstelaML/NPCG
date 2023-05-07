using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.BusinessLogic.EstrategiaSonido;
using preguntaods.BusinessLogic.Services;
using System;
using System.Threading.Tasks;

namespace preguntaods.Presentacion.ViewModels
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

            var perfil = FindViewById<Button>(Resource.Id.perfil);
            if (perfil != null) perfil.Click += Perfil_Click;

            var ranking = FindViewById<Button>(Resource.Id.ranking);
            if (ranking != null) ranking.Click += Ranking_Click;

            var nuevaPartida = FindViewById<Button>(Resource.Id.nuevaPartida);
            if (nuevaPartida != null) nuevaPartida.Click += NuevaPartida_Click;
        }

        protected override async void OnDestroy()
        {
            base.OnDestroy();

            System.Console.WriteLine("\n OnDestroy ---------------------------------------------------------------------------- \n --------------");
            await fachada.GuardarTiempo();
            // Realizar alguna acción aquí antes de que la aplicación se cierre
            // Guardar tiempo en bd
            System.Console.WriteLine("\n OnDestroy ----------------------------------------------------------------- \n -------------------------");
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
                        var i = new Android.Content.Intent(this, typeof(RankingViewModel));
                        StartActivity(i);
                        break;
                    }
                case Resource.Id.menuItem4:
                    {
                        _ = fachada.LogoutAsync();

                        var i = new Android.Content.Intent(this, typeof(InicioSesionViewModel));
                        StartActivity(i);

                        break;
                    }
            }

            return base.OnOptionsItemSelected(item);
        }

        private void Perfil_Click(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            var i = new Android.Content.Intent(this, typeof(PerfilViewModel));
            StartActivity(i);
        }

        private void Ranking_Click(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            var i = new Android.Content.Intent(this, typeof(RankingViewModel));
            StartActivity(i);
        }

        private void NuevaPartida_Click(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            var i = new Android.Content.Intent(this, typeof(SeleccionRetoViewModel));
            StartActivity(i);
        }
    }
}