using preguntaods.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace preguntaods.Entities
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
            type = typePregunta;
            servicio.InitList().ContinueWith(t => { SetDif(orden, listaRetos); });
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
                pregunta = await servicio.SolicitarPregunta(Pregunta.difBaja);
            }
            else if (orden < 7 || orden == 11)
            {
                pregunta = await servicio.SolicitarPregunta(Pregunta.difMedia);
            }
            else { pregunta = await servicio.SolicitarPregunta(Pregunta.difAlta); }
        }
    }
}