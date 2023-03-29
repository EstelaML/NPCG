using Android.Media;

namespace preguntaods
{
    internal class Sonido
    {
        private MediaPlayer mp;
        public Sonido() {
            mp = new MediaPlayer();
        }

        public void PararSonido() {
            mp.Stop();
        }

        public void HacerSonido(Android.Content.Context t, Android.Net.Uri uri) {
            mp.SetDataSource(t, uri);
            mp.Prepare();
            mp.Start();
        }
    }
}