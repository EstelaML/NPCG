using preguntaods.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preguntaods.Persistencia.Repository
{
    public class RepositorioPregunta : Repository<Pregunta>
    {
        SingletonConexion conexion;
        public RepositorioPregunta()
        {
            conexion = SingletonConexion.GetInstance();
        }
        public async Task<IEnumerable<Pregunta>> GetByDificultad(int dificultad)
        {
            var response = await conexion.cliente
                .From<Pregunta>()
                .Where(x => x.Dificultad == dificultad).Get();

            return response.Models.AsEnumerable();
        }
    }       
}