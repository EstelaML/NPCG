﻿using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.BusinessLogic.Fachada;
using preguntaods.Entities;
using System;
using System.Threading.Tasks;
using preguntaods.BusinessLogic.Sonidos;

namespace preguntaods.Presentacion.ViewModels
{
    [Activity(Label = "", Theme = "@style/AppTheme")]
    public class SeleccionRetoViewModel : AppCompatActivity
    {
        private Facade fachada;
        private Sonido sonido;

        private Usuario usuario;

        private Button partida;
        private Button ahorcado;
        private Button fiesta;
        private ImageButton atras;

        private int level;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaSeleccionReto);
            sonido = new Sonido();
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);

            fachada = Facade.GetInstance();

            usuario = await fachada.GetUsuarioLogged();

            UserDialogs.Init(this);

            partida = FindViewById<Button>(Resource.Id.partidaB);
            if (partida != null) partida.Click += Partida_Click;

            ahorcado = FindViewById<Button>(Resource.Id.ahorcadoB);
            if (ahorcado != null) ahorcado.Click += Ahorcado_Click;

            if (ahorcado != null) ahorcado.Enabled = false;

            fiesta = FindViewById<Button>(Resource.Id.fiestaB);
            if (fiesta != null) fiesta.Click += Fiesta_Click;

            if (fiesta != null) fiesta.Enabled = false;

            atras = FindViewById<ImageButton>(Resource.Id.button1);
            if (atras != null) atras.Click += Atras;

            UserDialogs.Instance.ShowLoading("Cargando...");

            await InitBoton();

            UserDialogs.Instance.HideLoading();
        }

        public async Task InitBoton()
        {
            level = usuario.Nivel;

            var color = new Color(150, 220, 240);

            await Task.Delay(500);

            if (level == 1)
            {
                ahorcado.Enabled = true;
                ahorcado.SetBackgroundColor(color);
            }
            else if (level > 1)
            {
                ahorcado.Enabled = true;
                ahorcado.SetBackgroundColor(color);

                fiesta.Enabled = true;
                fiesta.SetBackgroundColor(color);
            }
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
                        _ = fachada.LogoutAsync();

                        var i = new Intent(this, typeof(InicioSesionViewModel));
                        StartActivity(i);
                        Finish();

                        break;
                    }
            }

            return base.OnOptionsItemSelected(item);
        }

        private void Partida_Click(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();
            var i = new Intent(this, typeof(VistaPartidaViewModel));
            i.PutExtra("BOTON_PULSADO", "1");
            StartActivity(i);
            Finish();
        }

        private void Ahorcado_Click(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();
            var i = new Intent(this, typeof(VistaPartidaViewModel));
            i.PutExtra("BOTON_PULSADO", "2");
            StartActivity(i);
            Finish();
        }

        private void Fiesta_Click(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();
            var i = new Intent(this, typeof(VistaPartidaViewModel));
            i.PutExtra("BOTON_PULSADO", "5");
            StartActivity(i);
            Finish();
        }

        private void Atras(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            var i = new Intent(this, typeof(MenuViewModel));
            StartActivity(i);
            Finish();
        }
    }
}