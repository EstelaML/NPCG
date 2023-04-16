using preguntaods.Entities;
using preguntaods.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preguntaods.Services
{
    public class PreguntadosService : IPreguntadosService
    {
        private readonly RepositorioPregunta repositorioPre;
        public PreguntadosService()
        {
            repositorioPre = new RepositorioPregunta();
        }

        #region ODS

        #endregion

        #region Reto
        #endregion

        #region RetoPregunta
        public async Task<Pregunta> SolicitarPregunta(int dificultad, List<Reto> retos)
        {
            // coge los retos de esa dificultad pero ya solo los que no haya hecho
            List<Pregunta> respuesta = await repositorioPre.GetByDificultad(dificultad);
            
            Random random = new Random();
            return  respuesta[random.Next(respuesta.Count)];
        }
        #endregion
    }
}