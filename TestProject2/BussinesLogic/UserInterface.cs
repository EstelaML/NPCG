using preguntaods.BusinessLogic.Retos;

namespace preguntaods.Presentacion.UI_impl
{
    public abstract class UserInterface
    {
        // protected Activity Activity;

        public void InitializeUi(int fallos, int pistasUsadas, int ptsTotales, int ptsConsolidados, IReto retoActual, int orden)
        {
            SetValues(fallos, pistasUsadas, ptsTotales, ptsConsolidados);
            Init();
            SetDatosReto(retoActual);
        }

        /*     public void SetActivity(Activity activity)
             {
                 Activity = activity;
             }
        */

        public abstract void Init();

        public abstract void SetDatosReto(IReto reto);

        public abstract void SetValues(int fallos, int pistasUsadas, int puntuacion, int ptsConsolidados);

        public abstract void FinReto();
    }
}