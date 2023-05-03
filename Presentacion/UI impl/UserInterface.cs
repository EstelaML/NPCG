using Android.Widget;
using preguntaods.BusinessLogic.Partida.Retos;
using Activity = Android.App.Activity;

namespace preguntaods.Presentacion.UI_impl
{
    public abstract class UserInterface
    {
        private TextView textoPuntosTotales;
        private TextView textoPuntosConsolidados;
        protected Activity activity;

        public void InitializeUi(int fallos, int ptsTotales, int ptsConsolidados, Reto retoActual)
        {
            SetValues(fallos, ptsTotales, ptsConsolidados);
            Init();
            SetDatosReto(retoActual);

            textoPuntosTotales = activity.FindViewById<TextView>(Resource.Id.textView2);
            if (textoPuntosTotales != null) textoPuntosTotales.Text = "Puntos totales: " + ptsTotales;

            textoPuntosConsolidados = activity.FindViewById<TextView>(Resource.Id.textPtsConsolidados);
            if (textoPuntosConsolidados != null) textoPuntosConsolidados.Text = "Puntos consolidados: " + ptsConsolidados;
        }

        public void SetActivity(Activity activity)
        {
            this.activity = activity;
        }

        public abstract void Init();

        public abstract void SetDatosReto(Reto reto);

        public abstract void SetValues(int fallos, int puntuacion, int ptsConsolidados);

        public abstract void FinReto();
    }
}