using preguntaods.Entities;
using System.Threading.Tasks;

namespace preguntaods.Services
{
    public interface IPreguntadosService
    {
        #region RetoPregunta

        Task<Pregunta> SolicitarPregunta(int dificultad);

        #endregion RetoPregunta
    }
}