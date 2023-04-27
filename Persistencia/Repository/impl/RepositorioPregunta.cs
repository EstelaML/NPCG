using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using preguntaods.BusinessLogic.Partida.Retos;
using preguntaods.Entities;

namespace preguntaods.Persistencia.Repository.impl
{
    public class RepositorioPregunta : Repository<Pregunta>
    {
        private SingletonConexion conexion;
        private readonly RepositorioUsuario repositorioUser;

        public RepositorioPregunta()
        {
            conexion = SingletonConexion.GetInstance();
            repositorioUser = new RepositorioUsuario();
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

            // get lista de retos 

            var uuid = (conexion.Usuario.Id);
            var user = await repositorioUser.GetUserByUUid(uuid);
            if (user?.Id == null) return null;
            var id = (int) user.Id;
            var task1 = (conexion.Cliente.From<RetosRealizados>().Where(x => x.Usuario == id).Single());
            var task2 = (conexion.Cliente.From<Pregunta>().Where(x => x.Dificultad == dificultad).Get());
            List<Task> tareas = new List<Task> { task1, task2 };
            await Task.WhenAll(tareas);

            var retos = task1.Result;
            var response = task2.Result;
            var preguntas = response.Models.ToList();
            var preguntasHechas = retos?.PreguntasRealizadas?.ToList();
            preguntas = preguntasHechas != null ? preguntas.Where(pregunta => pregunta.Id != null && !preguntasHechas.Contains((int)pregunta.Id)).ToList() : preguntas;

            if (preguntas.Count >= 5) return preguntas;
            repositorioUser.UpdatePreguntaAcertada("", null, user);
            return response.Models.ToList();
        }

        public async Task AñadirPreguntaRealizada(int id, Reto reto) 
        {
            // cogemos del usuario las preguntas acertadas ya
            var pregunta = ((RetoPre)reto).GetPregunta();
            var a = conexion.Usuario.Id;
            var usuario = await repositorioUser.GetUserByUUid(a);
            var preguntas = await repositorioUser.GetPreguntasAcertadasAsync(a, reto, usuario);
            if (preguntas != null)
            {
                // redimensionas el array
                Array.Resize(ref preguntas, preguntas.Length + 1);
                // agregar el nuevo valor al final del arreglo
                if (pregunta.Id != null) preguntas[^1] = (int)pregunta.Id;
                await repositorioUser.UpdatePreguntaAcertada(a, preguntas, usuario);
            }
            else
            {
                if (pregunta.Id != null)
                {
                    int[] preguntass = { (int) pregunta.Id };
                    await repositorioUser.UpdatePreguntaAcertada(a, preguntass, usuario);
                }
            }
        }
    }
}