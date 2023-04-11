using Java.Util;
using preguntaods.Entities;
using preguntaods.Persistencia;
using preguntaods.Persistencia.Repository;
using Supabase.Gotrue;
using System.Threading.Tasks;

namespace preguntaods.Services
{
    public class Facade
    {
        private readonly SingletonConexion conexion;
        private readonly RepositorioUsuario repositorioUser;
        public Facade()
        {
            conexion = SingletonConexion.GetInstance();
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

        public async Task<Usuario> GetUsarioLogged()
        {
            var a = conexion.usuario.Id;
            var respuesta = await repositorioUser.GetUserById(a);
            return respuesta;
        }

       

        public async Task newUsuario(Usuario user)
        {
            await conexion.cliente
                .From<Usuario>()
                .Insert(user);
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