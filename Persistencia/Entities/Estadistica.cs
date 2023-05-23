using Postgrest.Attributes;
using Postgrest.Models;
using System;

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

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("PartidasGanadas")]
        public int PartidasGanadas { get; set; }
        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("PuntuacionDiaria")]
        public int PuntuacionDiaria { get; set; }
        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("FechaDiaria")]
        public DateTime? FechaDiaria { get; set; }
        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("PuntuacionSemanal")]
        public int PuntuacionSemanal { get; set; }
    }
}