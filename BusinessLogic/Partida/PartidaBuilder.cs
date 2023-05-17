using preguntaods.BusinessLogic.EstrategiaSonido;
using preguntaods.BusinessLogic.Partida.Retos;
using preguntaods.BusinessLogic.Services;
using System;
using System.Threading.Tasks;

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
                int n = numeroReto;

                if (numeroReto == 5)
                {
                    Random random = new Random();

                    n = random.Next(1, 3);
                }

                switch (n) //ampliar conforme se añadan nuevos >> random.Next(1,5)
                {
                    case 1:
                        {
                            fabrica = new FabricaRetoPregunta();
                            break;
                        }
                    case 2:
                        {
                            fabrica = new FabricaRetoAhorcado();
                            break;
                        }
                    case 3:
                        {
                            fabrica = new FabricaRetoFrase();
                            break;
                        }
                    default:
                        {
                            fabrica = new FabricaRetoSopa();
                            break;
                        }
                }

                reto = fabrica.CrearReto(i);

                await reto.SetValues();

                partida.AddReto(reto);
            }
        }

        public void BuildUserInterface()
        {
            //partida.UpdateUi();
        }

        public void BuildFacade()
        {
            partida.SetFacade(new Facade());
        }

        public void BuildSonido()
        {
            partida.SetSonido(new Sonido());
        }

        public Partida GetPartida()
        {
            return partida;
        }
    }
}