using preguntaods.Entities;
using preguntaods.Persistencia.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preguntaods.Services
{
    public class PreguntadosService : IPreguntadosService
    {
        private readonly RepositorioPregunta repositorioPre;
        private readonly Repository<Usuario> repositoryUser;
        public PreguntadosService()
        {
            repositorioPre = new RepositorioPregunta();
        }

        #region Usuario

        public async Task<Usuario> GetUser(int id)
        {
            var respuesta = await repositoryUser.GetById(id);

            return respuesta;
        }

        #endregion

        #region Reto
        #endregion

        #region RetoPregunta
        public async Task<Pregunta> SolicitarPregunta(int dificultad)
        {
            var respuesta = await repositorioPre.GetByDificultad(dificultad);
                
            return respuesta.FirstOrDefault();
        }
        #endregion
    }
}