using Postgrest.Models;

namespace preguntaods.Entities
{
    public partial class ODS : BaseModel, IEntity
    {
        public ODS() { }

        public ODS(string nombre, string descripcion, string imagen)
        {
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.Imagen = imagen;
        }
    }
}