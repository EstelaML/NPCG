using Android.Widget;
using preguntaods.BusinessLogic.Retos;
using Activity = Android.App.Activity;

namespace preguntaods.Presentacion.UI_impl
{
    public abstract class UserInterface
    {
        private TextView textoPuntosTotales;
        private TextView textoPuntosConsolidados;
        private TextView textoNumReto;
        protected Activity Activity;

        public void InitializeUi(int fallos, int pistasUsadas, int ptsTotales, int ptsConsolidados, IReto retoActual, int orden)
        {
            SetValues(fallos, pistasUsadas, ptsTotales, ptsConsolidados);
            Init();
            SetDatosReto(retoActual);

            textoPuntosTotales = Activity.FindViewById<TextView>(Resource.Id.textView2);
            if (textoPuntosTotales != null) textoPuntosTotales.Text = "Puntos totales: " + ptsTotales;

            textoPuntosConsolidados = Activity.FindViewById<TextView>(Resource.Id.textPtsConsolidados);
            if (textoPuntosConsolidados != null) textoPuntosConsolidados.Text = "Puntos consolidados: " + ptsConsolidados;

            textoNumReto = Activity.FindViewById<TextView>(Resource.Id.numReto);
            if (textoNumReto != null) textoNumReto.Text = orden + "/10";
        }

        public void SetActivity(Activity activity)
        {
            Activity = activity;
        }

        public abstract void Init();

        public abstract void SetDatosReto(IReto reto);

        public abstract void SetValues(int fallos, int pistasUsadas, int puntuacion, int ptsConsolidados);

        public abstract void FinReto();
    }
}