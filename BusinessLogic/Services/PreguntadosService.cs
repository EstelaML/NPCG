using Android.Hardware.Camera2;
using Java.Util;
using preguntaods.Entities;
using preguntaods.Persistencia.Repository;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using preguntaods.Entities;
using preguntaods.Persistencia.Repository.impl;

namespace preguntaods.Services
{
    public class PreguntadosService : IPreguntadosService
    {
        object sync = new object();
        private readonly RepositorioPregunta repositorioPre;
        private readonly RepositorioAhorcado repositorioAhorcado;
        private static List<Pregunta> preguntasBajas;
        private static List<Pregunta> preguntasMedias;
        private static List<Pregunta> preguntasAltas;
        private static List<Ahorcado> ahorcadoBajo;
        private static List<Ahorcado> ahorcadoMedio;
        private static List<Ahorcado> ahorcadoAlto;

        public PreguntadosService()
        {
            repositorioPre = new RepositorioPregunta();
            repositorioAhorcado = new RepositorioAhorcado();
        }

        #region RetoPregunta

        public async Task InitAhorcadoList()
        {
            if (ahorcadoBajo == null)
            {
                ahorcadoBajo ??= await repositorioAhorcado.GetAhorcadoDificultad(Ahorcado.DifBaja);
            }
            if (ahorcadoMedio == null)
            {
                ahorcadoMedio ??= await repositorioAhorcado.GetAhorcadoDificultad(Ahorcado.DifMedia);
            }
            var p = (List<Ahorcado>) await repositorioAhorcado.GetAhorcadoDificultad(Ahorcado.DifAlta);
            lock (sync)
            {
                ahorcadoAlto ??= p;
            }
        }

        public async Task InitPreguntaList()
        {
            if (preguntasBajas == null)
            {
                preguntasBajas = await repositorioPre.GetByDificultad(Pregunta.DifBaja);
            }
            if (preguntasMedias == null)
            {
                preguntasMedias = await repositorioPre.GetByDificultad(Pregunta.DifMedia);
            }
            var p = await repositorioPre.GetByDificultad(Pregunta.DifAlta);
            lock (sync)
            {
                preguntasAltas ??= p;
            }
        }

        public Task<Ahorcado> SolicitarAhorcado(int dif) 
        {
            Ahorcado ahor = null;

            switch (dif)
            {
                case Ahorcado.DifBaja:
                    {
                        ahor = ahorcadoBajo.Last();
                        ahorcadoBajo.Remove(ahor);
                        break;
                    }
                case Ahorcado.DifMedia:
                    {
                        ahor = ahorcadoMedio.Last();
                        ahorcadoMedio.Remove(ahor);

                        break;
                    }
                case Ahorcado.DifAlta:
                    {
                        ahor = ahorcadoAlto.Last();
                        ahorcadoAlto.Remove(ahor);
                        break;
                    }
            }

            return Task.FromResult(ahor);
        }

        public Task<Pregunta> SolicitarPregunta(int dificultad)
        {
            Pregunta respuesta = null;

            switch (dificultad)
            {
                case Pregunta.DifBaja:
                    {
                        respuesta = preguntasBajas.Last();
                        preguntasBajas.Remove(respuesta);
                        break;
                    }
                case Pregunta.DifMedia:
                    {
                        respuesta = preguntasMedias.Last();
                        preguntasMedias.Remove(respuesta);

                        break;
                    }
                case Pregunta.DifAlta:
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