using Android.Media;

namespace preguntaods
{
    internal class EstrategiaSonidoClick : IEstrategiaSonido
    {
        private MediaPlayer mp;
        public EstrategiaSonidoClick() {
            mp = new MediaPlayer();
        }
        public void Play(Android.Content.Context t)
        {
            Android.Net.Uri uri = Android.Net.Uri.Parse("android.resource://" + t.PackageName + "/" + Resource.Raw.sonido_click);
            mp.SetDataSource(t, uri);
            mp.Prepare();
            mp.Start();
        }

        public void Stop() {
            mp.Stop();
        }
    }
}