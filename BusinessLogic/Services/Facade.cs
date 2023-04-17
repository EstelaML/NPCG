using preguntaods.Entities;
using preguntaods.Persistencia;
using preguntaods.Persistencia.Repository;
using Supabase.Gotrue;
using System;
using System.Threading.Tasks;

namespace preguntaods.Services
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
            var session = await conexion.cliente.Auth.SignIn(correo, password);
            conexion.usuario = session.User;
        }

        public async Task LogoutAsync()
        {
            await conexion.cliente.Auth.SignOut();
        }

        public async Task<User> SignUpAsync(string correo, string password)
        {
            var session = await conexion.cliente.Auth.SignUp(correo, password);
            conexion.usuario = session.User;
            return session.User;
        }

        public async Task<Usuario> GetUsuarioLogged()
        {
            if (conexion.usuario != null)
            {
                var a = conexion.usuario.Id;
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
            if (conexion.usuario != null)
            {
                var a = conexion.usuario.Id;
                var usuario = await repositorioUser.GetUserByUUid(a);
                await repositorioUser.UpdatePuntosUsuario(a, usuario.Puntos, puntos);
            }
        }

        public async Task newUsuario(Usuario user)
        {
            await repositorioUser.Add(user);
        }

        public async Task GuardarPregunta(Reto reto)
        {
            var pregunta = (reto as RetoPre).GetPregunta();
            var a = conexion.usuario.Id;

            var preguntas = await repositorioUser.GetPreguntasAcertadasAsync(a);
            if (preguntas != null)
            {
                int l = preguntas.Length;
                Array.Resize(ref preguntas, preguntas.Length + 1);  // redimensiona el arreglo
                preguntas[l - 1] = (int)pregunta.Id;
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