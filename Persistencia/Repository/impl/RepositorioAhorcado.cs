using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Service.Notification;
using preguntaods.BusinessLogic.Partida.Retos;
using preguntaods.Entities;

namespace preguntaods.Persistencia.Repository.impl
{
    public class RepositorioAhorcado : Repository<Ahorcado>
    {
        private SingletonConexion conexion;
        private readonly RepositorioUsuario repositorioUser;
        public RepositorioAhorcado()
        {
            conexion = SingletonConexion.GetInstance();
            repositorioUser = new RepositorioUsuario();
        }

        public async Task<List<Ahorcado>> GetAhorcadoDificultad(int dificultad)
        {
            // OBTIENE TODOS LOS AHORCADOS QUE HAY
           /* var task1 = (conexion.Cliente.From<Ahorcado>().Where(x => x.Dificultad == dificultad).Get());
            // COGEMOS EL USUARIO
            var uuid = conexion.Usuario.Id;
            var usuario = await conexion.Cliente.From<Usuario>().Where(x => x.Uuid == uuid).Single();
            int id = (int)usuario?.Id;
            // COGEMOS UNA LISTA DE LOS RETOS AHORCADO QUE HAYA HECHO
            var task2 = conexion.Cliente.From<RetosRealizados>().Where(x => x.Usuario == id).Where(c => c.Ahorcado != null).Get();
            List<Task> tareas = new List<Task> { task1, task2 };
            await Task.WhenAll(tareas);

            var listaRetosRealizados = task2.Result.Models.ToList();
            var ahorcadosList = task1.Result.Models.ToList();

            List<int> listaPreguntasRealizadas = listaRetosRealizados.Select(r => (int)r.Pregunta).ToList();
            // LE QUITAMOS A ahorcados LAS listaPreguntasRealizadas
            ahorcadosList = ahorcadosList.Where(pregunta => !listaPreguntasRealizadas.Contains((int)pregunta.Id)).ToList();
            return ahorcadosList;*/




            var user = await repositorioUser.GetUserByUUid(conexion.Usuario.Id);
            var id = (int)user?.Id;
            var task1 = (conexion.Cliente.From<RetosRealizados>().Where(x => x.Usuario == id).Single());
            var task2 = (conexion.Cliente.From<Ahorcado>().Where(x => x.Dificultad == dificultad).Get());
            List<Task> tareas = new List<Task> { task1, task2 };
            await Task.WhenAll(tareas);

            var retos = task1.Result;
            var response = task2.Result;
            var preguntas = response.Models.ToList();
            var preguntasHechas = retos?.Ahorcado2?.ToList();
            preguntas = preguntasHechas != null ? preguntas.Where(pregunta => !preguntasHechas.Contains((int)pregunta.Id)).ToList() : preguntas;

            return preguntas;
        }

        public async Task AñadirAhorcadoRealizado(int id, Reto reto)
        {
            // cogemos del usuario las preguntas acertadas ya
            var pregunta = (reto as RetoAhorcado).GetAhorcado();
            var a = conexion.Usuario.Id;
            var usuario = await repositorioUser.GetUserByUUid(a);
            var preguntas = await repositorioUser.GetPreguntasAcertadasAsync(a, reto, usuario);
            if (preguntas != null)
            {
                // redimensionas el array
                Array.Resize(ref preguntas, preguntas.Length + 1);
                // agregar el nuevo valor al final del arreglo
                preguntas[^1] = (int)pregunta.Id;
                await repositorioUser.UpdateAhorcadoAcertado(a, preguntas, usuario);
            }
            else
            {
                int[] preguntass = { (int)pregunta.Id };
                await repositorioUser.UpdateAhorcadoAcertado(a, preguntass, usuario);
            }
        }
    }
}