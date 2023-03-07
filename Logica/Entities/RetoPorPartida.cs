using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using pruebasEF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pruebasEF.Entities
{
    public partial class RetoPorPartida
    {
        public RetoPorPartida() { }


        public RetoPorPartida(Reto r, Partida p)
        {
            RPartida = p;
            PartidaId = p.Id;

            RReto = r;
            RetoId = r.Id;
        }
    }
}