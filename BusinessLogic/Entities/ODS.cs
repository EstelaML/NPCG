using Postgrest.Models;

// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    public partial class Ods : BaseModel, IEntity
    {
        public Ods()
        { }

        public Ods(string nombre, string descripcion, string imagen)
        {
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.Imagen = imagen;
        }
    }
}