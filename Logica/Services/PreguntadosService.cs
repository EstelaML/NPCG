using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using preguntaods.Entities;
using preguntaods.Services;
using Supabase.Gotrue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace preguntaods.Services
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
        bool IPreguntadosService.Login(string login, string password)
        {
            throw new NotImplementedException();
        }

        void Logout() { }

        void IPreguntadosService.Logout()
        {
            throw new NotImplementedException();
        }

        Usuario SignUp(String username, String correo, String password, String img) { return new Usuario(); }

        Usuario IPreguntadosService.SignUp(string username, string correo, string password, string img)
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