using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Java.Util;
using preguntaods.BusinessLogic.Partida.Retos;
using preguntaods.Entities;

namespace preguntaods.Persistencia.Repository.impl
{
    public class RepositorioUsuario : Repository<Usuario>
    {
        private SingletonConexion conexion;

        public RepositorioUsuario()
        {
            conexion = SingletonConexion.GetInstance();
        }

        public async Task<Usuario> GetUserByUUid(string u)
        {
            var response = await conexion.Cliente
                .From<Usuario>()
                .Where(x => x.Uuid == u)
                .Single();
            return response;
        }

        public async Task UpdatePuntosUsuario(string uuid, int puntosA, int puntosS)
        {
            int p = puntosA + puntosS;
            var response2 = await conexion.Cliente
                        .From<Usuario>()
                        .Where(x => x.Uuid == uuid)
                        .Set(x => x.Puntos, p)
                        .Update();
        }

        public async Task UpdatePreguntaAcertada(string a, int[] preguntas)
        {
            var update = await conexion.Cliente
                     .From<Usuario>()
                     .Where(x => x.Uuid == a)
                     .Set(x => x.PreguntasRealizadas, preguntas)
                     .Update();
        }

        public async Task<int[]> GetPreguntasAcertadasAsync(string a)
        {
            var res = await conexion.Cliente
                .From<Usuario>()
                .Where(x => x.Uuid == a).Single();
            return res.PreguntasRealizadas;
        }

        public async Task<int[]> GetRetoAcertado(string a, Reto reto) {
            var usuario = await conexion.Cliente.From<Usuario>().Where(x => x.Uuid == a).Single();
            int id = (int)usuario.Id;
            if (reto is RetoPre) {
                // solicitas los RetosRealizados pero solo la columna de preguntas donde preguntas != null
                var retosPreguntas = await conexion.Cliente
                    .From<RetosRealizados>()
                    .Where(x => x.Usuario == id)
                    .Where(c => c.Pregunta != null).Get();
                var listaPreguntasRealizadas = retosPreguntas.Models;
                var respuesta = new int[listaPreguntasRealizadas.Count];
                for (int i = 0; i < listaPreguntasRealizadas.Count; i++)
                {
                    respuesta[i] = (int)listaPreguntasRealizadas[i].Pregunta;
                }
                return respuesta;
            } else if (reto is RetoAhorcado) 
            {
                var retosPreguntas = await conexion.Cliente
                     .From<RetosRealizados>()
                     .Where(x => x.Usuario == id)
                     .Where(c => c.Ahorcado != null).Get();
                var listaPreguntasRealizadas = retosPreguntas.Models;
                var respuesta = new int[listaPreguntasRealizadas.Count];
                for (int i = 0; i < listaPreguntasRealizadas.Count; i++)
                {
                    respuesta[i] = (int)listaPreguntasRealizadas[i].Pregunta;
                }
                return respuesta;
            } //... diferentes retos
            return null;
        }
    }
}