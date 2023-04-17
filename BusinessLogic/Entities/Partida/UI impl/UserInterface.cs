using Android.App;

namespace preguntaods.Entities
{
    public abstract class UserInterface
    {
        public abstract void Init();

        public abstract void SetDatosReto(Reto reto);

        public abstract void SetActivity(Activity activity);

        public abstract void SetValues(int fallos, int puntuacion);

        public abstract void FinReto();
    }
}