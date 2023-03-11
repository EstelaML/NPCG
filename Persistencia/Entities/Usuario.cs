using Android.App;
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

namespace pruebasEF.Entities
{
    [Table("User")]
    public partial class Usuario
    {
        [Key]
        [Column("id")]
        public int? id { get; set; }

        [Column("fecha_creacion")]
        public DateTime? fecha_creacion { get; set; }

        [Column("nombre")]
        public string nombre { get; set; }

        [Column("email")]
        public string email { get; set; }

        [Column("contraseña")]
        public string contraseña { get; set; }

        [Column("avatar_url")]
        public string? avatar_url { get; set; }
    }
}