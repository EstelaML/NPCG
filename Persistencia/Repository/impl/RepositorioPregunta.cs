using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using preguntaods.Entities;

namespace preguntaods.Persistencia.Repository.impl
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
            //var id = (conexion.usuario.Id);
            //var user = await conexion.cliente.From<Usuario>().Where(x => x.Uuid == id).Single();
            //var response = await conexion.cliente.From<Pregunta>().Where(x => x.Dificultad == dificultad).Get();
            //
            //List<Pregunta> preguntas = response?.Models?.ToList();
            //List<int> preguntasHechas = user?.PreguntasRealizadas?.ToList();
            //preguntas = preguntasHechas != null ? preguntas?.Where(pregunta => !preguntasHechas.Contains((int)pregunta.Id)).ToList() : preguntas;
            //
            //return preguntas ?? new List<Pregunta>();

            var id = (conexion.usuario.Id);
            var task1 = (conexion.cliente.From<Usuario>().Where(x => x.Uuid == id).Single());
            var task2 = (conexion.cliente.From<Pregunta>().Where(x => x.Dificultad == dificultad).Get());
            List<Task> tareas = new List<Task> { task1, task2 };
            await Task.WhenAll(tareas);

            var usuario = task1.Result;
            var response = task2.Result;
            List<Pregunta> preguntas = response?.Models?.ToList();
            List<int> preguntasHechas = usuario?.PreguntasRealizadas?.ToList();
            preguntas = preguntasHechas != null ? preguntas?.Where(pregunta => !preguntasHechas.Contains((int)pregunta.Id)).ToList() : preguntas;

            return preguntas ?? new List<Pregunta>();
        }
    }
}