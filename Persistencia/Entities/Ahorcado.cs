using Postgrest.Attributes;
using Postgrest.Models;

// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    [Table("Ahorcado")]
    public partial class Ahorcado : BaseModel, IEntity
    {
        [PrimaryKey]
        public int? Id { get; set; }

        [Column]
        public string Enunciado { get; set; }

        [Column]
        public string Palabra { get; set; }

        [Column]
        public int Dificultad { get; set; }

        [Column]
        public int? OdsRelacionada { get; set; }
    }
}