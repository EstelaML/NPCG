using preguntaods.Entities;
using preguntaods.Persistencia.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Threading.Tasks;

namespace preguntaods.Services
{
    public class PreguntadosService : IPreguntadosService
    {
        private readonly RepositorioPregunta repositorioPre;
        private static List<Pregunta> preguntasBajas;
        private static List<Pregunta> preguntasMedias;
        private static List<Pregunta> preguntasAltas;

        public PreguntadosService()
        {
            repositorioPre = new RepositorioPregunta();
        }

        #region RetoPregunta

        public async Task InitList()
        {
            if (preguntasBajas == null) preguntasBajas = await repositorioPre.GetByDificultad(Pregunta.difBaja);
            if (preguntasMedias == null) preguntasMedias = await repositorioPre.GetByDificultad(Pregunta.difMedia);
            if (preguntasAltas == null) preguntasAltas = await repositorioPre.GetByDificultad(Pregunta.difAlta);
        }

        public async Task<Pregunta> SolicitarPregunta(int dificultad)
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

            return respuesta;
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