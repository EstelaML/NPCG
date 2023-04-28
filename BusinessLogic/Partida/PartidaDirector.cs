namespace preguntaods.BusinessLogic.Partida
{
    public class PartidaDirector
    {
        public Partida ConstructPartida(IPartidaBuilder builder, int numeroReto)
        {
            builder.BuildFacade();
            builder.BuildSonido();
            builder.BuildPlayer();
            builder.BuildRetos(numeroReto);
            builder.BuildUserInterface();

            return builder.GetPartida();
        }
    }
}