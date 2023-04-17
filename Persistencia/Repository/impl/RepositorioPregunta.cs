using Android.Net.Wifi.Rtt;
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