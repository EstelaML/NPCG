using Android.Media;
using Java.Util;
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

        private RetoPre(PreguntadosService servicio, Pregunta pregunta, int type, List<Reto> retos, int orden)
        {
            this.servicio = servicio;
            this.pregunta = pregunta;
            this.type = type;
            this.retos = retos;
            this.orden = orden;
        }

        public static async Task<RetoPre> RetoPreAsync(List<Reto> retos, int i)
        {
            var servicio = new PreguntadosService();
            var p = await servicio.SetDif(i, retos);
            return new RetoPre(servicio, p, typePregunta, retos, i);
        }


        public override int GetType()
        {
            return type;
        }

        public Pregunta GetPregunta()
        {
            return pregunta;
        }



    }
}