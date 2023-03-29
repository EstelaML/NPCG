using Postgrest.Models;

namespace preguntaods.Entities
{
    public partial class Pregunta : BaseModel, IEntity
    { 
        public Pregunta() { }

        public Pregunta(string enunciado, string respuesta1, string respuesta2, string respuesta3, string respuesta4, string solucion, string dificultad, string ods)
        {
            Enunciado = enunciado;
            Respuesta1 = respuesta1;
            Respuesta2 = respuesta2;
            Respuesta3 = respuesta3;
            Respuesta4 = respuesta4;
            Correcta = solucion;
            Dificultad = dificultad;
            OdsRelacionada = ods;
        }
    }
}