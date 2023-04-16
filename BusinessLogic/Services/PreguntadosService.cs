using Java.Util;
using preguntaods.Entities;
using preguntaods.Persistencia.Repository;
using Supabase.Gotrue;
using System;
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

        #region ODS

        #endregion

        #region Reto
        public async Task<Pregunta> SetDif(int orden, List<Reto> retos)
        {

            if (orden < 4 || orden == 10)
            {

                return await SetPregunta(1, retos);

            }
            else if (orden < 7 || orden == 11)
            {

                return await SetPregunta(2, retos);

            }
            else { return await SetPregunta(3, retos); }

        }

        private async Task<Pregunta> SetPregunta(int dif, List<Reto> retos)
        {
            switch (dif)
            {
                case 1:
                    {

                        return await SolicitarPregunta(Pregunta.difBaja, retos);

                        break;
                    }
                case 2:
                    {
                        return await SolicitarPregunta(Pregunta.difMedia, retos);
                        break;
                    }
                case 3:
                    {
                        return await SolicitarPregunta(Pregunta.difAlta, retos);
                        break;
                    }
            }
            return null;
        }

        #endregion




        #region RetoPregunta
        public async Task<Pregunta> SolicitarPregunta(int dificultad, List<Reto> retos)
        { 
            List<Pregunta> respuesta = await repositorioPre.GetByDificultad(dificultad, retos);
            return respuesta.FirstOrDefault();
        }

        public Pregunta PreguntaAleatoria(List<Pregunta> respuesta, List<Reto> retos) {
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

        #endregion
    }
}