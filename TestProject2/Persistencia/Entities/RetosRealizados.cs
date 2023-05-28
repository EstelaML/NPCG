using Postgrest.Attributes;
using Postgrest.Models;

// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    [Table("RetosRealizados")]
    public partial class RetosRealizados : BaseModel, IEntity
    {
        // ReSharper disable once ExplicitCallerInfoArgument
        [PrimaryKey("id")]
        public int? Id { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Usuario")]
        public int Usuario { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("PreguntasRealizadas")]
        public int[] PreguntasRealizadas { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("AhorcadosRealizados")]
        public int[] AhorcadosRealizados { get; set; }
    }
}