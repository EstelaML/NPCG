namespace preguntaods.Entities
{
    public class PartidaDirector
    {
        public Partida ConstructPartida(IPartidaBuilder builder)
        {
            return builder.GetPartida();
        }
    }
}