using preguntaods.Entities;
using System.Threading.Tasks;

namespace preguntaods.Persistencia.Repository
{
    internal class RepositorioConfiguracion : Repository<Usuario>
    {
        public async Task<Usuario> GetUserByName(int id)
        {
            var a = SingletonConexion.GetInstance();
            var response = await a.cliente
                .From<Usuario>()
                .Where(x => x.Id == id)
                .Single();

            return response;
        }
    }
}