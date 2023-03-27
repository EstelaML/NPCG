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
    [Table("ods")]
    public partial class ODS : BaseModel, IEntity
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("Nombre")]
        public string Nombre { get; set; }

        [Column("Descripcion")]
        public string Descripcion { get; set; }

        [Column("Imagen")]
        public string Imagen { get; set; }
    }
}