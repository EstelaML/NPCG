using Postgrest.Attributes;
using Postgrest.Models;

// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    [Table("Estadisticas")]
    public partial class Estadistica : BaseModel, IEntity
    {
        // ReSharper disable once ExplicitCallerInfoArgument
        [PrimaryKey("id")]
        public int? Id { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Usuario")]
        public string Usuario { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Puntuacion")]
        public int Puntuacion { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Aciertos")]
        public int[] Aciertos { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Fallos")]
        public int[] Fallos { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Nombre")]
        public string Nombre { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Tiempo")]
        public float? Tiempo { get; set; }
    }
}