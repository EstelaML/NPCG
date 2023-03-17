using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using preguntaods.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace preguntaods.Entities
{
    [Table("reto_partida")]
    public partial class RetoPorPartida
    {
        [ForeignKey("RetoId")]
        public Reto RReto { get; set; }

        [Key]
        [Column("retoId")]
        public int RetoId { get; set; }

        [ForeignKey("PartidaId")]
        public Partida RPartida { get; set; }

        
        [Column("partidaId")]
        public int PartidaId { get; set; }
    }
}