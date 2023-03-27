using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Postgrest.Attributes;
using Postgrest.Models;
using preguntaods.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ColumnAttribute = Postgrest.Attributes.ColumnAttribute;
using TableAttribute = Postgrest.Attributes.TableAttribute;

namespace preguntaods.Entities
{
    [Table("reto_partida")]
    public partial class RetoPorPartida : BaseModel, IEntity
    {
        [ForeignKey("RetoId")]
        public Reto RReto { get; set; }

        [PrimaryKey("id")]
        public int Id { get; set; }

        [ForeignKey("PartidaId")]
        public Partida RPartida { get; set; }

        
        [Column("partidaId")]
        public int PartidaId { get; set; }
    }
}