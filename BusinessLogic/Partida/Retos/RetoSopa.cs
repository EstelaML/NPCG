﻿namespace preguntaods.BusinessLogic.Partida.Retos
{
    public class RetoSopa : Reto
    {
        private readonly int type;

        public RetoSopa()
        {
            type = TypeSopa;
        }

        public override int GetType()
        {
            return type;
        }
    }
}