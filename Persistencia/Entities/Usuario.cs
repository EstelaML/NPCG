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
        public int Id { get; set; }

        [Column("fecha_creacíon")]
        public string FechaCreacion { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("contraseña")]
        public string Contraseña { get; set; }

        [Column("avatar_url")]
        public string Image { get; set; }
    }
}