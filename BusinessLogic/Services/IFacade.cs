using preguntaods.Entities;
using Supabase.Gotrue;
using System.Threading.Tasks;

namespace preguntaods.Services
{
    public interface IFacade
    {
        #region Usuario
        Task LoginAsync(string correo, string password);
        Task LogoutAsync();
        Task<User> SignUpAsync(string correo, string password);
        Task<Usuario> GetUsuarioLogged();

        #endregion

        #region Sonido

        void EjecutarSonido(Android.Content.Context t, IEstrategiaSonido estrategia);
        void PararSonido(IEstrategiaSonido estrategia);

        #endregion
    }
}