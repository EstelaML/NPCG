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

namespace preguntaods.Services
{
    public interface IPreguntadosService
    {
        void DBInitialization();
        void DBRestartData();

        #region Usuario
        bool Login(string login, string password);
        void Logout();
        Usuario SignUp(String username, String correo, String password, String img);

        #endregion

        #region ODS

        #endregion

        #region Reto
        #endregion

        #region RetoPregunta
        #endregion

    }
}