using preguntaods.Entities;
using preguntaods.Persistencia;
using preguntaods.Persistencia.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preguntaods.Services
{
    public class PreguntadosService : IPreguntadosService
    {
        private readonly RepositorioPregunta repositorioPre;
        private readonly RepositorioUsuario repositorioUser;
        private static List<Pregunta> preguntasBajas;
        private static List<Pregunta> preguntasMedias;
        private static List<Pregunta> preguntasAltas;
        private Usuario usuario;

        public PreguntadosService()
        {
            repositorioPre = new RepositorioPregunta();
            repositorioUser = new RepositorioUsuario();
        }

        #region RetoPregunta

        public async Task InitPreguntaList()
        {
            if (usuario == null) usuario = await repositorioUser.GetUserByUUid(SingletonConexion.GetInstance().usuario.Id);

            if (preguntasBajas == null)
            {
                preguntasBajas = await repositorioPre.GetByDificultad(Pregunta.difBaja);
                preguntasBajas = ListFilter(preguntasBajas, usuario);
            }
            if (preguntasMedias == null)
            {
                preguntasMedias = await repositorioPre.GetByDificultad(Pregunta.difMedia);
                preguntasMedias = ListFilter(preguntasMedias, usuario);
            }
            if (preguntasAltas == null)
            {
                preguntasAltas = await repositorioPre.GetByDificultad(Pregunta.difAlta);
                preguntasAltas = ListFilter(preguntasAltas, usuario);
            }
        }

        public List<Pregunta> ListFilter(List<Pregunta> list, Usuario usuario)
        {
            int[] retosID = usuario.PreguntasRealizadas;

            var preguntasPosibles = list;

            if (retosID != null)
            {
                List<int> preguntasRealizadas = retosID.ToList();
                // elimino de la lista de preguntas ya realizadas
                preguntasPosibles = list.Where(x => x.Id.HasValue && !preguntasRealizadas.Contains((int)x.Id)).Select(x => x).ToList();
            }

            return preguntasPosibles;
        }

        public Task<Pregunta> SolicitarPregunta(int dificultad)
        {
            Pregunta respuesta = null;

            switch (dificultad)
            {
                case Pregunta.difBaja:
                    {
                        respuesta = preguntasBajas.FirstOrDefault();
                        preguntasBajas.Remove(respuesta);

                        break;
                    }
                case Pregunta.difMedia:
                    {
                        respuesta = preguntasMedias.FirstOrDefault();
                        preguntasMedias.Remove(respuesta);

                        break;
                    }
                case Pregunta.difAlta:
                    {
                        respuesta = preguntasAltas.FirstOrDefault();
                        preguntasAltas.Remove(respuesta);

                        break;
                    }
            }

            return Task.FromResult(respuesta);
        }

        public Pregunta PreguntaAleatoria(List<Pregunta> respuesta, List<Reto> retos)
        {
            System.Random random = new System.Random();

            // Obtener un índice aleatorio dentro del rango de la lista de preguntas
            int indiceAleatorio = random.Next(respuesta.Count);
            Pregunta preguntaAleatoria = respuesta[indiceAleatorio];

            if (retos != null)
            {
                if (!retos.Any(reto => (reto as RetoPre).GetPregunta().Enunciado == preguntaAleatoria.Enunciado))
                {
                    return preguntaAleatoria;
                }
                else
                {
                    return null;
                }
            }
            return respuesta[random.Next(respuesta.Count)];
        }

        #endregion RetoPregunta
    }
}