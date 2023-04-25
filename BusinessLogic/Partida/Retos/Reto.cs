namespace preguntaods.Entities
{
    public abstract class Reto
    {
        public const int TypePregunta = 100;
        public const int TypeAhorcado = 101;
        public const int TypeFrase = 102;
        public const int TypeSopa = 103;

        public abstract new int GetType();
    }
}