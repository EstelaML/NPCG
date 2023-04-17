using preguntaods.Services;
using System;
using System.Linq;

namespace preguntaods.Entities
{
    public class PartidaBuilder : IPartidaBuilder
    {
        private Partida partida = new Partida();

        public async void BuildPlayer()
        {
            var fachada = partida.GetFacade();
            partida.user = await fachada.GetUsuarioLogged();
        }

        public void BuildRetos()
        {
            for (int i = 0; i < 12; i++)
            {
                Random random = new Random();

                switch (1) //ampliar conforme se añadan nuevos >> random.Next(1,5)
                {
                    case 1:
                        {
                            partida.AddReto(new RetoPre(partida.GetRetos(), i));
                            break;
                        }
                    case 2:
                        {
                            partida.AddReto(new RetoAhorcado());
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
            partida.UpdateUI();
        }

        public void BuildFacade()
        {
            partida.SetFacade(new Facade());
        }

        public Partida GetPartida() {
            return partida;
        }
    }
}