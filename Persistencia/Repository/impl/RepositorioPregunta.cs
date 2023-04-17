using preguntaods.Entities;
using System.Collections.Generic;
using System.Linq;
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
            var id = (conexion.usuario.Id);
            var user = await conexion.cliente.From<Usuario>().Where(x => x.Uuid == id).Single();
            var response = await conexion.cliente.From<Pregunta>().Where(x => x.Dificultad == dificultad).Get();

            List<Pregunta> preguntas = response?.Models?.ToList();
            List<int> preguntasHechas = user?.PreguntasRealizadas?.ToList();
            preguntas = preguntasHechas != null ? preguntas?.Where(pregunta => !preguntasHechas.Contains((int)pregunta.Id)).ToList() : preguntas;

            return preguntas ?? new List<Pregunta>();
        }
    }
}