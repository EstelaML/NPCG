using System;
using Android.Media;
using preguntaods.BusinessLogic.Services;

namespace preguntaods.BusinessLogic.Sonidos
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
            var leftVolume = (float)(PreguntadosService.GetInstance().volumenSonidos / 100.0);
            var rightVolume = (float)(PreguntadosService.GetInstance().volumenSonidos / 100.0);

            var uri = Android.Net.Uri.Parse("android.resource://" + t.PackageName + "/" + Resource.Raw.tictac);
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