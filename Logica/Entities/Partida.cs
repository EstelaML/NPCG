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
    public partial class Partida
    {
        public Partida(int id, DateTime fecha, RetoPorPartida retoPorPartida)
        {
            Id = id;
            Fecha = fecha;
            RetoPorPartidas = new List<RetoPorPartida> { retoPorPartida };
        }
        public Partida()
        {
            RetoPorPartidas = new List<RetoPorPartida>();
        }
    }
}