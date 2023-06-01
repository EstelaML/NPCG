using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.BusinessLogic.EstrategiaSonido;
using preguntaods.BusinessLogic.Fachada;
using preguntaods.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preguntaods.Presentacion.ViewModels
{
    [Activity(Label = "", Theme = "@style/HiddenTitleTheme")]
    public class CoberturaODSViewModel : AppCompatActivity
    {
        private Sonido sonido;
        private Facade fachada;
        private Usuario usuario;
        private Estadistica estadisticas;
        private TextView odsTitleTextView;

        private List<ProgressBar> progressBarList;
        private List<TextView> textViewList;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaCoberturaOds);

            UserDialogs.Init(this);

            UserDialogs.Instance.ShowLoading("Cargando Estadísticas...");

            ScrollView scrollView = FindViewById<ScrollView>(Resource.Id.scrollView);
            scrollView.NestedScrollingEnabled = true;

            sonido = new Sonido();
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);

            fachada = Facade.GetInstance();

            usuario = await fachada.GetUsuarioLogged();

            estadisticas = await fachada.PedirEstadisticas(usuario.Uuid);

            #region ProgressBar

            progressBarList = new List<ProgressBar>();

            textViewList = new List<TextView>();

            ProgressBar progressBar1 = FindViewById<ProgressBar>(Resource.Id.ods1ProgressBar);
            progressBarList.Add(progressBar1);
            TextView ods1PercentageTextView = FindViewById<TextView>(Resource.Id.ods1PercentageTextView);
            textViewList.Add(ods1PercentageTextView);

            ProgressBar progressBar2 = FindViewById<ProgressBar>(Resource.Id.ods2ProgressBar);
            progressBarList.Add(progressBar2);
            TextView ods2PercentageTextView = FindViewById<TextView>(Resource.Id.ods2PercentageTextView);
            textViewList.Add(ods2PercentageTextView);

            ProgressBar progressBar3 = FindViewById<ProgressBar>(Resource.Id.ods3ProgressBar);
            progressBarList.Add(progressBar3);
            TextView ods3PercentageTextView = FindViewById<TextView>(Resource.Id.ods3PercentageTextView);
            textViewList.Add(ods3PercentageTextView);

            ProgressBar progressBar4 = FindViewById<ProgressBar>(Resource.Id.ods4ProgressBar);
            progressBarList.Add(progressBar4);
            TextView ods4PercentageTextView = FindViewById<TextView>(Resource.Id.ods4PercentageTextView);
            textViewList.Add(ods4PercentageTextView);

            ProgressBar progressBar5 = FindViewById<ProgressBar>(Resource.Id.ods5ProgressBar);
            progressBarList.Add(progressBar5);
            TextView ods5PercentageTextView = FindViewById<TextView>(Resource.Id.ods5PercentageTextView);
            textViewList.Add(ods5PercentageTextView);

            ProgressBar progressBar6 = FindViewById<ProgressBar>(Resource.Id.ods6ProgressBar);
            progressBarList.Add(progressBar6);
            TextView ods6PercentageTextView = FindViewById<TextView>(Resource.Id.ods6PercentageTextView);
            textViewList.Add(ods6PercentageTextView);

            ProgressBar progressBar7 = FindViewById<ProgressBar>(Resource.Id.ods7ProgressBar);
            progressBarList.Add(progressBar7);
            TextView ods7PercentageTextView = FindViewById<TextView>(Resource.Id.ods7PercentageTextView);
            textViewList.Add(ods7PercentageTextView);

            ProgressBar progressBar8 = FindViewById<ProgressBar>(Resource.Id.ods8ProgressBar);
            progressBarList.Add(progressBar8);
            TextView ods8PercentageTextView = FindViewById<TextView>(Resource.Id.ods8PercentageTextView);
            textViewList.Add(ods8PercentageTextView);

            ProgressBar progressBar9 = FindViewById<ProgressBar>(Resource.Id.ods9ProgressBar);
            progressBarList.Add(progressBar9);
            TextView ods9PercentageTextView = FindViewById<TextView>(Resource.Id.ods9PercentageTextView);
            textViewList.Add(ods9PercentageTextView);

            ProgressBar progressBar10 = FindViewById<ProgressBar>(Resource.Id.ods10ProgressBar);
            progressBarList.Add(progressBar10);
            TextView ods10PercentageTextView = FindViewById<TextView>(Resource.Id.ods10PercentageTextView);
            textViewList.Add(ods10PercentageTextView);

            ProgressBar progressBar11 = FindViewById<ProgressBar>(Resource.Id.ods11ProgressBar);
            progressBarList.Add(progressBar11);
            TextView ods11PercentageTextView = FindViewById<TextView>(Resource.Id.ods11PercentageTextView);
            textViewList.Add(ods11PercentageTextView);

            ProgressBar progressBar12 = FindViewById<ProgressBar>(Resource.Id.ods12ProgressBar);
            progressBarList.Add(progressBar12);
            TextView ods12PercentageTextView = FindViewById<TextView>(Resource.Id.ods12PercentageTextView);
            textViewList.Add(ods12PercentageTextView);

            #endregion ProgressBar

            var atras = FindViewById<ImageButton>(Resource.Id.buttonAtras);
            if (atras != null) atras.Click += Atras;

            await RellenarEstats();

            UserDialogs.Instance.HideLoading();
        }

        private void Atras(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            var i = new Intent(this, typeof(PerfilViewModel));
            StartActivity(i);
            Finish();
        }

        private async Task RellenarEstats()
        {
            var retosAcertados = estadisticas.Aciertos;
            var retosFallados = estadisticas.Fallos;

            for (int i = 1; i < 13; i++)
            {
                int totalAcertados = 0;
                int totalFallados = 0;

                var ahorcados = await fachada.GetAhorcadoByODS(i);
                var preguntas = await fachada.GetPreguntasByODS(i);

                List<int?> listaIdsAhor = ahorcados.Select(ahorcado => ahorcado.Id).ToList();
                List<int?> listaIdsPreg = preguntas.Select(pregunta => pregunta.Id).ToList();

                for (int j = 0; j < retosAcertados.Count(); j++)
                {
                    if (listaIdsAhor.Contains(retosAcertados.ElementAt(j)) || listaIdsPreg.Contains(retosAcertados.ElementAt(j)))
                    {
                        totalAcertados++;
                    }
                }

                for (int j = 0; j < retosFallados.Count(); j++)
                {
                    if (listaIdsAhor.Contains(retosFallados.ElementAt(j)) || listaIdsPreg.Contains(retosFallados.ElementAt(j)))
                    {
                        totalFallados++;
                    }
                }

                int totalPreguntas = totalAcertados + totalFallados;
                int porcentajeAcertadas = (int)((float)totalAcertados / totalPreguntas * 100);

                ProgressBar progressBar = progressBarList[i - 1];
                TextView textView = textViewList[i - 1];
                progressBar.ScaleX = 1.0f;
                progressBar.Progress = porcentajeAcertadas;

                textView.Text = $"{porcentajeAcertadas}%";
            }
        }
    }
}