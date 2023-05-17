using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    public interface IReto
    {
        public const int TypePregunta = 100;
        public const int TypeAhorcado = 101;
        public const int TypeFrase = 102;
        public const int TypeSopa = 103;

        int Type { get; set; }

        public int GetType()
        {
            return Type;
        }

        public Task SetValues();

        public Task SetDif(int orden);
    }
}