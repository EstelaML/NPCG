using Postgrest.Attributes;
using Postgrest.Models;

// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    [Table("Pregunta")]
    public partial class Pregunta : BaseModel, IEntity
    {
        [PrimaryKey]
        public int? Id { get; set; }

        [Column]
        public string Enunciado { get; set; }

        [Column]
        public string Respuesta1 { get; set; }

        [Column]
        public string Respuesta2 { get; set; }

        [Column]
        public string Respuesta3 { get; set; }

        [Column]
        public string Respuesta4 { get; set; }

        [Column]
        public string Correcta { get; set; }

        [Column]
        public int Dificultad { get; set; }

        [Column]
        public string OdsRelacionada { get; set; }
    }
}