using Postgrest.Attributes;
using Postgrest.Models;

namespace preguntaods.Entities
{
    [Table("Usuario")]
    public partial class Usuario : BaseModel, IEntity
    {
        [PrimaryKey("Id")]
        public int? Id { get; set; }

        [Column("UUID")]
        public string Uuid { get; set; }

        [Column("Nombre")]
        public string Nombre { get; set; }

        [Column("Puntos")]
        public int Puntos { get; set; }

        [Column("Sonidos")]
        public bool Sonidos { get; set; }

        [Column("Musica")]
        public int Musica { get; set; }

        [Column("PreguntasRealizadas")]
        public int[] PreguntasRealizadas { get; set; }
    }
}