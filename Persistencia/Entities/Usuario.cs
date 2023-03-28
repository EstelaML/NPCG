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
    [Table("Configuracion")]
    public partial class Usuario : BaseModel, IEntity
    {
        [PrimaryKey("Id")]
        public int Id { get; set; }

        [Column("Nombre")]
        public string Nombre{ get; set; }

        [Column("Puntos")]
        public int Puntos { get; set; }

        [Column("Sonnidos")]
        public bool Sonidos { get; set; }

        [Column("Musica")]
        public int Musica { get; set; }
    }
}