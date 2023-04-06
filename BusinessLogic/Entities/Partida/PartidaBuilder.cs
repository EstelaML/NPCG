﻿using System;

namespace preguntaods.Entities
{
    public class PartidaBuilder : IPartidaBuilder
    {
        private Partida partida = new Partida();

        public void BuildPlayer()
        {
            partida.user = new Usuario();
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

            partida.NextReto();
        }

        public void BuildUserInterface()
        {
            partida.UpdateUI();
        }

        public Partida GetPartida() {
            return partida;
        }
    }
}