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

        [Column("Ahorcado")]
        public int? Ahorcado { get; set; }

        [Column("Pregunta")]
        public int? Pregunta { get; set; }

        [Column("Usuario")]
        public int Usuario { get; set; }
    }
}