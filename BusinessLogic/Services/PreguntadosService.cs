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

        #region RetoPregunta
        public async Task<Pregunta> SolicitarPregunta(int dificultad, List<Reto> retos)
        {
            // coge los retos de esa dificultad pero ya solo los que no haya hecho
            List<Pregunta> respuesta = await repositorioPre.GetByDificultad(dificultad, retos);
            if (respuesta != null)
            {
                return respuesta.First();
            }
            else 
            {
                return null;
            }
        }
           
        #endregion
    }
}