using preguntaods.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace preguntaods.Persistencia.Repository
{
    public class RepositorioPregunta : Repository<Pregunta>
    {
        private SingletonConexion conexion;

        public RepositorioPregunta()
        {
            conexion = SingletonConexion.GetInstance();
        }

        public async Task<List<Pregunta>> GetByDificultad(int dificultad)
        {
            var response = await conexion.cliente
                .From<Pregunta>()
                .Where(x => x.Dificultad == dificultad)
                .Get();

            return response.Models;
        }

        /*
        public async Task<List<Pregunta>> GetByDificultad(int dificultad, List<Reto> retos)
        {
            var a = conexion.usuario.Id;
            // cojo todas las Preguntas y las paso a una lista
            var preguntasTodos = await conexion.cliente
                .From<Pregunta>()
                .Where(x => x.Dificultad == dificultad)
                .Get();

            List<Pregunta> preguntasTodasLista = preguntasTodos.Models.AsEnumerable().ToList();

            // cojo del usuario Loggeado las preguntas que haya acertado y estén en la BD
            var retosRealizados = await conexion.cliente.From<Usuario>().Where(x => x.Uuid == a).Single();
            int[] retosID = retosRealizados.PreguntasRealizadas;
            List<Pregunta> preguntasPosibles = preguntasTodasLista;
            if (retosID != null)
            {
                List<int> preguntasRealizadas = retosID.ToList();
                // elimino de la lista de preguntas ya realizadas
                preguntasPosibles = preguntasTodasLista.Where(x => x.Id.HasValue && !preguntasRealizadas.Contains((int)x.Id)).Select(x => x).ToList();
            }
            if (retos != null)
            {
                // !retos
                List<int> preguntasRetoRealizada = retos.Cast<RetoPre>().Where(x => x.GetPregunta()?.Id.HasValue == true).Select(x => x.GetPregunta().Id.Value).ToList();
                preguntasPosibles = preguntasPosibles.Where(x => !preguntasRetoRealizada.Contains((int)x.Id)).ToList();
            }
            //return response.Models.AsEnumerable();
            return preguntasPosibles;
        }
        */
    }
}