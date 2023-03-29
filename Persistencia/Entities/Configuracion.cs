using Postgrest.Attributes;
using Postgrest.Models;

namespace preguntaods.Entities
{
    [Table("Configuracion")]
    public partial class Configuracion : BaseModel, IEntity
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
    }
}