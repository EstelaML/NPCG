using Postgrest.Attributes;
using Postgrest.Models;

// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    [Table("ods")]
    public partial class Ods : BaseModel, IEntity
    {
        [PrimaryKey]
        public int? Id { get; set; }

        [Column]
        public string Nombre { get; set; }

        [Column]
        public string Descripcion { get; set; }

        [Column]
        public string Imagen { get; set; }
    }
}