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
            pregunta = SetDif(orden, listRetos).Result;
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

        public async Task<Pregunta> SetDif(int orden, List<Reto> retos)
        {
            if (orden < 4 || orden == 10)
            {
                return await SetPregunta(1, retos);
            }
            else if (orden < 7 || orden == 11)
            {
                return await SetPregunta(2, retos);
            }
            else { return await SetPregunta(3, retos); }
        }

        private async Task<Pregunta> SetPregunta(int dif, List<Reto> retos)
        {
            switch (dif)
            {
                case 1:
                    {
                        return await servicio.SolicitarPregunta(Pregunta.difBaja, retos);

                        break;
                    }
                case 2:
                    {
                        return await servicio.SolicitarPregunta(Pregunta.difMedia, retos);
                        break;
                    }
                case 3:
                    {
                        return await servicio.SolicitarPregunta(Pregunta.difAlta, retos);
                        break;
                    }
            }
            return null;
        }
    }
}