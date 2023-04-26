using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Postgrest.Models;
using preguntaods.Entities;

namespace preguntaods.Persistencia.Repository.impl
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
            var response = await conexion.Cliente
                .From<T>()
                .Where(x => x.Id == id)
                .Single();

            return response;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var response = await conexion.Cliente
                .From<T>()
                .Get();

            return response.Models.AsEnumerable();
        }

        public async Task Add(T entity)
        {
            await conexion.Cliente
                .From<T>()
                .Insert(entity);
        }

        public async Task Update(T entity)
        {
            await conexion.Cliente
                .From<T>()
                .Update(entity);
        }

        public async Task Delete(int id)
        {
            await conexion.Cliente
                .From<T>()
                .Where(x => x.Id == id)
                .Delete();
        }
    }
}