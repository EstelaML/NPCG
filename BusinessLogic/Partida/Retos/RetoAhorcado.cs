using preguntaods.Services;

namespace preguntaods.Entities
{
    public class RetoAhorcado : Reto
    {
        private readonly int type;
        private string palabra;
        private string enunciado;
        private static PreguntadosService servicio;

        public RetoAhorcado()
        {
            type = typeAhorcado;

        }

        public override int GetType()
        {
            return type;
        }

        public string GetPalabra()
        {
            return palabra;
        }

        public string GetEnunciado() 
        {
            return enunciado;
        }
    }
}