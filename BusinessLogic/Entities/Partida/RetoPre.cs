using Android.OS.Strictmode;
using preguntaods.Services;
using System.Collections.Generic;

namespace preguntaods.Entities
{
    public class RetoPre : Reto
    {
        List<Pregunta> preguntas;
        PreguntadosService servicio;
        public RetoPre()
        {
            servicio = new PreguntadosService();
            preguntas = servicio.SolicitarPreguntas();
        }

        public List<Pregunta> GetPreguntas()
        {
            return preguntas;
        }
    }
}