using System;
using Android.Media;
using preguntaods.BusinessLogic.Services;

namespace preguntaods.BusinessLogic.Sonidos
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
            var leftVolume = (float)(PreguntadosService.GetInstance().volumenSonidos / 100.0);
            var rightVolume = (float)(PreguntadosService.GetInstance().volumenSonidos / 100.0);

            var uri = Android.Net.Uri.Parse("android.resource://" + t.PackageName + "/" + Resource.Raw.letra_correcta_sonido);
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