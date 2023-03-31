using System;

namespace preguntaods.Entities
{
    public class PartidaBuilder : IPartidaBuilder
    {
        private Partida partida = new Partida();

        public void BuildPlayer()
        {
            partida.user = new Usuario();
        }
        public void BuildReto()
        {
            Random random = new Random();

            switch (1) //ampliar conforme se añadan nuevos >> random.Next(1,5)
            {
                case 1:
                    {
                        partida.reto = new RetoPre();
                        break;
                    }
                case 2:
                    {
                        partida.reto = new RetoAhorcado();
                        break;
                    }
                case 3:
                    {
                        partida.reto = new RetoSopa();
                        break;
                    }
                case 4:
                    {
                        partida.reto = new RetoFrase();
                        break;
                    }
            }
            
        }
        public Partida GetPartida() {
            return partida;
        }
    }
}