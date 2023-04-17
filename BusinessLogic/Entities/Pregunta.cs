using Postgrest.Models;

namespace preguntaods.Entities
{
    public partial class Pregunta : BaseModel, IEntity
    {
        public const int difAlta = 1003;
        public const int difMedia = 1002;
        public const int difBaja = 1001;

        public Pregunta()
        { }

        public Pregunta(string enunciado, string respuesta1, string respuesta2, string respuesta3, string respuesta4, string solucion, int dificultad, string ods)
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