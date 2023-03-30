using preguntaods.Persistencia;

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