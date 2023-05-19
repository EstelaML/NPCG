using Android.App;
using Android.OS;

namespace preguntaods.Presentacion.ViewModels
{
    [Activity(Label = "", Theme = "@style/HiddenTitleTheme")]
    public class RankSemanalViewModel : RankingViewModel
    {
        private TextView tipoRanking;
        private Button botonDer;
        protected override async void OnCreate(Bundle savedInstanceState)
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