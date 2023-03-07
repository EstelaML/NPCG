using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using pruebasEF.Services;
using Supabase.Gotrue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pruebasEF.Services
{
    public class PreguntadosService : IPreguntadosService
    {
        void DBInitialization() { 
        
        }

        void IPreguntadosService.DBInitialization()
        {
            throw new NotImplementedException();
        }

        void DBRestartData() { }

        void IPreguntadosService.DBRestartData()
        {
            throw new NotImplementedException();
        }

        #region Usuario
        string Login(string login, string password) { return "";  }

        string IPreguntadosService.Login(string login, string password)
        {
            throw new NotImplementedException();
        }

        void Logout() { }

        void IPreguntadosService.Logout()
        {
            throw new NotImplementedException();
        }

        User SignUp(String username, String correo, String password, String img) { return new User(); }

        User IPreguntadosService.SignUp(string username, string correo, string password, string img)
        {
            throw new NotImplementedException();
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