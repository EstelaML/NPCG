using Postgrest.Attributes;
using Postgrest.Models;

namespace preguntaods.Entities
{
    [Table("Pregunta")]
    public partial class Pregunta : BaseModel, IEntity
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("Pregunta")]
        public string Enunciado { get; set; }

        [Column("Respuesta1")]
        public string Respuesta1 { get; set; }

        [Column("Respuesta2")]
        public string Respuesta2 { get; set; }

        [Column("Respuesta3")]
        public string Respuesta3 { get; set; }

        [Column("Respuesta4")]
        public string Respuesta4 { get; set; }

        [Column("Correcta")]
        public string Correcta { get; set; }

        [Column("Dificultad")]
        public string Dificultad { get; set; }

        [Column("OdsRelacionada")]
        public string OdsRelacionada { get; set; }
    }
}