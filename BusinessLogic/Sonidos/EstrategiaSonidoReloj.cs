using Android.Media;
using System;

namespace preguntaods.BusinessLogic.EstrategiaSonido
{
    internal class EstrategiaSonidoReloj : IEstrategiaSonido
    {
        private readonly MediaPlayer mp;

        public EstrategiaSonidoReloj()
        {
            mp = new MediaPlayer();
        }

        public void Play(Android.Content.Context t)
        {
            var uri = Android.Net.Uri.Parse("android.resource://" + t.PackageName + "/" + Resource.Raw.tictac);
            mp.SetDataSource(t, uri ?? throw new InvalidOperationException());
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