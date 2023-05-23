using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.BusinessLogic.EstrategiaSonido;
using preguntaods.BusinessLogic.Fachada;
using preguntaods.Entities;
using System;

namespace preguntaods.Presentacion.ViewModels
{
    [Activity(Label = "", Theme = "@style/HiddenTitleTheme")]
    public class CoberturaODSViewModel : AppCompatActivity
    {
        private Sonido sonido;
        private Facade fachada;
        private Usuario usuario;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaCoberturaOds);

            ScrollView scrollView = FindViewById<ScrollView>(Resource.Id.scrollView);
            scrollView.NestedScrollingEnabled = true;

            sonido = new Sonido();
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);

            string odsTitle = "ODS 1: Fin de la pobreza";
            int preguntasAcertadas = 10;
            int preguntasFalladas = 5;
            int totalPreguntas = preguntasAcertadas + preguntasFalladas;

            TextView odsTitleTextView = FindViewById<TextView>(Resource.Id.ods1Title);
            ProgressBar odsProgressBar = FindViewById<ProgressBar>(Resource.Id.ods1ProgressBar);
            TextView odsPercentageTextView = FindViewById<TextView>(Resource.Id.ods1PercentageTextView);

            var atras = FindViewById<ImageButton>(Resource.Id.buttonAtras);
            if (atras != null) atras.Click += Atras;

            odsTitleTextView.Text = odsTitle;

            int porcentajeAcertadas = (int)((float)preguntasAcertadas / totalPreguntas * 100);

            odsProgressBar.SetProgress(porcentajeAcertadas, true);
            odsPercentageTextView.Text = $"{porcentajeAcertadas}%";
        }

        private void Atras(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            var i = new Intent(this, typeof(PerfilViewModel));
            StartActivity(i);
            Finish();
        }
    }
}