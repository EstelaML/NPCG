using Postgrest.Attributes;
using Postgrest.Models;

// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    [Table("ods")]
    public partial class Ods : BaseModel, IEntity
    {
        // ReSharper disable once ExplicitCallerInfoArgument
        [PrimaryKey("id")]
        public int? Id { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Nombre")]
        public string Nombre { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Descripcion")]
        public string Descripcion { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Imagen")]
        public string Imagen { get; set; }
    }
}