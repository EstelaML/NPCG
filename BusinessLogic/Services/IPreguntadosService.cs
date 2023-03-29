using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using preguntaods.Entities;
using Supabase.Gotrue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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