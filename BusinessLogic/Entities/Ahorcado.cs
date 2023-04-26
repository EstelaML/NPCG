// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    public partial class Ahorcado
    {
        public const int DifAlta = 1003;
        public const int DifMedia = 1002;
        public const int DifBaja = 1001;

        public Ahorcado()
        { }

        public Ahorcado(string enunciado, string palabra, int dif)
        {
            Enunciado = enunciado;
            Palabra = palabra;
            Dificultad = dif;
        }
    }
}