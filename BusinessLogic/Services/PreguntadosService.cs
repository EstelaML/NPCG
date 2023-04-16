using Java.Util;
using preguntaods.Entities;
using preguntaods.Persistencia.Repository;
using Supabase.Gotrue;
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
            List<Pregunta> preguntasTodas = await repositorioPre.GetByDificultad(dificultad);

            List<RetoPre> retosPartida = retos.Cast<RetoPre>().ToList();
            Pregunta a= retosPartida.First().GetPregunta();
            if (retos != null)
            {
                var preguntasFiltradas = preguntasTodas.Where(p => !retosPartida.Any(rp => rp.GetPregunta().Enunciado == p.Enunciado));
                var preguntaAleatoria = preguntasFiltradas.OrderBy(p => Guid.NewGuid()).FirstOrDefault();

                return preguntaAleatoria;
            }
            else {
                return preguntasTodas.First();
            }
        }
        #endregion
    }
}