﻿using Android.App;
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
    [Table("reto")]
    public partial class Reto
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("dificultad")]
        public int Dificultad { get; set; }

        [Column("ods_tratada")]
        public int Ods_tratada { get; set; }

        [ForeignKey("ods_tratada")]
        public ODS Ods { get; set; }
        public List<RetoPorPartida> RetoPorPartidas { get; set; }

    }
}