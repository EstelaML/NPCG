namespace preguntaods.Entities
{
    public class RetoFrase : Reto
    {
        private readonly int type;

        public RetoFrase()
        {
            type = TypeFrase;
        }

        public override int GetType()
        {
            return type;
        }
    }
}