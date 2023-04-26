using preguntaods.Entities;
using System.Threading.Tasks;

namespace preguntaods.BusinessLogic.Services
{
    public interface IPreguntadosService
    {
        #region RetoPregunta

        Task<Pregunta> SolicitarPregunta(int dificultad);

        #endregion RetoPregunta
    }
}