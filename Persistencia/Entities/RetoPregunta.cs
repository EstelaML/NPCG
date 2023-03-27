using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Postgrest.Attributes;
using Postgrest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace preguntaods.Entities
{
    [Table("Reto_preguntas")]
    public partial class RetoPregunta : BaseModel, IEntity
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

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

        [Column("Dificultad")]
        public string Dificultad { get; set; }

        [Column("OdsRelacionada")]
        public string OdsRelacionada { get; set; }
    }
}