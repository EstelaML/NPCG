using preguntaods.BusinessLogic.FabricaRetos;
using preguntaods.BusinessLogic.Fachada;
using preguntaods.BusinessLogic.Retos;

namespace preguntaods.BusinessLogic.Partida
{
    public class PartidaBuilder : IPartidaBuilder
    {
        private readonly Partida partida = new Partida();

        public async void BuildPlayer()
        {
            var fachada = partida.GetFacade();
            partida.User = await fachada.GetUsuarioLogged();
        }

        public async Task BuildRetos(int numeroReto)
        {
            for (var i = 0; i < 12; i++)
            {
                IReto reto;
                FabricaReto fabrica;
                var n = numeroReto;

                if (numeroReto == 5)
                {
                    var random = new Random();

                    n = random.Next(1, 3);
                }

                fabrica = n switch //ampliar conforme se añadan nuevos >> random.Next(1,5)
                {
                    1 => new FabricaRetoPregunta(),
                    2 => new FabricaRetoAhorcado(),
                    3 => new FabricaRetoFrase(),
                    _ => new FabricaRetoSopa()
                };

                reto = fabrica.CrearReto(i);

                await reto.SetValues();

                partida.AddReto(reto);
            }
        }

        public void BuildFacade()
        {
            partida.SetFacade(Facade.GetInstance());
        }

        public Partida GetPartida()
        {
            return partida;
        }
    }
}