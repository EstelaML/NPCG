using Java.Util;
using preguntaods.Entities;
using preguntaods.Persistencia.Repository.impl;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preguntaods.BusinessLogic.Services
{
    public class PreguntadosService : IPreguntadosService
    {
        private readonly object sync = new object();
        private readonly RepositorioPregunta repositorioPre;
        private readonly RepositorioAhorcado repositorioAhorcado;
        private static List<Pregunta> _preguntasBajas;
        private static List<Pregunta> _preguntasMedias;
        private static List<Pregunta> _preguntasAltas;
        private static List<Ahorcado> _ahorcadoBajo;
        private static List<Ahorcado> _ahorcadoMedio;
        private static List<Ahorcado> _ahorcadoAlto;

        public PreguntadosService()
        {
            repositorioPre = new RepositorioPregunta();
            repositorioAhorcado = new RepositorioAhorcado();
        }

        #region RetoPregunta


        public async Task InitPreguntaList()
        {
            _preguntasBajas ??= await repositorioPre.GetByDificultad(Pregunta.DifBaja);
            _preguntasMedias ??= await repositorioPre.GetByDificultad(Pregunta.DifMedia);
            var p = await repositorioPre.GetByDificultad(Pregunta.DifAlta);
            lock (sync)
            {
                _preguntasAltas ??= p;
            }
        }        

        public Task<Pregunta> SolicitarPregunta(int dificultad)
        {
            Pregunta respuesta = null;

            switch (dificultad)
            {
                case Pregunta.DifBaja:
                    {
                        Random rnd = new Random();
                        int indiceAleatorio = rnd.NextInt(_preguntasBajas.Count);
                        respuesta = _preguntasBajas[indiceAleatorio];
                        _preguntasBajas.Remove(respuesta);
                        break;
                    }
                case Pregunta.DifMedia:
                    {
                        Random rnd = new Random();
                        int indiceAleatorio = rnd.NextInt(_preguntasMedias.Count);
                        respuesta = _preguntasMedias[indiceAleatorio];
                        _preguntasMedias.Remove(respuesta);

                        break;
                    }
                case Pregunta.DifAlta:
                    {
                        Random rnd = new Random();
                        int indiceAleatorio = rnd.NextInt(_preguntasAltas.Count);
                        respuesta = _preguntasAltas[indiceAleatorio];
                        _preguntasAltas.Remove(respuesta);
                        break;
                    }
            }

            return Task.FromResult(respuesta);
        }

        #endregion RetoPregunta

        #region RetoAhorcado

        public async Task InitAhorcadoList()
        {
            _ahorcadoBajo ??= await repositorioAhorcado.GetAhorcadoDificultad(Ahorcado.DifBaja);
            _ahorcadoMedio ??= await repositorioAhorcado.GetAhorcadoDificultad(Ahorcado.DifMedia);
            var p = await repositorioAhorcado.GetAhorcadoDificultad(Ahorcado.DifAlta);
            lock (sync)
            {
                _ahorcadoAlto ??= p;
            }
        }

        public Task<Ahorcado> SolicitarAhorcado(int dif)
        {
            Ahorcado ahorca = null;

            switch (dif)
            {
                case Ahorcado.DifBaja:
                    {
                        Random rnd = new Random();
                        int indiceAleatorio = rnd.NextInt(_ahorcadoBajo.Count);
                        ahorca = _ahorcadoBajo[indiceAleatorio];
                        _ahorcadoBajo.Remove(ahorca);
                        break;
                    }
                case Ahorcado.DifMedia:
                    {
                        Random rnd = new Random();
                        int indiceAleatorio = rnd.NextInt(_ahorcadoMedio.Count);
                        ahorca = _ahorcadoMedio[indiceAleatorio];
                        _ahorcadoMedio.Remove(ahorca);

                        break;
                    }
                case Ahorcado.DifAlta:
                    {
                        Random rnd = new Random();
                        int indiceAleatorio = rnd.NextInt(_ahorcadoAlto.Count);
                        ahorca = _ahorcadoAlto[indiceAleatorio];
                        _ahorcadoAlto.Remove(ahorca);
                        break;
                    }
            }

            return Task.FromResult(ahorca);
        }

        #endregion RetoAhorcado
    }
}