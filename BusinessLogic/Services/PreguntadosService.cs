using Android.Hardware.Camera2;
using Java.Util;
using preguntaods.Entities;
using preguntaods.Persistencia.Repository;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace preguntaods.Services
{
    public class PreguntadosService : IPreguntadosService
    {
        object sync = new object();
        private readonly RepositorioPregunta repositorioPre;
        private readonly Repository<Ahorcado> repositorioAhorcado;
        private static List<Pregunta> preguntasBajas;
        private static List<Pregunta> preguntasMedias;
        private static List<Pregunta> preguntasAltas;
        private static List<Ahorcado> ahorcadoList;

        public PreguntadosService()
        {
            repositorioPre = new RepositorioPregunta();
            repositorioAhorcado = new Repository<Ahorcado>();
        }

        #region RetoPregunta

        public async Task InitAhorcadoList()
        {
            if (ahorcadoList == null)
            {
                ahorcadoList = (List<Ahorcado>) await repositorioAhorcado.GetAll();
            }
        }

        public async Task InitPreguntaList()
        {
            if (preguntasBajas == null)
            {
                preguntasBajas = await repositorioPre.GetByDificultad(Pregunta.difBaja);
            }
            if (preguntasMedias == null)
            {
                preguntasMedias = await repositorioPre.GetByDificultad(Pregunta.difMedia);
            }
            var p = await repositorioPre.GetByDificultad(Pregunta.difAlta);
            lock (sync)
            {
                preguntasAltas ??= p;
            }
        }

        public Task<Ahorcado> SolicitarAhorcado() 
        {
            Random rmd = new Random();
            int indiceAleatorio = rmd.NextInt(ahorcadoList.Count);
            var valorAleatorio = ahorcadoList[indiceAleatorio];
            return Task.FromResult(valorAleatorio);
        }

        public Task<Pregunta> SolicitarPregunta(int dificultad)
        {
            Pregunta respuesta = null;

            switch (dificultad)
            {
                case Pregunta.difBaja:
                    {
                        respuesta = preguntasBajas.Last();
                        preguntasBajas.Remove(respuesta);
                        break;
                    }
                case Pregunta.difMedia:
                    {
                        respuesta = preguntasMedias.Last();
                        preguntasMedias.Remove(respuesta);

                        break;
                    }
                case Pregunta.difAlta:
                    {
                        respuesta = preguntasAltas.Last();
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