using preguntaods.BusinessLogic.Services;
using preguntaods.Entities;
using System.Threading.Tasks;

namespace preguntaods.BusinessLogic.Partida.Retos
{
    public class RetoAhorcado : IReto
    {
        private Ahorcado ahorcado;
        private static PreguntadosService _servicio;
        private readonly int numeroReto;

        public int Type { get; set; }

        public RetoAhorcado(int orden)
        {
            Type = IReto.TypeAhorcado;

            _servicio = new PreguntadosService();
            numeroReto = orden;
        }

        public async Task SetValues()
        {
            await _servicio.InitAhorcadoList().ContinueWith(t => { _ = SetDif(numeroReto); });
        }

        public Ahorcado GetAhorcado()
        { return ahorcado; }

        public async Task SetDif(int orden)
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