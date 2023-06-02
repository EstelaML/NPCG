namespace preguntaods.BusinessLogic.Sonidos
{
    public class Sonido
    {
        private IEstrategiaSonido estrategia;
        private Android.Content.Context context;

        public void SetEstrategia(IEstrategiaSonido newEstrategia, Android.Content.Context newContext)
        {
            estrategia = newEstrategia;
            context = newContext;
        }

        public void EjecutarSonido()
        {
            estrategia.Play(context);
        }

        public void PararSonido()
        {
            estrategia.Stop();
        }
    }
}