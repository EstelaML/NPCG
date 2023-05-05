using Android.Widget;
using preguntaods.BusinessLogic.Partida.Retos;
using Activity = Android.App.Activity;

namespace preguntaods.Presentacion.UI_impl
{
    public abstract class UserInterface
    {
        private TextView textoPuntosTotales;
        private TextView textoPuntosConsolidados;
        protected Activity Activity;

        public void InitializeUi(int fallos, int pistasUsadas, int ptsTotales, int ptsConsolidados, Reto retoActual)
        {
            SetValues(fallos, pistasUsadas, ptsTotales, ptsConsolidados);
            Init();
            SetDatosReto(retoActual);

            textoPuntosTotales = Activity.FindViewById<TextView>(Resource.Id.textView2);
            if (textoPuntosTotales != null) textoPuntosTotales.Text = "Puntos totales: " + ptsTotales;

            textoPuntosConsolidados = Activity.FindViewById<TextView>(Resource.Id.textPtsConsolidados);
            if (textoPuntosConsolidados != null) textoPuntosConsolidados.Text = "Puntos consolidados: " + ptsConsolidados;
        }

        public void SetActivity(Activity activity)
        {
            this.Activity = activity;
        }

        public abstract void Init();

        public abstract void SetDatosReto(Reto reto);

        public abstract void SetValues(int fallos, int pistasUsadas, int puntuacion, int ptsConsolidados);

        public abstract void FinReto();
    }
}