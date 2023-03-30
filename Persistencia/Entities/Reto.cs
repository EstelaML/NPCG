using Postgrest.Attributes;
using Postgrest.Models;
using System.ComponentModel.DataAnnotations.Schema;

using ColumnAttribute = Postgrest.Attributes.ColumnAttribute;
using TableAttribute = Postgrest.Attributes.TableAttribute;

namespace preguntaods.Entities
{
    [Table("Reto")]
    public partial class Reto : BaseModel, IEntity
    {
        [PrimaryKey("Id")]
        public int Id { get; set; }

        [Column("Dificultad")]
        public int Dificultad { get; set; }

        [Column("OdsTratada")]
        public int Ods_tratada { get; set; }

        [ForeignKey("OdsTratada")]
        public ODS Ods { get; set; }
    }
}