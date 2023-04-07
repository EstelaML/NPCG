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
            pregunta = await servicio.SolicitarPregunta(Pregunta.difBaja);
        }
    }
}