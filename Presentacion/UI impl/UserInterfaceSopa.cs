using Android.App;
using preguntaods.BusinessLogic.Partida.Retos;

namespace preguntaods.Presentacion.UI_impl
{
    public class UserInterfaceSopa : UserInterface
    {
        private Activity activity;

        public override void SetActivity(Activity newActivity)
        {
            this.activity = newActivity;
        }

        public override void Init()
        {
            throw new System.NotImplementedException();
        }

        public override void SetDatosReto(Reto reto)
        {
            throw new System.NotImplementedException();
        }

        public override void FinReto()
        {
            throw new System.NotImplementedException();
        }

        public override void SetValues(int fallos, int puntuacion, int ptsConsolidados)
        {
            throw new System.NotImplementedException();
        }
    }
}