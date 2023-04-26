namespace preguntaods.BusinessLogic.EstrategiaSonido
{
    public class Sonido
    {
        private IEstrategiaSonido estrategia;
        private Android.Content.Context context;

        public Sonido()
        { }

        public void SetEstrategia(IEstrategiaSonido newEstrategia, Android.Content.Context context)
        {
            this.estrategia = newEstrategia;
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