using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Systems;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace preguntaods.Entities
{
    public partial class Reto
    {
        public Reto(int id, int dificultad, int oDSFk, ODS oDS)
        {
            this.Id = id;
            this.Dificultad = dificultad;
            this.Ods_tratada = oDSFk;
            this.Ods = oDS;
            RetoPorPartidas = new List<RetoPorPartida>();
        }
        public Reto() { RetoPorPartidas = new List<RetoPorPartida>(); }

        public void AddRetoPorPartida(RetoPorPartida retoPorPartida)
        {
            RetoPorPartidas.Add(retoPorPartida);
        }
    }
}