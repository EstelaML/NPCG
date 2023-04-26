using preguntaods.BusinessLogic.Partida.Retos;
using preguntaods.Entities;
using preguntaods.Persistencia;
using preguntaods.Persistencia.Repository.impl;
using Supabase.Gotrue;
using System;
using System.Threading.Tasks;

namespace preguntaods.BusinessLogic.Services
{
    public class Facade : IFacade
    {
        private readonly SingletonConexion conexion;
        private readonly RepositorioUsuario repositorioUser;
        private readonly RepositorioAhorcado repositorioAhorcado;
        private readonly RepositorioPregunta repositorioPregunta;
        public Facade()
        {
            conexion = SingletonConexion.GetInstance();
            repositorioUser = new RepositorioUsuario();
            repositorioAhorcado = new RepositorioAhorcado();
            repositorioPregunta = new RepositorioPregunta();
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
            conexion.Usuario = session?.User;
            return session?.User;
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
                var usuario = await repositorioUser.GetUserByUUid(a);
                await repositorioUser.UpdatePuntosUsuario(a, usuario.Puntos, puntos);
            }
        }

        public async Task NewUsuario(Usuario user)
        {
            await repositorioUser.Add(user);
        }

        public async Task GuardarPregunta(Reto reto)
        {
            // obtengo el id del usuario
            var usuario = await GetUsuarioLogged();
            var id = (int)usuario?.Id;
            // añado a la BD ese reto
            if (reto is RetoPre)
            {
                await repositorioPregunta.AñadirPreguntaRealizada(id, reto);
            }
            else if (reto is RetoAhorcado)
            {
                await repositorioAhorcado.AñadirAhorcadoRealizado(id, reto);
            }
        }

        #endregion Usuario
    }
}