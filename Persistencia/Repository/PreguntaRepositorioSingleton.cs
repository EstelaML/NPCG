using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Microsoft.EntityFrameworkCore;
using preguntaods.Entities;
using preguntaods.Persistencia;
using preguntaods.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace preguntaods.Persistencia
{
    public class PreguntaRepositorioSingleton : IRepository<RetoPregunta>
    {
        private readonly SingletonConexion conexion;

        public PreguntaRepositorioSingleton() { 
            conexion = SingletonConexion.getInstance();
        }
        public Task Add(RetoPregunta entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RetoPregunta>> GetAll()
        {
            var response = await conexion.cliente
                .From<RetoPregunta>()
                .Get();

            return response.Models.AsEnumerable();
        }

        public Task<RetoPregunta> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(RetoPregunta entity)
        {
            throw new NotImplementedException();
        }
    }
}