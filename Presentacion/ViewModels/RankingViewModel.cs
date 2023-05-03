using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Java.Beans;
using preguntaods.BusinessLogic.EstrategiaSonido;
using preguntaods.BusinessLogic.Services;
using preguntaods.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace preguntaods.Presentacion.ViewModels
{
    [Activity(Label = "", Theme = "@style/HiddenTitleTheme")]
    public class RankingViewModel : AppCompatActivity
    {
        private Sonido sonido;
        private GridLayout rankingGridLayout;
        private Facade fachada;
        private TextView textAnimo;
        private const int numFilas = 10;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaRanking);
            fachada = new Facade();
            rankingGridLayout = FindViewById<GridLayout>(Resource.Id.rankingGridLayout);
            textAnimo = FindViewById<TextView>(Resource.Id.textAnimo);

            sonido = new Sonido();
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            var atras = FindViewById<ImageButton>(Resource.Id.buttonAtras);
            if (atras != null) { atras.Click += Atras; }

            var usuarios = await fachada.GetOrderedUsers(numFilas);
            CrearRanking(usuarios);
            MensajeAnimo(usuarios);
        }
        private void CrearRanking(List<Usuario> usuarios) {
            List<string> posiciones = new List<string>();
            int i = 1;
            while (i <= numFilas)
            {
                posiciones.Add(i.ToString() + ".");
                i++;
            }

            // Agregar la fila de encabezado al GridLayout
            rankingGridLayout.AddView(new TextView(this) { Text = "Posición", TextAlignment = TextAlignment.Center });
            rankingGridLayout.AddView(new TextView(this) { Text = "Nombre", TextAlignment = TextAlignment.Center });
            rankingGridLayout.AddView(new TextView(this) { Text = "Puntos", TextAlignment = TextAlignment.Center });

            // Agregar los datos al GridLayout
            for (int j = 0; j < numFilas; j++)
            {
                rankingGridLayout.AddView(new TextView(this) { Text = posiciones[j], TextAlignment = TextAlignment.Center });
                if (j < usuarios.Count)
                {
                    rankingGridLayout.AddView(new TextView(this) { Text = usuarios[j].Nombre, TextAlignment = TextAlignment.Center });
                    rankingGridLayout.AddView(new TextView(this) { Text = usuarios[j].Puntos.ToString(), TextAlignment = TextAlignment.Center });
                }
                else
                {
                    rankingGridLayout.AddView(new TextView(this) { Text = "---", TextAlignment = TextAlignment.Center });
                    rankingGridLayout.AddView(new TextView(this) { Text = "---", TextAlignment = TextAlignment.Center });
                }
            }
        }

        private async void MensajeAnimo(List<Usuario> usuarios) {
            var usuarioLogged = await fachada.GetUsuarioLogged();
            bool estaEnLaLista = usuarios.Any(u => u.Nombre == usuarioLogged.Nombre);
            int indice = usuarios.FindIndex(u => u.Nombre == usuarioLogged.Nombre);
            if (estaEnLaLista && indice != -1)
            {
                int pos = indice + 1;
                textAnimo.Text = "Eres el Top " + pos + ". ¡ENHORABUENA!";
            }
            else
            {
                textAnimo.Text = "Todavía puedes seguir jugando y sumar puntos para llegar a la cima.";
            }
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