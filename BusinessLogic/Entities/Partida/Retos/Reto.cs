namespace preguntaods.Entities
{
    public abstract class Reto
    {
        public const int typePregunta = 100;
        public const int typeAhorcado = 101;
        public const int typeFrase = 102;
        public const int typeSopa = 103;

        public abstract new int GetType();
    }
}