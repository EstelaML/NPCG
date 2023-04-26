using System.Threading.Tasks;
using preguntaods.Entities;

namespace preguntaods.BusinessLogic.Services
{
    public interface IPreguntadosService
    {
        #region RetoPregunta

        Task<Pregunta> SolicitarPregunta(int dificultad);

        #endregion RetoPregunta
    }
}