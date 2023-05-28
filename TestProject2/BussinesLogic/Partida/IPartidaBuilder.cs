
namespace preguntaods.BusinessLogic.Partida
{
    public interface IPartidaBuilder
    {
        void BuildPlayer();

        Task BuildRetos(int numeroReto);

        void BuildFacade();

        Partida GetPartida();
    }
}