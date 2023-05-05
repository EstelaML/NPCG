using Postgrest.Attributes;
using Postgrest.Models;

// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    [Table("Estadisticas")]
    public partial class Estadistica : BaseModel, IEntity
    {
        [PrimaryKey]
        public int? Id { get; set; }

        [Column]
        public string Usuario { get; set; }

        [Column]
        public int Puntuacion { get; set; }

        [Column]
        public int[] Aciertos { get; set; }

        [Column]
        public int[] Fallos { get; set; }

        [Column]
        public string Nombre { get; set; }
    }
}