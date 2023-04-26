using System.Threading.Tasks;
using preguntaods.BusinessLogic.Services;
using preguntaods.Entities;

namespace preguntaods.BusinessLogic.Partida.Retos
{
    public class RetoAhorcado : Reto
    {
        private readonly int type;
        private Ahorcado ahorcado;
        private static PreguntadosService _servicio;

        public RetoAhorcado(int dificultad)
        {
            type = TypeAhorcado;
            _servicio = new PreguntadosService();
            _servicio.InitAhorcadoList().ContinueWith(t => { _ = SetDiff(dificultad); });
        }

        public override int GetType()
        {
            return type;
        }

        public Ahorcado GetAhorcado() { return ahorcado;  }

        public async Task SetDiff(int orden)
        {
            if (orden < 4 || orden == 10)
            {
                ahorcado = await _servicio.SolicitarAhorcado(Ahorcado.DifBaja);
            }
            else if (orden < 7 || orden == 11)
            {
                ahorcado = await _servicio.SolicitarAhorcado(Ahorcado.DifMedia);
            }
            else
            {
                ahorcado = await _servicio.SolicitarAhorcado(Ahorcado.DifAlta);
            }
        }
    }
}