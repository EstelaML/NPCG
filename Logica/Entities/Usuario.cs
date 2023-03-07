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

namespace pruebasEF.Entities
{
    public partial class Usuario
    {
        public Usuario() { }
        public Usuario(int id, string fechaCreacion, string nombre, string email, string contraseña, string image)
        {
            this.Id = id;
            this.FechaCreacion = fechaCreacion;
            this.Nombre = nombre;
            this.Email = email;
            this.Contraseña = contraseña;
            this.Image = image;
        }

    }
}