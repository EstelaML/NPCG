using Postgrest.Models;

namespace preguntaods.Entities
{
    public partial class Configuracion : BaseModel, IEntity
    {
        public Configuracion() { }
        public Configuracion(int id, string nombre, bool sonidos, int puntos, int musica)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Sonidos = sonidos;
            this.Puntos = puntos;
            this.Musica = musica;
        }
    }
}