namespace preguntaods.Entities
{
    public class PartidaDirector
    {
        public Partida ConstructPartida(IPartidaBuilder builder)
        {
            builder.BuildPlayer();
            builder.BuildRetos();
            builder.BuildUserInterface();
            builder.BuildFacade();

            return builder.GetPartida();
        }
    }
}