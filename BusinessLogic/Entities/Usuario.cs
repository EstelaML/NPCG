using Postgrest.Models;
using System.Collections.Generic;

namespace preguntaods.Entities
{
    public partial class Usuario : BaseModel, IEntity
    {
        public Usuario() { }
        public Usuario(int id, string nombre, bool sonidos, int puntos, int musica, IEnumerable<int> PreguntasRealizadas)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Sonidos = sonidos;
            this.Puntos = puntos;
            this.Musica = musica;
            this.PreguntasRealizadas = PreguntasRealizadas;
        }
    }
}