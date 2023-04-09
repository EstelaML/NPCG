using Java.Util;
using preguntaods.Entities;
using System.Threading.Tasks;

namespace preguntaods.Persistencia.Repository
{
    internal class RepositorioUsuario : Repository<Usuario>
    {
        public async Task<Usuario> GetUserById(UUID id)
        {
            var a = SingletonConexion.GetInstance();
            var response = await a.cliente
                .From<Usuario>()
                .Where(x => x.Id.Equals(id))
                .Single();

            return response;
        }
    }
}