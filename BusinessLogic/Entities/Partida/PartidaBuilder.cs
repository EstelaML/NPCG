using Android.SE.Omapi;
using preguntaods.Persistencia.Repository;
using preguntaods.Services;
using System;
using System.Collections.Generic;
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
                            RetoPre retoPre = new RetoPre(partida.GetRetos(), i);
                            partida.AddReto(retoPre);
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
            var a = partida.GetRetos();
            var pregunta = (a.First() as RetoPre).GetPregunta();
            var p = pregunta.Enunciado;
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