namespace preguntaods.Entities
{
    public class RetoAhorcado : Reto
    {
        private readonly int type;
        public RetoAhorcado()
        {
            type = typeAhorcado;
        }

        public override int GetType()
        {
            return type;
        }
    }
}