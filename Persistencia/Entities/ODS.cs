using Postgrest.Attributes;
using Postgrest.Models;

// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    [Table("ods")]
    public partial class Ods : BaseModel, IEntity
    {
        [PrimaryKey("id")]
        public int? Id { get; set; }

        [Column("Nombre")]
        public string Nombre { get; set; }

        [Column("Descripcion")]
        public string Descripcion { get; set; }

        [Column("Imagen")]
        public string Imagen { get; set; }
    }
}