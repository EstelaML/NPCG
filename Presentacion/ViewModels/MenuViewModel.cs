﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.BusinessLogic.Fachada;
using System;
using preguntaods.BusinessLogic.Sonidos;

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

            fachada = Facade.GetInstance();

            var perfil = FindViewById<Button>(Resource.Id.perfil);
            if (perfil != null) perfil.Click += Perfil_Click;

            var ranking = FindViewById<Button>(Resource.Id.ranking);
            if (ranking != null) ranking.Click += Ranking_Click;

            var nuevaPartida = FindViewById<Button>(Resource.Id.nuevaPartida);
            if (nuevaPartida != null) nuevaPartida.Click += NuevaPartida_Click;

            fachada.PonerPuntuacionDiaria();
            fachada.PonerPuntuacionSemanal();
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
                        var i = new Intent(this, typeof(ConfiguracionViewModel));
                        StartActivity(i);
                        Finish();

                        break;
                    }
                case Resource.Id.menuItem2:
                    {
                        _ = fachada.LogoutAsync();

                        var i = new Intent(this, typeof(InicioSesionViewModel));
                        StartActivity(i);
                        Finish();

                        break;
                    }
            }

            return base.OnOptionsItemSelected(item);
        }

        private void Perfil_Click(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            var i = new Intent(this, typeof(PerfilViewModel));
            StartActivity(i);
            Finish();
        }

        private void Ranking_Click(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            var i = new Intent(this, typeof(RankingViewModel));
            StartActivity(i);
            Finish();
        }

        private void NuevaPartida_Click(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            var i = new Intent(this, typeof(SeleccionRetoViewModel));
            StartActivity(i);
            Finish();
        }
    }
}