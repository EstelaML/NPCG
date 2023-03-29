using preguntaods.Persistencia;
using Supabase.Gotrue;
using System.Threading.Tasks;

namespace preguntaods.Services
{
    public class PreguntadosService : IPreguntadosService
    {
        private readonly SingletonConexion conexion;
        public PreguntadosService()
        {
            conexion = SingletonConexion.GetInstance();
        }

        #region Usuario
        public async Task<Session> LoginAsync(string correo, string password)
        {
            var session = await conexion.cliente.Auth.SignIn(correo, password);

            return session;
        }

        public async Task LogoutAsync()
        {
            await conexion.cliente.Auth.SignOut();
        }
        public async Task<Session> SignUpAsync(string correo, string password)
        {
            var session = await conexion.cliente.Auth.SignUp(correo, password);

            return session;
        }

        #endregion

        #region ODS

        #endregion

        #region Reto
        #endregion

        #region RetoPregunta
        #endregion
    }
}