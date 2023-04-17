using preguntaods.Entities;
using preguntaods.Persistencia.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preguntaods.Services
{
    public class PreguntadosService : IPreguntadosService
    {
        private readonly RepositorioPregunta repositorioPre;
        private List<Pregunta> preguntas;
        private List<Pregunta> preguntasMedias;

        public PreguntadosService()
        {
            repositorioPre = new RepositorioPregunta();
        }

        #region RetoPregunta

        public async Task<Pregunta> SolicitarPregunta(int dificultad, List<Reto> retos)
        {
            List<Pregunta> respuesta = await repositorioPre.GetByDificultad(dificultad, retos);
            return respuesta.FirstOrDefault();
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