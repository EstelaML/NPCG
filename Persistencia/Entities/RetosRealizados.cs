using Postgrest.Attributes;
using Postgrest.Models;

// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    [Table("RetosRealizados")]
    public partial class RetosRealizados : BaseModel, IEntity
    {
        [PrimaryKey("id")]
        public int? Id { get; set; }

        [Column("Usuario")]
        public int Usuario { get; set; }

        [Column("PreguntasRealizadas")]

        public int[] PreguntasRealizadas { get; set; }

        [Column("AhorcadosRealizados")]
        public int[] AhorcadosRealizados { get; set; }
    }
}