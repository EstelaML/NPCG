using Postgrest.Attributes;
using Postgrest.Models;

// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    [Table("Ahorcado")]
    public partial class Ahorcado : BaseModel, IEntity
    {
        [PrimaryKey("Id")]
        public int? Id { get; set; }

        [Column("Enunciado")]
        public string Enunciado { get; set; }

        [Column("Palabra")]
        public string Palabra { get; set; }

        [Column("Dificultad")]
        public int Dificultad { get; set; }
    }
}