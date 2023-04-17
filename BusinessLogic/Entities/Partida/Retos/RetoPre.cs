using preguntaods.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace preguntaods.Entities
{
    public class RetoPre : Reto
    {
        private Pregunta pregunta;
        private PreguntadosService servicio;
        private readonly int type;
        private List<Reto> retos;
        private List<Pregunta> preguntas;
        private int orden;

        public RetoPre(List<Reto> listRetos, int orden)
        {
            servicio = new PreguntadosService();
            this.orden = orden;
            retos = listRetos;
            SetDif(orden, listRetos);
            type = typePregunta;
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
                pregunta = await SetPregunta(Pregunta.difBaja, retos);
            }
            else if (orden < 7 || orden == 11)
            {
                pregunta = await SetPregunta(Pregunta.difMedia, retos);
            }
            else { pregunta = await SetPregunta(Pregunta.difAlta, retos); }
        }

        private async Task<Pregunta> SetPregunta(int dif, List<Reto> retos)
        {
            return await servicio.SolicitarPregunta(dif, retos);
        }
    }
}