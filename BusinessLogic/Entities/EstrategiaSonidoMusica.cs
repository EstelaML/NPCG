using Android.Media;
using Java.IO;
using System;
using Console = System.Console;

namespace preguntaods
{
    internal class EstrategiaSonidoMusica : IEstrategiaSonido
    {
        private static MediaPlayer mp;
        public EstrategiaSonidoMusica() {
            if (mp == null)
            {
                mp = new MediaPlayer();
            }
        }
        public void Play(Android.Content.Context t)
        {
            Android.Net.Uri uri = Android.Net.Uri.Parse("android.resource://" + t.PackageName + "/" + Resource.Raw.sonido_musica);
            mp.SetDataSource(t, uri);
            mp.Prepare();
            mp.Start();
        }

        public void Stop() {
            mp.Stop();
        }
    }
}