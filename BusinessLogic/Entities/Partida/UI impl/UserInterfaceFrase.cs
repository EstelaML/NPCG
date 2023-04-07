using Android.App;

namespace preguntaods.Entities
{
    public class UserInterfaceFrase : UserInterface
    {
        private Activity _activity;

        public UserInterfaceFrase() { }

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

        public override void SetValues(int fallos, int puntuacion)
        {
            throw new System.NotImplementedException();
        }
    }
}