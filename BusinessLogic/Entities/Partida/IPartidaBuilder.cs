namespace preguntaods.Entities
{
    public interface IPartidaBuilder
    {
        void BuildPlayer();
        void BuildRetos();
        void BuildUserInterface();
        Partida GetPartida();
    }
}