using Java.Util;
using preguntaods.Services;
using System.Collections.Generic;

namespace preguntaods.Entities
{
    public class RetoPre : Reto
    {
        private Pregunta pregunta;
        private PreguntadosService servicio;
        private readonly int type;
        private List<Reto> retos;
        private List<Pregunta> preguntas;
        public RetoPre(List<Reto> listRetos)
        {
            servicio = new PreguntadosService();
            SetPregunta();
            retos = listRetos;
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

        private async void SetPregunta()
        {
            Random random = new Random();

            switch (random.NextInt(3) + 1)
            {
                case 1:
                    {
                        pregunta = await servicio.SolicitarPregunta(Pregunta.difBaja, retos);
                        break;
                    }
                case 2:
                    {
                        pregunta = await servicio.SolicitarPregunta(Pregunta.difMedia, retos);
                        break;
                    }
                case 3:
                    {
                        pregunta = await servicio.SolicitarPregunta(Pregunta.difAlta, retos);
                        break;
                    }
            }
            
        }
    }
}