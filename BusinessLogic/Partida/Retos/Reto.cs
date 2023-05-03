using System.Threading.Tasks;

namespace preguntaods.BusinessLogic.Partida.Retos
{
    public abstract class Reto
    {
        public const int TypePregunta = 100;
        public const int TypeAhorcado = 101;
        public const int TypeFrase = 102;
        public const int TypeSopa = 103;

        private int type;

        public int GetType()
        {
            return type;
        }

        public void SetType(int newType)
        {
            type = newType;
        }

        public abstract Task SetValues();
    }
}