using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.BusinessLogic.EstrategiaSonido;
using preguntaods.BusinessLogic.Services;
using System;
using System.Collections.Generic;

namespace preguntaods.Presentacion.ViewModels
{
    [Activity(Label = "", Theme = "@style/HiddenTitleTheme")]
    public class RankingViewModel : AppCompatActivity
    {
        private Sonido sonido;
        private GridLayout rankingGridLayout;
        private Facade fachada;
        private const int numFilas = 10;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaRanking);
            fachada = new Facade();
            rankingGridLayout = FindViewById<GridLayout>(Resource.Id.rankingGridLayout);

            sonido = new Sonido();
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            var atras = FindViewById<ImageButton>(Resource.Id.buttonAtras);
            if (atras != null) { atras.Click += Atras; }

            var usuarios = await fachada.GetOrderedUsers(numFilas);
            List<string> posiciones = new List<string>();
            int i = 1;
            while (i <= numFilas)
            {
                posiciones.Add(i.ToString() + ".");
                i++;
            }
            //for (int j = 0; j < posiciones.Count; j++)
            //{
            //    var textView = new TextView(this);
            //    textView.Text = posiciones[j];
            //    rankingGridLayout.AddView(textView);
            //
            //    var textViewNombre = new TextView(this);
            //    textViewNombre.Text = usuarios[j].Nombre;
            //    rankingGridLayout.AddView(textViewNombre);
            //
            //    var textViewPuntos = new TextView(this);
            //    textViewPuntos.Text = usuarios[j].Puntos.ToString();
            //    rankingGridLayout.AddView(textViewPuntos);
            //}

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

        private void Atras(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            var i = new Intent(this, typeof(MenuViewModel));
            StartActivity(i);
        }
    }
}