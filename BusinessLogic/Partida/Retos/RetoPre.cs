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

        public RetoPre(int orden)
        {
            _servicio = new PreguntadosService();
            type = TypePregunta;
            _servicio.InitPreguntaList().ContinueWith(t => { _ = SetDif(orden); });
        }

        public override int GetType()
        {
            return type;
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