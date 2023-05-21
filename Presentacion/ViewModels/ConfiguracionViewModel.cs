using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Text;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.BusinessLogic.EstrategiaSonido;
using System;

namespace preguntaods.Presentacion.ViewModels
{
    [Activity(Label = "", Theme = "@style/HiddenTitleTheme")]
    public class ConfiguracionViewModel : AppCompatActivity
    {
        private SeekBar scrollVolumenApp;
        private SeekBar scrollVolumenMusica;
        private TextView textoVolumenApp;
        private TextView textoVolumenMusica;
        private ImageButton atras;
        private ImageButton imagenApp;
        private ImageButton imagenMusica;

        private Sonido sonido;

        private int volumenApp;
        private int volumenMusica;

        private bool toggleImagenApp;
        private bool toggleImagenMusica;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaConfiguracion);

            scrollVolumenApp = FindViewById<SeekBar>(Resource.Id.scrollVolumenApp);
            scrollVolumenApp.ProgressChanged += ProgressChangedVolumenApp;

            textoVolumenApp = FindViewById<TextView>(Resource.Id.textoVolumenApp);

            imagenApp = FindViewById<ImageButton>(Resource.Id.sonidoApp);
            imagenApp.Click += ChangeButtonApp;

            scrollVolumenMusica = FindViewById<SeekBar>(Resource.Id.scrollVolumenMusica);
            scrollVolumenMusica.ProgressChanged += ProgressChangedVolumenMusica;

            textoVolumenMusica = FindViewById<TextView>(Resource.Id.textoVolumenMusica);

            imagenMusica = FindViewById<ImageButton>(Resource.Id.sonidoMusica);
            imagenMusica.Click += ChangeButtonMusica;

            atras = FindViewById<ImageButton>(Resource.Id.buttonAtras);
            atras.Click += Atras;

            sonido = new Sonido();
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);

            volumenApp = 100;
            volumenMusica = 100;

            toggleImagenApp = false;
            toggleImagenMusica = false;

            scrollVolumenApp.Progress = volumenApp;
            textoVolumenApp.Text = volumenApp + "%";

            scrollVolumenMusica.Progress = volumenMusica;
            textoVolumenMusica.Text = volumenMusica + "%";
        }

        private void Atras(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            var i = new Intent(this, typeof(MenuViewModel));
            StartActivity(i);
            Finish();
        }

        private void ProgressChangedVolumenApp(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            if (toggleImagenApp) return;
            textoVolumenApp.Text = e.Progress + "%";
        }

        private void ProgressChangedVolumenMusica(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            if (toggleImagenMusica) return;
            textoVolumenMusica.Text = e.Progress + "%";
        }

        private void ChangeButtonApp(object sender, EventArgs e)
        {
            toggleImagenApp = !toggleImagenApp;
            if (toggleImagenApp)
            {
                imagenApp.SetImageResource(Resource.Drawable.icon_silencio);
                textoVolumenApp.Text = "0%";
                scrollVolumenApp.Progress = 0;
                scrollVolumenApp.Enabled = false;
            }
            else
            {
                imagenApp.SetImageResource(Resource.Drawable.icon_sonido);
                scrollVolumenApp.Enabled = false;
            }
        }

        private void ChangeButtonMusica(object sender, EventArgs e)
        {
            toggleImagenMusica = !toggleImagenMusica;
            if (toggleImagenMusica)
            {
                imagenMusica.SetImageResource(Resource.Drawable.icon_silencio);
                textoVolumenMusica.Text = "0%";
                scrollVolumenMusica.Progress = 0;
                scrollVolumenMusica.Enabled = false;
            }
            else
            {
                imagenMusica.SetImageResource(Resource.Drawable.icon_sonido);
                scrollVolumenMusica.Enabled = true;
            }
        }
    }

}