using Android.App;

namespace preguntaods.Entities
{
    public class UserInterfaceAhorcado : UserInterface
    {
        private Activity _activity;

        public UserInterfaceAhorcado()
        { }

        public override void SetActivity(Activity activity)
        {
            _activity = activity;
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