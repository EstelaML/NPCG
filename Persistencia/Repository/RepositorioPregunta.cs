using preguntaods.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preguntaods.Persistencia.Repository
{
    public class RepositorioPregunta : Repository<Pregunta>
    {
        public async Task<IEnumerable<Pregunta>> GetByDificultad(string dificultad)
        {
            var a = SingletonConexion.GetInstance();
            var response = await a.cliente
                .From<Pregunta>()
                .Where(x => x.Dificultad == dificultad).Get();

            return response.Models.AsEnumerable();
        }
    }       
}