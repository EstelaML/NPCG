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
        public void DBInitialization()
        {
            throw new NotImplementedException();
        }

        public void DBRestartData()
        {
            throw new NotImplementedException();
        }

        #region Usuario
        public bool Login(string login, string password)
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
        public Configuracion SignUp(string username, string correo, string password, string img)
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