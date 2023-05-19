using preguntaods.BusinessLogic.Services;
using preguntaods.Entities;
using System.Threading.Tasks;

namespace preguntaods.BusinessLogic.Retos
{
    public class RetoPre : IReto
    {
        private Pregunta pregunta;
        private static PreguntadosService _servicio;
        private readonly int numeroReto;

        public int Type { get; set; }

        public RetoPre(int orden)
        {
            Type = IReto.TypePregunta;

            _servicio = new PreguntadosService();
            numeroReto = orden;
        }

        public async Task SetValues()
        {
            await _servicio.InitPreguntaList().ContinueWith(t => { _ = SetDif(numeroReto); });
        }

        public Pregunta GetPregunta()
        {
            return pregunta;
        }

        public async Task SetDif(int orden)
        {
            if (orden < 4 || orden == 10)
            {
                pregunta = await _servicio.SolicitarPregunta(Pregunta.DifBaja);
            }
            else if (orden < 7 || orden == 11)
            {
                pregunta = await _servicio.SolicitarPregunta(Pregunta.DifMedia);
            }
            else
            {
                pregunta = await _servicio.SolicitarPregunta(Pregunta.DifAlta);
            }
        }
    }
}