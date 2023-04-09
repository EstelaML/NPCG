using preguntaods.Entities;
using System.Threading.Tasks;

namespace preguntaods.Persistencia.Repository
{
    internal class RepositorioConfiguracion : Repository<Usuario>
    {
        public async Task<Usuario> GetUserByName(string nombre)
        {
            var a = SingletonConexion.GetInstance();
            var response = await a.cliente
                .From<Usuario>()
                .Where(x => x.Nombre.Equals(nombre))
                .Single();

            return response;
        }
    }
}