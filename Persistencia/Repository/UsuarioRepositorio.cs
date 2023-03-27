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
    internal class UsuarioRepositorio : Repository<Usuario>
    {
        public async Task<Usuario> GetUserByName(String id)
        {
            var a = SingletonConexion.getInstance();
            var response = await a.cliente
                .From<Usuario>()
                .Where(x => x.nombre == id)
                .Single();

            return response;
        }
    }
}