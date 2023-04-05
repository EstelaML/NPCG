using Android.Media;

namespace preguntaods
{
    internal class EstrategiaSonidoMusica : IEstrategiaSonido
    {
        private MediaPlayer mp;
        public EstrategiaSonidoMusica() {
            //Si el MediaPlayer de la Musica no existe, lo crea
            if (mp == null)
            {
                mp = new MediaPlayer();
            }
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
        }
    }
}