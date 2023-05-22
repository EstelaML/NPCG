using Android.Media;
using preguntaods.BusinessLogic.Services;
using System;

namespace preguntaods.BusinessLogic.EstrategiaSonido
{
    internal class EstrategiaSonidoVictoria : IEstrategiaSonido
    {
        private readonly MediaPlayer mp;

        public EstrategiaSonidoVictoria()
        {
            mp = new MediaPlayer();
        }

        public void Play(Android.Content.Context t)
        {
            var leftVolume = (float)(PreguntadosService.GetInstance().volumenSonidos / 100.0);
            var rightVolume = (float)(PreguntadosService.GetInstance().volumenSonidos / 100.0);

            var uri = Android.Net.Uri.Parse("android.resource://" + t.PackageName + "/" + Resource.Raw.sonido_victoria);
            mp.SetDataSource(t, uri ?? throw new InvalidOperationException());
            mp.SetVolume(leftVolume, rightVolume);
            mp.Prepare();
            mp.Start();
        }

        public void Stop()
        {
            mp.Stop();
        }
    }
}