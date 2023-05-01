using Postgrest.Attributes;
using Postgrest.Models;


namespace preguntaods.Entities
{

    [Table("Estadisticas")]
    public partial class Estadisticas : BaseModel, IEntity
    {
        [PrimaryKey("id")]
        public int? Id { get; set; }

        [Column("Usuario")]
        public int Usuario { get; set; }

        [Column("Puntuacion")]
        public int Puntuacion { get; set;}

        [Column("Aciertos")]
        public int[] Aciertos { get; set; }

        [Column("Fallos")]
        public int[] Fallos { get; set; }
    }
}