using System.Collections.Generic;
using System.Threading.Tasks;
using preguntaods.Entities;

namespace preguntaods.Persistencia.Repository.impl
{
    public class RepositorioAhorcado : Repository<Ahorcado>
    {
        private SingletonConexion conexion;

        public RepositorioAhorcado()
        {
            conexion = SingletonConexion.GetInstance();
        }

        public async Task<List<Ahorcado>> GetAhorcadoDificultad(int dificultad)
        {
            
            var ahorcados = await (conexion.Cliente.From<Ahorcado>().Where(x => x.Dificultad == dificultad).Get());
            return ahorcados.Models;
        }
    }
}