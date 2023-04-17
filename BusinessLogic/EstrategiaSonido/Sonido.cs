namespace preguntaods.Entities
{
    public class Sonido
    {
        private IEstrategiaSonido _estrategia;
        private Android.Content.Context _context;

        public Sonido()
        { }

        public void SetEstrategia(IEstrategiaSonido estrategia, Android.Content.Context context)
        {
            _estrategia = estrategia;
            _context = context;
        }

        public void EjecutarSonido()
        {
            _estrategia.Play(_context);
        }

        public void PararSonido()
        {
            _estrategia.Stop();
        }
    }
}