using Postgrest.Attributes;
using Postgrest.Models;

// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    [Table("RetosRealizados")]
    public partial class RetosRealizados : BaseModel, IEntity
    {
        [PrimaryKey]
        public int? Id { get; set; }

        [Column]
        public int Usuario { get; set; }

        [Column]
        public int[] PreguntasRealizadas { get; set; }

        [Column]
        public int[] AhorcadosRealizados { get; set; }
    }
}