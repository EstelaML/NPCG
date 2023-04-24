namespace preguntaods.Entities
{
    public class PartidaDirector
    {
        public Partida ConstructPartida(IPartidaBuilder builder, int j)
        {
            builder.BuildFacade();
            builder.BuildSonido();
            builder.BuildPlayer();
            builder.BuildRetos(j);
            builder.BuildUserInterface();

            return builder.GetPartida();
        }
    }
}