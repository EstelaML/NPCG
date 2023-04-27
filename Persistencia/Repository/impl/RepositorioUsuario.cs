using System;
using System.Threading.Tasks;
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
            await conexion.Cliente
                        .From<Usuario>()
                        .Where(x => x.Uuid == uuid)
                        .Set(x => x.Puntos, p)
                        .Update();
        }

        public async Task UpdatePreguntaAcertada(string a, int[] preguntas, Usuario usuario)
        {
            preguntas ??= Array.Empty<int>();
            if (usuario.Id != null)
            {
                var id = (int)usuario.Id;
                await conexion.Cliente
                    .From<RetosRealizados>()
                    .Where(x => x.Usuario == id)
                    .Set(x => x.PreguntasRealizadas, preguntas)
                    .Update();
            }
        }

        public async Task UpdateAhorcadoAcertado(string a, int[] preguntas, Usuario usuario)
        {
            preguntas ??= Array.Empty<int>();
            if (usuario.Id != null)
            {
                var id = (int)usuario.Id;
                await conexion.Cliente
                    .From<RetosRealizados>()
                    .Where(x => x.Usuario == id)
                    .Set(x => x.AhorcadosRealizados, preguntas)
                    .Update();
            }
        }

        public async Task<int[]> GetPreguntasAcertadasAsync(string a, Reto reto, Usuario usuario)
        {
            if (usuario.Id == null) return null;
            var id = (int)usuario.Id;
            var respuesta = await conexion.Cliente.From<RetosRealizados>().Where(x => x.Usuario == id).Single();
            if (respuesta == null) 
            {
                var inser = new RetosRealizados((int)usuario.Id, null, null);
                await conexion.Cliente.From<RetosRealizados>().Insert(inser);
                return null;
            } 
            if (reto is RetoPre)
            {
                return respuesta.PreguntasRealizadas;
            }
            else if (reto is RetoAhorcado) 
            {
                return respuesta.AhorcadosRealizados;
            }

            return null;
        }

        
        public async Task<int[]> GetRetoAcertado(string a, Reto reto) {
            return null;
            /*var usuario = await conexion.Cliente.From<Usuario>().Where(x => x.Uuid == a).Single();
            int id = (int)usuario.Id;
            if (reto is RetoPre) {
                // solicitas los RetosRealizados pero solo la columna de preguntas donde preguntas != null
                var retosPreguntas = await conexion.Cliente
                    .From<RetosRealizados>()
                    .Where(x => x.Usuario == id)
                    .Where(c => c.PreguntasRealizadas != null).Get();
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
            return null;*/
        }
    }
}