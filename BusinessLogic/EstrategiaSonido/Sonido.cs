namespace preguntaods.BusinessLogic.EstrategiaSonido
{
    public class Sonido
    {
        private IEstrategiaSonido estrategia;
        private Android.Content.Context context;

        public Sonido()
        { }

        public void SetEstrategia(IEstrategiaSonido estrategia, Android.Content.Context context)
        {
            this.estrategia = estrategia;
            this.context = context;
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