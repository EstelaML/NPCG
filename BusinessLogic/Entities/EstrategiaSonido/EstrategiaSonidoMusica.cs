using Android.Media;

namespace preguntaods
{
    internal class EstrategiaSonidoMusica : IEstrategiaSonido
    {
        private MediaPlayer mp;
        public EstrategiaSonidoMusica() {
            mp = new MediaPlayer();
        }
        public void Play(Android.Content.Context t)
        {
            Android.Net.Uri uri = Android.Net.Uri.Parse("android.resource://" + t.PackageName + "/" + Resource.Raw.sonido_musica);
            mp.SetDataSource(t, uri);
            mp.Prepare();
            mp.Start();
        }

        public void Stop() {
            mp.Stop();
            mp.Reset();
        }
    }
}