using preguntaods.Persistencia;
using Supabase.Gotrue;
using System.Threading.Tasks;

namespace preguntaods.Services
{
    public class PreguntadosService : IPreguntadosService
    {
        private readonly SingletonConexion conexion;
        public PreguntadosService()
        {
            conexion = SingletonConexion.GetInstance();
        }

        #region ODS

        #endregion

        #region Reto
        #endregion

        #region RetoPregunta

        #endregion
    }
}