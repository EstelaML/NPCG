using Supabase.Gotrue;
using System.Threading.Tasks;

namespace preguntaods.Services
{
    public interface IPreguntadosService
    {
        #region Usuario
        Task<Session> LoginAsync(string correo, string password);
        Task LogoutAsync();
        Task<Session> SignUpAsync(string correo, string password);

        #endregion

        #region ODS

        #endregion

        #region Reto
        #endregion

        #region RetoPregunta
        #endregion

    }
}