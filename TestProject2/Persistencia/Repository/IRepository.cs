using Postgrest.Models;
using preguntaods.Entities;

namespace preguntaods.Persistencia.Repository
{
    public interface IRepository<T> where T : BaseModel, IEntity, new()
    {
        Task<T> GetById(int id);

        Task<IEnumerable<T>> GetAll();

        Task Add(T entity);

        Task Update(T entity);

        Task Delete(int id);
    }
}