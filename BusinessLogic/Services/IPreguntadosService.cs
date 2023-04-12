using preguntaods.Entities;
using Supabase.Gotrue;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace preguntaods.Services
{
    public interface IPreguntadosService
    {

        #region ODS

        #endregion

        #region Reto
        #endregion

        #region RetoPregunta

        Task<Pregunta> SolicitarPregunta(int dificultad, List<Reto> retos);
        #endregion

    }
}