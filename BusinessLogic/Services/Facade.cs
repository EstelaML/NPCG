using preguntaods.BusinessLogic.Partida.Retos;
using preguntaods.Entities;
using preguntaods.Persistencia;
using preguntaods.Persistencia.Repository.impl;
using Supabase.Gotrue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preguntaods.BusinessLogic.Services
{
    public class Facade : IFacade
    {
        private readonly SingletonConexion conexion;
        private readonly RepositorioUsuario repositorioUser;
        private readonly RepositorioAhorcado repositorioAhorcado;
        private readonly RepositorioPregunta repositorioPregunta;
        private readonly Repository<Estadistica> repositorioEstadisticas;

        public Facade()
        {
            conexion = SingletonConexion.GetInstance();
            repositorioUser = new RepositorioUsuario();
            repositorioAhorcado = new RepositorioAhorcado();
            repositorioPregunta = new RepositorioPregunta();
            repositorioEstadisticas = new Repository<Estadistica>();
        }

        #region Usuario

        public async Task LoginAsync(string correo, string password)
        {
            var session = await conexion.Cliente.Auth.SignIn(correo, password);
            conexion.Usuario = session?.User;
        }

        public async Task LogoutAsync()
        {
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
            await repositorioUser.UpdateNombre(a,nombre);

        }

        public async Task NewUsuario(Usuario user)
        {
            await repositorioUser.Add(user);
        }

        public async Task GuardarPregunta(Reto reto)
        {
            // obtengo el id del usuario
            var usuario = await GetUsuarioLogged();
            if (usuario?.Id != null)
            {
                var id = (int)usuario.Id;
                switch (reto)
                {
                    // añado a la BD ese reto
                    case RetoPre _:
                        await repositorioPregunta.AñadirPreguntaRealizada(id, reto);
                        break;

                    case RetoAhorcado _:
                        await repositorioAhorcado.AñadirAhorcadoRealizado(id, reto);
                        break;
                }
            }
        }

        public async Task GuardarPreguntaFallada(Reto reto)
        {
            // obtengo el id del usuario
            var usuario = await GetUsuarioLogged();
            if (usuario?.Id != null)
            {
                switch (reto)
                {
                    // añado a la BD ese reto
                    case RetoPre _:
                        await repositorioPregunta.AñadirPreguntaFallada(reto);
                        break;

                    case RetoAhorcado _:
                        await repositorioAhorcado.AñadirAhorcadoFallado(reto);
                        break;
                }
            }
        }

        public async Task<bool> ComprobarUsuario(string nombre)
        {
            var respuesta = await repositorioUser.GetAll();

            return respuesta.All(u => !u.Nombre.Equals(nombre));
        }

        #endregion Usuario

        public async Task<List<Estadistica>> GetOrderedUsers(int cantidad)
        {
            var respuesta = await repositorioEstadisticas.GetAll();
            var listaUsuarios = respuesta.Select(estadisticas => new Estadistica { Nombre = estadisticas.Nombre, Puntuacion = estadisticas.Puntuacion })
                                         .OrderByDescending(estadisticas => estadisticas.Puntuacion)
                                         .Take(cantidad)
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
    }
}