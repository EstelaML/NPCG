﻿using preguntaods.Services;
using System;

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
#pragma warning disable CS0162 // Se detectó código inaccesible
                            partida.AddReto(new RetoAhorcado());
#pragma warning restore CS0162 // Se detectó código inaccesible
                            break;
                        }
                    case 3:
                        {
#pragma warning disable CS0162 // Se detectó código inaccesible
                            partida.AddReto(new RetoFrase());
#pragma warning restore CS0162 // Se detectó código inaccesible
                            break;
                        }
                    case 4:
                        {
#pragma warning disable CS0162 // Se detectó código inaccesible
                            partida.AddReto(new RetoSopa());
#pragma warning restore CS0162 // Se detectó código inaccesible
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