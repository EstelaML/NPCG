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

        public RepositorioAhorcado()
        {
            conexion = SingletonConexion.GetInstance();
        }

        public async Task<List<Ahorcado>> GetAhorcadoDificultad(int dificultad)
        {
            // OBTIENE TODOS LOS AHORCADOS QUE HAY
            var task1 = (conexion.Cliente.From<Ahorcado>().Where(x => x.Dificultad == dificultad).Get());
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
            return ahorcadosList;
        }

        public async Task AñadirAhorcadoRealizado(int id, Reto reto)
        {
            var model = new RetosRealizados
            {
                Usuario = id,
                Ahorcado = (reto as RetoAhorcado).GetAhorcado().Id,
                Pregunta = null,
            };
            await conexion.Cliente.From<RetosRealizados>().Insert(model);
        }
    }
}