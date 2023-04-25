using preguntaods.Entities;
using preguntaods.Persistencia.Repository.impl;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preguntaods.Persistencia.Repository
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
            
            var ahorcados = await (conexion.cliente.From<Ahorcado>().Where(x => x.Dificultad == dificultad).Get());
            return ahorcados.Models;
        }
    }
}