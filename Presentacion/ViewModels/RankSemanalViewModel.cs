using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using preguntaods.BusinessLogic.EstrategiaSonido;
using System;

namespace preguntaods.Presentacion.ViewModels
{
    [Activity(Label = "", Theme = "@style/HiddenTitleTheme")]
    public class RankSemanalViewModel : RankingViewModel
    {
        private TextView tipoRanking;
        private Button botonDer;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            tipoRanking = FindViewById<TextView>(Resource.Id.tipoRanking);
            tipoRanking.Text = "Ranking semanal";

            botonDer = FindViewById<Button>(Resource.Id.botonDer);
            botonDer.Text = "General";
            if (botonDer != null) { botonDer.Click += BotonDer; }
        }

        private void BotonDer(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            var i = new Intent(this, typeof(RankingViewModel));
            StartActivity(i);
        }
    }
}