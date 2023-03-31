namespace preguntaods.Entities
{
    public interface IPartidaBuilder
    {
        void BuildPlayer();
        void BuildReto();
        Partida GetPartida();
    }
}