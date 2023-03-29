using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Postgrest.Models;
using preguntaods.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace preguntaods.Persistencia.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseModel, IEntity, new()
    {
        private readonly SingletonConexion conexion;

        public Repository()
        {
            conexion = SingletonConexion.GetInstance();
        }

        public async Task<T> GetById(int id)
        {
            var response = await conexion.cliente
                .From<T>()
                .Where(x => x.Id == id)
                .Single();

            return response;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var response = await conexion.cliente
                .From<T>()
                .Get();

            return response.Models.AsEnumerable();
        }

        public async Task Add(T entity)
        {
            await conexion.cliente
                .From<T>()
                .Insert(entity);
        }

        public async Task Update(T entity)
        {
            await conexion.cliente
                .From<T>()
                .Update(entity);
        }

        public async Task Delete(int id)
        {
            await conexion.cliente
                .From<T>()
                .Where (x => x.Id == id)
                .Delete();
        }
    }
}