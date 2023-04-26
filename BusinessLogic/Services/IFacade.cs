using System.Threading.Tasks;
using preguntaods.Entities;
using Supabase.Gotrue;

namespace preguntaods.BusinessLogic.Services
{
    public interface IFacade
    {
        #region Usuario

        Task LoginAsync(string correo, string password);

        Task LogoutAsync();

        Task<User> SignUpAsync(string correo, string password);

        Task<Usuario> GetUsuarioLogged();

        #endregion Usuario
    }
}