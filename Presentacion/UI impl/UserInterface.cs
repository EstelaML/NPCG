using Android.App;
using preguntaods.BusinessLogic.Partida.Retos;

namespace preguntaods.Presentacion.UI_impl
{
    public abstract class UserInterface
    {
        public Initialize
        public abstract void Init();

        public abstract void SetDatosReto(Reto reto);

        public abstract void SetActivity(Activity activity);

        public abstract void SetValues(int fallos, int puntuacion, int ptsConsolidados);

        public abstract void FinReto();
    }
}