namespace preguntaods.Entities
{
    public class RetoSopa : Reto
    {
        private readonly int type;

        public RetoSopa()
        {
            type = typeSopa;
        }

        public override int GetType()
        {
            return type;
        }
    }
}