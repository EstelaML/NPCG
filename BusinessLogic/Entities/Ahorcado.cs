using Postgrest.Models;

namespace preguntaods.Entities
{
    public partial class Ahorcado : BaseModel, IEntity
    {
        public const int DifAlta = 1003;
        public const int DifMedia = 1002;
        public const int DifBaja = 1001;

        public Ahorcado()
        { }

        public Ahorcado(string enunciado, string palabra)
        {
            Enunciado = enunciado;
            Palabra = palabra;
        }
    }
}