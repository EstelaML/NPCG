using Java.Util;
using preguntaods.Services;

namespace preguntaods.Entities
{
    public class RetoPre : Reto
    {
        private Pregunta pregunta;
        private PreguntadosService servicio;
        private readonly int type;
        public RetoPre()
        {
            servicio = new PreguntadosService();
            SetPregunta();
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
                        pregunta = await servicio.SolicitarPregunta(Pregunta.difBaja);
                        break;
                    }
                case 2:
                    {
                        pregunta = await servicio.SolicitarPregunta(Pregunta.difMedia);
                        break;
                    }
                case 3:
                    {
                        pregunta = await servicio.SolicitarPregunta(Pregunta.difAlta);
                        break;
                    }
            }
            
        }
    }
}