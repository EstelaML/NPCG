using Android.Media;
using System;

namespace preguntaods.BusinessLogic.EstrategiaSonido
{
    internal class EstrategiaSonidoMusica : IEstrategiaSonido
    {
        private readonly MediaPlayer mp;

        public EstrategiaSonidoMusica()
        {
            mp = new MediaPlayer();
        }

        public void Play(Android.Content.Context t)
        {
            float leftVolume = 0.4f;
            float rightVolume = 0.4f;

            var uri = Android.Net.Uri.Parse("android.resource://" + t.PackageName + "/" + Resource.Raw.sonido_musica);
            mp.SetDataSource(t, uri ?? throw new InvalidOperationException());
            mp.SetVolume(leftVolume, rightVolume);
            mp.Prepare();
            mp.Start();
        }

        public void Stop()
        {
            mp.Stop();
            mp.Reset();
        }
    }
}