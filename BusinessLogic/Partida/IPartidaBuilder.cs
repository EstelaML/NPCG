namespace preguntaods.BusinessLogic.Partida
{
    public interface IPartidaBuilder
    {
        void BuildPlayer();

        void BuildRetos(int j);

        void BuildUserInterface();

        void BuildFacade();

        void BuildSonido();

        Partida GetPartida();
    }
}