using System;
using preguntaods.BusinessLogic.EstrategiaSonido;
using preguntaods.BusinessLogic.Partida.Retos;
using preguntaods.BusinessLogic.Services;

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

        public void BuildRetos(int j)
        {
            for (var i = 0; i < 12; i++)
            {
                var random = new Random();

                switch (j) //ampliar conforme se añadan nuevos >> random.Next(1,5)
                {
                    case 1:
                        {
                            partida.AddReto(new RetoPre(partida.GetRetos(), i));
                            break;
                        }
                    case 2:
                        {
                            partida.AddReto(new RetoAhorcado(i));
                            break;
                        }
                    case 3:
                        {
                            partida.AddReto(new RetoFrase());
                            break;
                        }
                    case 4:
                        {
                            partida.AddReto(new RetoSopa());
                            break;
                        }
                }
            }
        }

        public void BuildUserInterface()
        {
            partida.UpdateUi();
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