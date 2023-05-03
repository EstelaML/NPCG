using preguntaods.BusinessLogic.Services;
using preguntaods.Entities;
using System.Threading.Tasks;

namespace preguntaods.BusinessLogic.Partida.Retos
{
    public class RetoPre : Reto
    {
        private Pregunta pregunta;
        private static PreguntadosService _servicio;
        private readonly int type;
        private readonly int numeroReto;

        public RetoPre(int orden)
        {
            SetType(TypePregunta);

            _servicio = new PreguntadosService();
            numeroReto = orden;
        }

        public override async Task SetValues()
        {
            await _servicio.InitPreguntaList().ContinueWith(t => { _ = SetDif(numeroReto); });
        }

        public Pregunta GetPregunta()
        {
            return pregunta;
        }

        private async Task SetDif(int orden)
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