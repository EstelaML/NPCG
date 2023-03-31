using Postgrest.Attributes;
using Postgrest.Models;
using System.Collections.Generic;

namespace preguntaods.Entities
{
    [Table("Configuracion")]
    public partial class Usuario : BaseModel, IEntity
    {
        [PrimaryKey("Id")]
        public int Id { get; set; }

        [Column("Nombre")]
        public string Nombre{ get; set; }

        [Column("Puntos")]
        public int Puntos { get; set; }

        [Column("Sonnidos")]
        public bool Sonidos { get; set; }

        [Column("Musica")]
        public int Musica { get; set; }

        [Column("PreguntasRealizadas")]
        public IEnumerable<int> PreguntasRealizadas { get; set; }
    }
}