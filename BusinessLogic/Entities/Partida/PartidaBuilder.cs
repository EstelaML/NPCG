using Android.SE.Omapi;
using preguntaods.Persistencia.Repository;
using preguntaods.Services;
using System;

namespace preguntaods.Entities
{
    public class PartidaBuilder : IPartidaBuilder
    {
        private Partida partida = new Partida();
       
        public void BuildPlayer()
        {
            partida.user = partida._fachada.GetUsarioLogged().Result;
            
        }

        public void BuildRetos()
        {
            for (int i = 0; i < 10; i++)
            {
                Random random = new Random();

                switch (1) //ampliar conforme se añadan nuevos >> random.Next(1,5)
                {
                    case 1:
                        {
                            partida.AddReto(new RetoPre());
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