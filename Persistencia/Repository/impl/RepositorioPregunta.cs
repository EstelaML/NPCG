using Java.Util;
using preguntaods.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preguntaods.Persistencia.Repository
{
    public class RepositorioPregunta : Repository<Pregunta>
    {
        SingletonConexion conexion;
        public RepositorioPregunta()
        {
            conexion = SingletonConexion.GetInstance();
        }
        public async Task<List<Pregunta>> GetByDificultad(int dificultad)
        {
            var a = conexion.usuario.Id;
            // cojo todas las Preguntas y las paso a una lista
            var preguntasTodos = await conexion.cliente
                .From<Pregunta>()
                .Where(x => x.Dificultad == dificultad).Get();

            List<Pregunta> preguntasTodasLista = preguntasTodos.Models.AsEnumerable().ToList();

            // cojo del usuario Loggeado las preguntas que haya acertado y estén en la BD
            var retosRealizados = await conexion.cliente.From<Usuario>().Where(x => x.Uuid == a).Single();
            int[] retosID = retosRealizados.PreguntasRealizadas;
            if (retosID != null)
            {
                List<int> preguntasRealizadas = retosID.ToList();

                // elimino de la lista de preguntas
                preguntasTodasLista.RemoveAll(elemento => preguntasRealizadas.Contains((int)elemento.Id));
            }
            
            //return response.Models.AsEnumerable();
            return preguntasTodasLista;
        }
    }       
}