using Android.Media;

namespace preguntaods
{
    internal class EstrategiaSonidoAcierto : IEstrategiaSonido
    {
        private MediaPlayer mp;

        public EstrategiaSonidoAcierto()
        {
            mp = new MediaPlayer();
        }

        public void Play(Android.Content.Context t)
        {
            Android.Net.Uri uri = Android.Net.Uri.Parse("android.resource://" + t.PackageName + "/" + Resource.Raw.sonido_acierto);
            mp.SetDataSource(t, uri);
            mp.Prepare();
            mp.Start();
        }

        public void Stop()
        {
            mp.Stop();
        }
    }
}