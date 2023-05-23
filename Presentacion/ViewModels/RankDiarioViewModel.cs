using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Views;
using Android.Widget;
using preguntaods.BusinessLogic.EstrategiaSonido;
using preguntaods.BusinessLogic.Fachada;
using System;

namespace preguntaods.Presentacion.ViewModels
{
    [Activity(Label = "", Theme = "@style/HiddenTitleTheme")]
    public class RankDiarioViewModel : RankingViewModel
    {
        private TextView tipoRanking;
        private Button botonIzq;
        private string nombreRanking = "Diario";

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            tipoRanking = FindViewById<TextView>(Resource.Id.tipoRanking);
            if (tipoRanking != null) tipoRanking.Text = "Ranking diario";

            botonIzq = FindViewById<Button>(Resource.Id.botonIzq);
            if (botonIzq != null)
            {
                botonIzq.Text = "General";
                botonIzq.Click += BotonIzq;
            }
            usuariosOrdenados = await fachada.GetAllUsersOrderedByDay();
            
        }

        private void BotonIzq(object sender, EventArgs e)
        {
            Sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            Sonido.EjecutarSonido();

            var i = new Intent(this, typeof(RankingViewModel));
            StartActivity(i);
            Finish();
        }
    }
}