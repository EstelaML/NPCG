using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Postgrest.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pruebasEF.Entities
{
    [Table("reto_pregunta")]
    public partial class RetoPregunta : Reto
    {
        [Column("pregunta")]
        public string Enunciado { get; set; }

        [Column("respuesta1")]
        public string Respuesta1 { get; set; }

        [Column("pregunta2")]
        public string Respuesta2 { get; set; }

        [Column("pregunta3")]
        public string Respuesta3 { get; set; }

        [Column("pregunta4")]
        public string Respuesta4 { get; set; }

        [Column("correcta")]
        public string Solucion { get; set; }
    }
}