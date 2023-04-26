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

        [Column("Pregunta2")]

        public int[] Pregunta2 { get; set; }

        [Column("Ahorcado2")]
        public int[] Ahorcado2 { get; set; }
    }
}