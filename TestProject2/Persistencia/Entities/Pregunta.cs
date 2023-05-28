using Postgrest.Attributes;
using Postgrest.Models;

// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    [Table("Pregunta")]
    public partial class Pregunta : BaseModel, IEntity
    {
        // ReSharper disable once RedundantArgumentDefaultValue
        [PrimaryKey("Id")]
        public int? Id { get; set; }

        // ReSharper disable once ExplicitCallerInfoArgument
        [Column("Pregunta")]
        public string Enunciado { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Respuesta1")]
        public string Respuesta1 { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Respuesta2")]
        public string Respuesta2 { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Respuesta3")]
        public string Respuesta3 { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Respuesta4")]
        public string Respuesta4 { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Correcta")]
        public string Correcta { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Dificultad")]
        public int Dificultad { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("OdsRelacionada")]
        public int? OdsRelacionada { get; set; }
    }
}