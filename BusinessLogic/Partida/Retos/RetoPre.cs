using System.Collections.Generic;
using System.Threading.Tasks;
using preguntaods.BusinessLogic.Services;
using preguntaods.Entities;

namespace preguntaods.BusinessLogic.Partida.Retos
{
    public class RetoPre : Reto
    {
        private Pregunta pregunta;
        private static PreguntadosService servicio;
        private readonly int type;
        private List<Reto> retos;

        public RetoPre(List<Reto> listaRetos, int orden)
        {
            servicio = new PreguntadosService();
            retos = listaRetos;
            type = TypePregunta;
            servicio.InitPreguntaList().ContinueWith(t => { _ = SetDif(orden, listaRetos); });
        }

        public override int GetType()
        {
            return type;
        }

        public Pregunta GetPregunta()
        {
            return pregunta;
        }

        public async Task SetDif(int orden, List<Reto> retos)
        {
            if (orden < 4 || orden == 10)
            {
                pregunta = await servicio.SolicitarPregunta(Pregunta.DifBaja);
            }
            else if (4 <= orden && orden < 7 || orden == 11)
            {
                pregunta = await servicio.SolicitarPregunta(Pregunta.DifMedia);
            }
            else { 
                pregunta = await servicio.SolicitarPregunta(Pregunta.DifAlta); 
            }
        }
    }
}