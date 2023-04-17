using preguntaods.Entities;
using System.Threading.Tasks;

namespace preguntaods.Persistencia.Repository
{
    public class RepositorioUsuario : Repository<Usuario>
    {
        private SingletonConexion conexion;

        public RepositorioUsuario()
        {
            conexion = SingletonConexion.GetInstance();
        }

        public async Task<Usuario> GetUserByUUid(string u)
        {
            var response = await conexion.cliente
                .From<Usuario>()
                .Where(x => x.Uuid == u)
                .Single();
            return response;
        }

        public async Task UpdatePuntosUsuario(string uuid, int puntosA, int puntosS)
        {
            int p = puntosA + puntosS;
            var response2 = await conexion.cliente
                        .From<Usuario>()
                        .Where(x => x.Uuid == uuid)
                        .Set(x => x.Puntos, p)
                        .Update();
        }

        public async Task UpdatePreguntaAcertada(string a, int[] preguntas)
        {
            var update = await conexion.cliente
                     .From<Usuario>()
                     .Where(x => x.Uuid == a)
                     .Set(x => x.PreguntasRealizadas, preguntas)
                     .Update();
        }

        public async Task<int[]> GetPreguntasAcertadasAsync()
        {
            var res = await conexion.cliente.From<Usuario>().Where(x => x.Uuid == conexion.usuario.Id).Single();
            return res.PreguntasRealizadas;
        }
    }
}