using Java.Util;
using Postgrest.Models;
using System.Collections.Generic;

namespace preguntaods.Entities
{
    public partial class Usuario : BaseModel
    {
        public Usuario() { }
        public Usuario(UUID uid, string nombre, bool sonidos, int puntos, int musica, IEnumerable<int> PreguntasRealizadas)
        {
            this.Uuid = uid;
            this.Nombre = nombre;
            this.Sonidos = sonidos;
            this.Puntos = puntos;
            this.Musica = musica;
            this.PreguntasRealizadas = PreguntasRealizadas;
        }
    }
}