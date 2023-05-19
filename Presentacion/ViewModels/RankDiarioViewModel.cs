using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using preguntaods.BusinessLogic.EstrategiaSonido;
using System;

namespace preguntaods.Presentacion.ViewModels
{
    [Activity(Label = "", Theme = "@style/HiddenTitleTheme")]
    public class RankDiarioViewModel : RankingViewModel
    {
        private TextView tipoRanking;
        private Button botonIzq;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            tipoRanking = FindViewById<TextView>(Resource.Id.tipoRanking);
            tipoRanking.Text = "Ranking diario";

            botonIzq = FindViewById<Button>(Resource.Id.botonIzq);
            botonIzq.Text = "General";
            if (botonIzq != null) { botonIzq.Click += BotonIzq; }
        }

        private void BotonIzq(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            var i = new Intent(this, typeof(RankingViewModel));
            StartActivity(i);
        }
    }
}