using Postgrest.Attributes;
using Postgrest.Models;

// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    [Table("Usuario")]
    public partial class Usuario : BaseModel, IEntity
    {
        // ReSharper disable once RedundantArgumentDefaultValue
        [PrimaryKey("Id")]
        public int? Id { get; set; }

        // ReSharper disable once ExplicitCallerInfoArgument
        [Column("UUID")]
        public string Uuid { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Nombre")]
        public string Nombre { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Puntos")]
        public int Puntos { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Sonidos")]
        public bool Sonidos { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Musica")]
        public int Musica { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("PreguntasRealizadas")]
        public int[] PreguntasRealizadas { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Foto")]
        public string Foto { get; set; }
    }
}