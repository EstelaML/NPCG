using preguntaods.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace preguntaods.Services
{
    public interface IPreguntadosService
    {
        #region RetoPregunta

        Task<Pregunta> SolicitarPregunta(int dificultad, List<Reto> retos);

        #endregion RetoPregunta
    }
}