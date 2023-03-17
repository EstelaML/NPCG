using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using preguntaods.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace preguntaods.Entities
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