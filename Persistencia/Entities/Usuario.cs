﻿using Postgrest.Attributes;
using Postgrest.Models;

// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    [Table("Usuario")]
    public partial class Usuario : BaseModel, IEntity
    {
        // ReSharper disable once RedundantArgumentDefaultValue
        [PrimaryKey("Id")]
        public int? Id { get; set; }

        // ReSharper disable once ExplicitCallerInfoArgument
        [Column("UUID")]
        public string Uuid { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Nombre")]
        public string Nombre { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Sonidos")]
        public int Sonidos { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Musica")]
        public int Musica { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Foto")]
        public string Foto { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("Nivel")]
        public int Nivel { get; set; }

        // ReSharper disable once RedundantArgumentDefaultValue
        [Column("VolumenActivado")]
        public int[] VolumenActivado { get; set; }
    }
}