using Postgrest.Models;

namespace preguntaods.Entities
{
    public partial class Ahorcado : BaseModel, IEntity
    {
        public const int difAlta = 1003;
        public const int difMedia = 1002;
        public const int difBaja = 1001;

        public Ahorcado()
        { }

        public Ahorcado(string enunciado, string palabra)
        {
            Enunciado = enunciado;
            Palabra = palabra;
        }
    }
}