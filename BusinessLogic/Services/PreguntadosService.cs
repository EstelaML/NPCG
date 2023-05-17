using System;
using preguntaods.Entities;
using preguntaods.Persistencia.Repository.impl;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using preguntaods.BusinessLogic.Retos;
using Random = Java.Util.Random;
using preguntaods.Persistencia;
using Supabase.Gotrue;

namespace preguntaods.BusinessLogic.Services
{
    public class PreguntadosService : IPreguntadosService
    {
        private readonly object sync = new object();

        private readonly SingletonConexion conexion;
        private readonly RepositorioPregunta repositorioPregunta;
        private readonly RepositorioAhorcado repositorioAhorcado;
        private readonly Repository<Estadistica> repositorioEstadisticas;
        private readonly RepositorioUsuario repositorioUser;

        private static List<Pregunta> _preguntasBajas;
        private static List<Pregunta> _preguntasMedias;
        private static List<Pregunta> _preguntasAltas;
        private static List<Ahorcado> _ahorcadoBajo;
        private static List<Ahorcado> _ahorcadoMedio;
        private static List<Ahorcado> _ahorcadoAlto;

        public PreguntadosService()
        {
            conexion = SingletonConexion.GetInstance();
            repositorioPregunta = new RepositorioPregunta();
            repositorioAhorcado = new RepositorioAhorcado();
            repositorioEstadisticas = new Repository<Estadistica>();
            repositorioUser = new RepositorioUsuario();
        }

        #region RetoPregunta

        public async Task InitPreguntaList()
        {
            _preguntasBajas ??= await repositorioPregunta.GetByDificultad(Pregunta.DifBaja);
            _preguntasMedias ??= await repositorioPregunta.GetByDificultad(Pregunta.DifMedia);
            var p = await repositorioPregunta.GetByDificultad(Pregunta.DifAlta);
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
                        var rnd = new Random();
                        var indiceAleatorio = rnd.NextInt(_preguntasBajas.Count);
                        respuesta = _preguntasBajas[indiceAleatorio];
                        _preguntasBajas.Remove(respuesta);
                        break;
                    }
                case Pregunta.DifMedia:
                    {
                        var rnd = new Random();
                        var indiceAleatorio = rnd.NextInt(_preguntasMedias.Count);
                        respuesta = _preguntasMedias[indiceAleatorio];
                        _preguntasMedias.Remove(respuesta);

                        break;
                    }
                case Pregunta.DifAlta:
                    {
                        var rnd = new Random();
                        var indiceAleatorio = rnd.NextInt(_preguntasAltas.Count);
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
                        var rnd = new Random();
                        var indiceAleatorio = rnd.NextInt(_ahorcadoBajo.Count);
                        ahorca = _ahorcadoBajo[indiceAleatorio];
                        _ahorcadoBajo.Remove(ahorca);
                        break;
                    }
                case Ahorcado.DifMedia:
                    {
                        var rnd = new Random();
                        var indiceAleatorio = rnd.NextInt(_ahorcadoMedio.Count);
                        ahorca = _ahorcadoMedio[indiceAleatorio];
                        _ahorcadoMedio.Remove(ahorca);

                        break;
                    }
                case Ahorcado.DifAlta:
                    {
                        var rnd = new Random();
                        var indiceAleatorio = rnd.NextInt(_ahorcadoAlto.Count);
                        ahorca = _ahorcadoAlto[indiceAleatorio];
                        _ahorcadoAlto.Remove(ahorca);
                        break;
                    }
            }

            return Task.FromResult(ahorca);
        }

        #endregion RetoAhorcado

        #region Estadisticas

        public async Task<List<Estadistica>> GetAllUsersOrdered()
        {
            var respuesta = await repositorioEstadisticas.GetAll();
            var listaUsuarios = respuesta.Select(estadisticas => new Estadistica { Nombre = estadisticas.Nombre, Puntuacion = estadisticas.Puntuacion })
                .OrderByDescending(estadisticas => estadisticas.Puntuacion)
                .ToList();

            return listaUsuarios;
        }

        public async Task CrearEstadisticas(Usuario user)
        {
            var aux = Array.Empty<int>();
            var a = new Estadistica(user.Uuid, 0, aux, aux, user.Nombre);
            await repositorioEstadisticas.Add(a);
        }

        public async Task<Estadistica> PedirEstadisticas(string uuid)
        {
            var respuesto = await repositorioUser.GetEstadisticasByUuid(uuid);
            return respuesto;
        }

        public async Task GuardarTiempo()
        {
            // calculas el tiempo que lleva esta vez
            var s = conexion.Cliente.Auth.CurrentSession;
            if (s != null)
            {
                var created = s.CreatedAt;
                var start = created.TimeOfDay;
                var now = DateTime.Now.TimeOfDay;
                var dif = now - start;
                await repositorioUser.UpdateTimeUsedAsync(dif);
            }
        }

        #endregion

        #region Usuario

        public async Task LoginAsync(string correo, string password)
        {
            var session = await conexion.Cliente.Auth.SignIn(correo, password);
            conexion.Usuario = session?.User;
        }

        public async Task LogoutAsync()
        {
            await GuardarTiempo();
            await conexion.Cliente.Auth.SignOut();
        }

        public async Task<User> SignUpAsync(string correo, string password)
        {
            var session = await conexion.Cliente.Auth.SignUp(correo, password);
            if (session?.User?.AppMetadata == null) return null;
            conexion.Usuario = session.User;
            return session.User;
        }

        public async Task<Usuario> GetUsuarioLogged()
        {
            if (conexion.Usuario != null)
            {
                var a = conexion.Usuario.Id;
                var respuesta = await repositorioUser.GetUserByUUid(a);
                return respuesta;
            }
            else
            {
                return null;
            }
        }

        public async Task UpdatePuntos(int puntos)
        {
            if (conexion.Usuario != null)
            {
                var a = conexion.Usuario.Id;
                var estadisticas = await repositorioUser.GetEstadisticasByUuid(a);
                await repositorioUser.UpdatePuntosUsuario(a, estadisticas.Puntuacion, puntos);
            }
        }

        public async Task CambiarNombre(string nombre)
        {
            var a = conexion.Usuario.Id;
            await repositorioUser.UpdateNombre(a, nombre);
        }

        public async Task CambiarFoto(string uuid, byte[] foto)
        {
            await repositorioUser.UpdateFoto(uuid, foto);
        }

        public async Task NewUsuario(Usuario user)
        {
            await repositorioUser.Add(user);
        }

        public async Task GuardarPregunta(IReto reto)
        {
            switch (reto.Type)
            {
                // añado a la BD ese reto
                case IReto.TypePregunta:
                    await repositorioPregunta.AñadirPreguntaRealizada(((RetoPre)reto).GetPregunta());
                    break;

                case IReto.TypeAhorcado:
                    await repositorioAhorcado.AñadirAhorcadoRealizado(((RetoAhorcado)reto).GetAhorcado());
                    break;
            }
        }

        public async Task GuardarPreguntaFallada(IReto reto)
        {
            switch (reto.Type)
            {
                // añado a la BD ese reto
                case IReto.TypePregunta:
                    await repositorioPregunta.AñadirPreguntaFallada(reto);
                    break;

                case IReto.TypeAhorcado:
                    await repositorioAhorcado.AñadirAhorcadoFallado(reto);
                    break;
            }
        }

        public async Task<bool> ComprobarUsuario(string nombre)
        {
            var respuesta = await repositorioUser.GetAll();

            return respuesta.All(u => !u.Nombre.Equals(nombre));
        }

        #endregion
    }
}