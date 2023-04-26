using System;
using Android.Media;

namespace preguntaods.BusinessLogic.EstrategiaSonido
{
    internal class EstrategiaSonidoDerrota : IEstrategiaSonido
    {
        private readonly MediaPlayer mp;

        public EstrategiaSonidoDerrota()
        {
            mp = new MediaPlayer();
        }

        public void Play(Android.Content.Context t)
        {
            Android.Net.Uri uri = Android.Net.Uri.Parse("android.resource://" + t.PackageName + "/" + Resource.Raw.sonido_derrota);
            mp.SetDataSource(t, uri ?? throw new InvalidOperationException());
            mp.Prepare();
            mp.Start();
        }

        public void Stop()
        {
            mp.Stop();
        }
    }
}