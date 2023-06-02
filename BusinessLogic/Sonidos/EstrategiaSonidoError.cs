using System;
using Android.Media;
using preguntaods.BusinessLogic.Services;

namespace preguntaods.BusinessLogic.Sonidos
{
    internal class EstrategiaSonidoError : IEstrategiaSonido
    {
        private readonly MediaPlayer mp;

        public EstrategiaSonidoError()
        {
            mp = new MediaPlayer();
        }

        public void Play(Android.Content.Context t)
        {
            var leftVolume = (float)(PreguntadosService.GetInstance().volumenSonidos / 100.0);
            var rightVolume = (float)(PreguntadosService.GetInstance().volumenSonidos / 100.0);

            var uri = Android.Net.Uri.Parse("android.resource://" + t.PackageName + "/" + Resource.Raw.sonido_error);
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