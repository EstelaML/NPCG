using Java.Util;
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
            conexion.usuario =  session.User;
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

        public async Task UpdatePuntos(int puntos) {
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

        public async Task GuardarPregunta(Reto reto) {
            var pregunta = (reto as RetoPre).GetPregunta();
            var a = conexion.usuario.Id;

            var preguntasRealizadas = await conexion.cliente.From<Usuario>().Where(x => x.Uuid == a).Single();
           if (preguntasRealizadas.PreguntasRealizadas != null)
            {
                int[] preguntas = preguntasRealizadas.PreguntasRealizadas;
                int l = preguntas.Length;
                Array.Resize(ref preguntas, preguntas.Length + 1);  // redimensiona el arreglo
                preguntas[l - 1] = (int)pregunta.Id;
                var update = await conexion.cliente
                     .From<Usuario>()
                     .Where(x => x.Uuid == a)
                     .Set(x => x.PreguntasRealizadas, preguntas)
                     .Update();
            }
            else {
                int[] preguntas = { (int) pregunta.Id };
                var update = await conexion.cliente
                     .From<Usuario>()
                     .Where(x => x.Uuid == a)
                     .Set(x => x.PreguntasRealizadas, preguntas)
                     .Update();
            }

           
        }

        #endregion

        #region Sonido

        public void EjecutarSonido(Android.Content.Context t, IEstrategiaSonido estrategia)
        {
            estrategia.Play(t);
        }

        public void PararSonido(IEstrategiaSonido estrategia)
        {
            estrategia.Stop();
        }

        #endregion
    }
}