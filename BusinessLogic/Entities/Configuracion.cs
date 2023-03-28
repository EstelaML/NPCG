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
    public partial class Configuracion
    {
        public Configuracion() { }
        public Configuracion(int id, string nombre, bool sonidos, int puntos, int musica)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Sonidos = sonidos;
            this.Puntos = puntos;
            this.Musica = musica;
        }
    }
}