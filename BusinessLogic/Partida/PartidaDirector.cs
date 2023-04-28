using System.Threading.Tasks;

namespace preguntaods.BusinessLogic.Partida
{
    public class PartidaDirector
    {
        public async Task<Partida> ConstructPartida(IPartidaBuilder builder, int numeroReto)
        {
            builder.BuildFacade();
            builder.BuildSonido();
            builder.BuildPlayer();
            await builder.BuildRetos(numeroReto);
            builder.BuildUserInterface();

            return builder.GetPartida();
        }
    }
}