using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace preguntaods.Entities
{
    public partial class Usuario
    {
        public Usuario() { }
        public Usuario(int id, DateTime fechaCreacion, string nombre, string email, string contraseña, string image)
        {
            this.Id = id;
            this.fecha_creacion = fechaCreacion;
            this.nombre = nombre;
            this.email = email;
            this.contraseña = contraseña;
            this.avatar_url = image;
        }

    }
}