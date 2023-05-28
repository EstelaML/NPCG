using Postgrest.Attributes;
using Postgrest.Models;

// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    [Table("Ahorcado")]
    public partial class Ahorcado : BaseModel, IEntity
    {
        // ReSharper disable once RedundantArgumentDefaultValue
        [PrimaryKey("Id")]
        public int? Id { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Enunciado")]
        public string Enunciado { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Palabra")]
        public string Palabra { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Dificultad")]
        public int Dificultad { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("OdsRelacionada")]
        public int? OdsRelacionada { get; set; }
    }
}