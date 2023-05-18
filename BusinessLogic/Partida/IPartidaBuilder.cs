using System.Threading.Tasks;

namespace preguntaods.BusinessLogic.Partida
{
    public interface IPartidaBuilder
    {
        void BuildPlayer();

        Task BuildRetos(int numeroReto);

        void BuildFacade();

        void BuildSonido();

        Partida GetPartida();
    }
}