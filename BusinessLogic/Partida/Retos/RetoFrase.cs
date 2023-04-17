namespace preguntaods.Entities
{
    public class RetoFrase : Reto
    {
        private readonly int type;

        public RetoFrase()
        {
            type = typeFrase;
        }

        public override int GetType()
        {
            return type;
        }
    }
}