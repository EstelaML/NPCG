using Postgrest.Attributes;
using Postgrest.Models;
using System.ComponentModel.DataAnnotations.Schema;

using ColumnAttribute = Postgrest.Attributes.ColumnAttribute;
using TableAttribute = Postgrest.Attributes.TableAttribute;

namespace preguntaods.Entities
{
    [Table("reto")]
    public partial class Reto : BaseModel, IEntity
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("dificultad")]
        public int Dificultad { get; set; }

        [Column("ods_tratada")]
        public int Ods_tratada { get; set; }

        [ForeignKey("ods_tratada")]
        public ODS Ods { get; set; }
    }
}