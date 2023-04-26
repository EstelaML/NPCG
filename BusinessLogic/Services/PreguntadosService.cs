using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using preguntaods.BusinessLogic.Partida.Retos;
using preguntaods.Entities;
using preguntaods.Persistencia.Repository.impl;

namespace preguntaods.BusinessLogic.Services
{
    public class PreguntadosService : IPreguntadosService
    {
        object sync = new object();
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

        public async Task InitAhorcadoList()
        {
            _ahorcadoBajo ??= await repositorioAhorcado.GetAhorcadoDificultad(Ahorcado.DifBaja);
            _ahorcadoMedio ??= await repositorioAhorcado.GetAhorcadoDificultad(Ahorcado.DifMedia);
            var p = (List<Ahorcado>) await repositorioAhorcado.GetAhorcadoDificultad(Ahorcado.DifAlta);
            lock (sync)
            {
                _ahorcadoAlto ??= p;
            }
        }

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

        public Task<Ahorcado> SolicitarAhorcado(int dif) 
        {
            Ahorcado ahorca = null;

            switch (dif)
            {
                case Ahorcado.DifBaja:
                    {
                        ahorca = _ahorcadoBajo.Last();
                        _ahorcadoBajo.Remove(ahorca);
                        break;
                    }
                case Ahorcado.DifMedia:
                    {
                        ahorca = _ahorcadoMedio.Last();
                        _ahorcadoMedio.Remove(ahorca);

                        break;
                    }
                case Ahorcado.DifAlta:
                    {
                        ahorca = _ahorcadoAlto.Last();
                        _ahorcadoAlto.Remove(ahorca);
                        break;
                    }
            }

            return Task.FromResult(ahorca);
        }

        public Task<Pregunta> SolicitarPregunta(int dificultad)
        {
            Pregunta respuesta = null;

            switch (dificultad)
            {
                case Pregunta.DifBaja:
                    {
                        respuesta = _preguntasBajas.Last();
                        _preguntasBajas.Remove(respuesta);
                        break;
                    }
                case Pregunta.DifMedia:
                    {
                        respuesta = _preguntasMedias.Last();
                        _preguntasMedias.Remove(respuesta);

                        break;
                    }
                case Pregunta.DifAlta:
                    {
                        respuesta = _preguntasAltas.Last();
                        _preguntasAltas.Remove(respuesta);
                        break;
                    }
            }

            return Task.FromResult(respuesta);
        }
        #endregion RetoPregunta
    }
}