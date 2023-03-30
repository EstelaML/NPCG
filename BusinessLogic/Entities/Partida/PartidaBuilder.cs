namespace preguntaods.Entities
{
    public class PartidaBuilder : IPartidaBuilder
    {
        private Partida partida = new Partida();

        public Partida GetPartida() {
            return partida;
        }
    }
}