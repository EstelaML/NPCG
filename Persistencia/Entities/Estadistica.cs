using Postgrest.Attributes;
using Postgrest.Models;
using System;

namespace preguntaods.Entities
{
    [Table("Estadisticas")]
    public partial class Estadistica : BaseModel, IEntity
    {
        [PrimaryKey("id")]
        public int? Id { get; set; }

        [Column("Usuario")]
        public string Usuario { get; set; }

        [Column("Puntuacion")]
        public int Puntuacion { get; set; }

        [Column("Aciertos")]
        public int[] Aciertos { get; set; }

        [Column("Fallos")]
        public int[] Fallos { get; set; }

        [Column("Nombre")]
        public string Nombre { get; set; }

        [Column("Tiempo")]
        public float? Tiempo { get; set; }
    }
}