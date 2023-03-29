using preguntaods.Persistencia;
using System.Threading.Tasks;
using preguntaods.Entities;

namespace preguntaods.Services
{
    public class Facade
    {
        private readonly SingletonConexion conexion;
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
        public async Task SignUpAsync(string correo, string password)
        {
            var session = await conexion.cliente.Auth.SignUp(correo, password);

            conexion.usuario = session.User;
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