namespace preguntaods.Entities
{
    public class PartidaDirector
    {
        public Partida ConstructPartida(IPartidaBuilder builder)
        {
            builder.BuildFacade();
            builder.BuildPlayer();
            builder.BuildRetos();
            builder.BuildUserInterface();

            return builder.GetPartida();
        }
    }
}