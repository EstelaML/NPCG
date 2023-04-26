using Android.Media;
using System;

namespace preguntaods.BusinessLogic.EstrategiaSonido
{
    internal class EstrategiaSonidoLetraAcierto : IEstrategiaSonido
    {
        private readonly MediaPlayer mp;

        public EstrategiaSonidoLetraAcierto()
        {
            mp = new MediaPlayer();
        }

        public void Play(Android.Content.Context t)
        {
            Android.Net.Uri uri = Android.Net.Uri.Parse("android.resource://" + t.PackageName + "/" + Resource.Raw.letra_correcta_sonido);
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