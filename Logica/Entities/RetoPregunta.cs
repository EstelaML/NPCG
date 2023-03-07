using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pruebasEF.Entities
{
    public partial class RetoPregunta : Reto
    { 
        public RetoPregunta() { }
        public RetoPregunta(string enunciado, string respuesta1, string respuesta2, string respuesta3, string respuesta4, string solucion)
        {
            Enunciado = enunciado;
            Respuesta1 = respuesta1;
            Respuesta2 = respuesta2;
            Respuesta3 = respuesta3;
            Respuesta4 = respuesta4;
            Solucion = solucion;
        }
    }
}