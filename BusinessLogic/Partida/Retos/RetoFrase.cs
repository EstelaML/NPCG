﻿using preguntaods.BusinessLogic.Services;
using System.Threading.Tasks;

namespace preguntaods.BusinessLogic.Partida.Retos
{
    public class RetoFrase : IReto
    {
        private static PreguntadosService _servicio;
        private readonly int numeroReto;

        public RetoFrase(int orden)
        {
            Type = IReto.TypeFrase;

            _servicio = new PreguntadosService();
            numeroReto = orden;
        }

        public Task SetDif(int orden)
        {
            throw new System.NotImplementedException();
        }

        public int Type { get; set; }

        public async Task SetValues()
        {
            await Task.CompletedTask;
        }
    }
}