using System.Collections.Generic;
using System.Threading.Tasks;
using preguntaods.BusinessLogic.Partida.Retos;
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

        public async Task AñadirAhorcadoRealizado(int id, Reto reto)
        {
            var model = new RetosRealizados
            {
                Usuario = id,
                Ahorcado = (reto as RetoAhorcado).GetAhorcado().Id,
                Pregunta = null,
            };
            await conexion.Cliente.From<RetosRealizados>().Insert(model);
        }
    }
}