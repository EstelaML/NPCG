using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace preguntaods
{
    internal class Sonido
    {
        private MediaPlayer mp;
        public Sonido() {
            mp = new MediaPlayer();
        }

        public void PararSonido() {
            mp.Stop();
        }

        public void HacerSonido(Android.Content.Context t, Android.Net.Uri uri) {
            mp.SetDataSource(t, uri);
            mp.Prepare();
            mp.Start();
        }
    }
}