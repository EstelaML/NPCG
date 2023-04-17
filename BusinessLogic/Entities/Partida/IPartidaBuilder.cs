namespace preguntaods.Entities
{
    public interface IPartidaBuilder
    {
        void BuildPlayer();

        void BuildRetos();

        void BuildUserInterface();

        void BuildFacade();
        void BuildSonido();
        Partida GetPartida();
    }
}