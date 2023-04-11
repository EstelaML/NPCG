using Java.Util;
using preguntaods.Entities;
using System.Threading.Tasks;

namespace preguntaods.Persistencia.Repository
{
    public class RepositorioUsuario : Repository<Usuario>
    {
        SingletonConexion conexion;

        public RepositorioUsuario()
        {
            conexion = SingletonConexion.GetInstance();
        }
        public async Task<Usuario> GetUserByUUid(string uuid)
        {
            var response = await conexion.cliente
                .From<Usuario>()
                .Where(x => x.Uuid == uuid)
                .Single();

            return response;
        }
    }
}