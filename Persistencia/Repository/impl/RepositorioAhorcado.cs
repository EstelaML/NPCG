using preguntaods.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preguntaods.Persistencia.Repository.impl
{
    public class RepositorioAhorcado : Repository<Ahorcado>
    {
        private readonly ConexionBD conexion;
        private readonly RepositorioUsuario repositorioUser;

        public RepositorioAhorcado()
        {
            conexion = ConexionBD.GetInstance();
            repositorioUser = new RepositorioUsuario();
        }

        public async Task<List<Ahorcado>> GetAhorcadoDificultad(int dificultad)
        {
            var user = await repositorioUser.GetUserByUUid(conexion.UsuarioBD.Id);
            if (user?.Id == null) return null;
            var id = (int)user.Id;
            var task1 = (conexion.Cliente.From<RetosRealizados>().Where(x => x.Usuario == id).Single());
            var task2 = (conexion.Cliente.From<Ahorcado>().Where(x => x.Dificultad == dificultad).Get());
            var tareas = new List<Task> { task1, task2 };
            await Task.WhenAll(tareas);

            var retos = task1.Result;
            var response = task2.Result;
            var preguntas = response.Models.ToList();
            var preguntasHechas = retos?.AhorcadosRealizados?.ToList();
            preguntas = preguntasHechas != null ? preguntas.Where(pregunta => pregunta.Id != null && !preguntasHechas.Contains((int)pregunta.Id)).ToList() : preguntas;

            if (preguntas.Count >= 5) return preguntas;
            _ = repositorioUser.UpdateAhorcadoAcertado("", null, user);
            return response.Models.ToList();
        }

        public async Task<List<Ahorcado>> GetAhorcadosByODS(int ods)
        {

            var preguntas = await conexion.Cliente.From<Ahorcado>().Where(x => x.OdsRelacionada == ods).Get();
            return preguntas.Models.ToList();

        }

        public async Task AñadirAhorcadoRealizado(Ahorcado pregunta)
        {
            // cogemos del usuario las preguntas acertadas ya
            var a = conexion.UsuarioBD.Id;
            var usuario = await repositorioUser.GetUserByUUid(a);
            var preguntas = await repositorioUser.GetAhorcadosAcertadosAsync(usuario);
            var retosAcertados = await repositorioUser.GetRetosAcertadosAsync(usuario);
            if (preguntas != null)
            {
                // redimensionas el array
                Array.Resize(ref preguntas, preguntas.Length + 1);
                // agregar el nuevo valor al final del arreglo
                if (pregunta.Id != null) preguntas[^1] = (int)pregunta.Id;
                await repositorioUser.UpdateAhorcadoAcertado(a, preguntas, usuario);
            }
            else
            {
                if (pregunta.Id != null)
                {
                    int[] preguntass = { (int)pregunta.Id };
                    await repositorioUser.UpdateAhorcadoAcertado(a, preguntass, usuario);
                }
            }

            if (retosAcertados != null)
            {
                // redimensionas el array
                Array.Resize(ref retosAcertados, retosAcertados.Length + 1);
                // agregar el nuevo valor al final del arreglo
                if (pregunta.Id != null) retosAcertados[^1] = (int)pregunta.Id;
                await repositorioUser.UpdateRetoAcertado(a, retosAcertados, usuario);
            }
            else
            {
                if (pregunta.Id != null)
                {
                    int[] retosAcertadoss = { (int)pregunta.Id };
                    await repositorioUser.UpdateRetoAcertado(a, retosAcertadoss, usuario);
                }
            }
        }

        public async Task AñadirAhorcadoFallado(Ahorcado pregunta)
        {
            var a = conexion.UsuarioBD.Id;
            var usuario = await repositorioUser.GetUserByUUid(a);
            var retosFallados = await repositorioUser.GetRetosFalladosAsync(usuario);
            if (retosFallados != null)
            {
                // redimensionas el array
                Array.Resize(ref retosFallados, retosFallados.Length + 1);
                // agregar el nuevo valor al final del arreglo
                if (pregunta.Id != null) retosFallados[^1] = (int)pregunta.Id;
                await repositorioUser.UpdateRetoFallado(retosFallados, usuario);
            }
            else
            {
                if (pregunta.Id != null)
                {
                    int[] retosFalladoss = { (int)pregunta.Id };
                    await repositorioUser.UpdateRetoFallado(retosFalladoss, usuario);
                }
            }
        }
    }
}