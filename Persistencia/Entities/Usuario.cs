using Postgrest.Attributes;
using Postgrest.Models;

// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    [Table("Usuario")]
    public partial class Usuario : BaseModel, IEntity
    {
        [PrimaryKey]
        public int? Id { get; set; }

        [Column]
        public string Uuid { get; set; }

        [Column]
        public string Nombre { get; set; }

        [Column]
        public int Puntos { get; set; }

        [Column]
        public bool Sonidos { get; set; }

        [Column]
        public int Musica { get; set; }

        [Column]
        public int[] PreguntasRealizadas { get; set; }
    }
}