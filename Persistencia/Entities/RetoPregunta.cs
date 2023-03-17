﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Postgrest.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace preguntaods.Entities
{
    [Table("Reto_preguntas")]
    public partial class RetoPregunta 
    {
        [Key]
        [Column("id")]
        public int? id { get; set; }

        [Column("Pregunta")]
        public string Pregunta { get; set; }

        [Column("Respuesta1")]
        public string Respuesta1 { get; set; }

        [Column("Respuesta2")]
        public string Respuesta2 { get; set; }

        [Column("Respuesta3")]
        public string Respuesta3 { get; set; }

        [Column("Respuesta4")]
        public string Respuesta4 { get; set; }

        [Column("Correcta")]
        public string Correcta { get; set; }
    }
}