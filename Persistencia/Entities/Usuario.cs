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
    [Table("User")]
    public partial class Usuario : BaseModel, IEntity
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

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