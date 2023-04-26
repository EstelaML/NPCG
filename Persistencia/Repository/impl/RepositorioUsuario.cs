using preguntaods.Entities;
using System.Threading.Tasks;

namespace preguntaods.Persistencia.Repository.impl
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
            var response = await conexion.Cliente
                .From<Usuario>()
                .Where(x => x.Uuid == u)
                .Single();
            return response;
        }

        public async Task UpdatePuntosUsuario(string uuid, int puntosA, int puntosS)
        {
            int p = puntosA + puntosS;
            await conexion.Cliente
                        .From<Usuario>()
                        .Where(x => x.Uuid == uuid)
                        .Set(x => x.Puntos, p)
                        .Update();
        }

        public async Task UpdatePreguntaAcertada(string a, int[] preguntas)
        {
            await conexion.Cliente
                .From<Usuario>()
                .Where(x => x.Uuid == a)
                .Set(x => x.PreguntasRealizadas, preguntas)
                .Update();
        }

        public async Task<int[]> GetPreguntasAcertadasAsync(string a)
        {
            var res = await conexion.Cliente
                .From<Usuario>()
                .Where(x => x.Uuid == a).Single();
            return res?.PreguntasRealizadas;
        }
    }
}