using System;
using System.Threading.Tasks;
using preguntaods.BusinessLogic.Partida.Retos;
using preguntaods.Entities;
using preguntaods.Persistencia;
using preguntaods.Persistencia.Repository.impl;
using Supabase.Gotrue;

namespace preguntaods.BusinessLogic.Services
{
    public class Facade : IFacade
    {
        private readonly SingletonConexion conexion;
        private readonly RepositorioUsuario repositorioUser;

        public Facade()
        {
            conexion = SingletonConexion.GetInstance();
            repositorioUser = new RepositorioUsuario();
        }

        #region Usuario

        public async Task LoginAsync(string correo, string password)
        {
            var session = await conexion.Cliente.Auth.SignIn(correo, password);
            conexion.Usuario = session.User;
        }

        public async Task LogoutAsync()
        {
            await conexion.Cliente.Auth.SignOut();
        }

        public async Task<User> SignUpAsync(string correo, string password)
        {
            var session = await conexion.Cliente.Auth.SignUp(correo, password); 
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
            var pregunta = (reto as RetoPre).GetPregunta();
            var a = conexion.Usuario.Id;
            var preguntas = await repositorioUser.GetPreguntasAcertadasAsync(a);
            if (preguntas != null)
            {
                // redimensionas el array
                Array.Resize(ref preguntas, preguntas.Length + 1);
                // agregar el nuevo valor al final del arreglo
                preguntas[^1] = (int)pregunta.Id;
                await repositorioUser.UpdatePreguntaAcertada(a, preguntas);
            }
            else
            {
                int[] preguntass = { (int)pregunta.Id };
                await repositorioUser.UpdatePreguntaAcertada(a, preguntass);
            }
        }

        #endregion Usuario
    }
}