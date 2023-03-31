using preguntaods.Entities;
using preguntaods.Persistencia.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preguntaods.Services
{
    public class PreguntadosService : IPreguntadosService
    {
        private readonly RepositorioPregunta repositorioPre;
        public PreguntadosService() { }

        #region ODS

        #endregion

        #region Reto
        #endregion

        #region RetoPregunta
        public List<Pregunta> SolicitarPreguntas()
        {
            var preguntasBajas = repositorioPre.GetByDificultad(Pregunta.difBaja).Result.Take(3);
            var preguntasMedias = repositorioPre.GetByDificultad(Pregunta.difMedia).Result.Take(4);
            var preguntasAltas = repositorioPre.GetByDificultad(Pregunta.difAlta).Result.Take(3);

            return preguntasBajas.Concat(preguntasMedias.Concat(preguntasAltas)).ToList();
        }
        #endregion
    }
}