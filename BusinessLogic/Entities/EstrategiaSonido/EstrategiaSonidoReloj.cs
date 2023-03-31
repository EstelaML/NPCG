using Android.Media;

namespace preguntaods
{
    internal class EstrategiaSonidoReloj : IEstrategiaSonido
    {
        private static MediaPlayer mp;
        public EstrategiaSonidoReloj()
        {
            //Si el MediaPlayer de la Musica no existe, lo crea
            if (mp == null)
            {
                mp = new MediaPlayer();
            }
            
        }
        public void Play(Android.Content.Context t)
        {
            Android.Net.Uri uri = Android.Net.Uri.Parse("android.resource://" + t.PackageName + "/" + Resource.Raw.sonido_reloj);
            mp.SetDataSource(t, uri);
            mp.Prepare();
            mp.Start();
        }

        public void Stop() {
            mp.Stop();
        }
    }
}