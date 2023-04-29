// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    public partial class Ods
    {
        public Ods()
        { }

        public Ods(string nombre, string descripcion, string imagen)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Imagen = imagen;
        }
    }
}