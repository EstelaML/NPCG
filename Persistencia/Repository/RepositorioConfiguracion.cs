using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using preguntaods.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace preguntaods.Persistencia.Repository
{
    internal class RepositorioConfiguracion : Repository<Configuracion>
    {
        public async Task<Configuracion> GetUserByName(int id)
        {
            var a = SingletonConexion.GetInstance();
            var response = await a.cliente
                .From<Configuracion>()
                .Where(x => x.Id == id)
                .Single();

            return response;
        }
    }
}