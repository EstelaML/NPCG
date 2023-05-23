using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.BusinessLogic.EstrategiaSonido;
using preguntaods.BusinessLogic.Fachada;
using preguntaods.Entities;
using System;
using System.Threading.Tasks;

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
        private Button guardarCambios;

        private Sonido sonido;
        private Facade fachada;
        private Usuario usuario;

        private int volumenApp;
        private int volumenMusica;

        private bool toggleImagenApp;
        private bool toggleImagenMusica;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaConfiguracion);

            scrollVolumenApp = FindViewById<SeekBar>(Resource.Id.scrollVolumenApp);
            if (scrollVolumenApp != null) scrollVolumenApp.ProgressChanged += ProgressChangedVolumenApp;

            textoVolumenApp = FindViewById<TextView>(Resource.Id.textoVolumenApp);

            imagenApp = FindViewById<ImageButton>(Resource.Id.sonidoApp);
            if (imagenApp != null) imagenApp.Click += ChangeButtonApp;

            scrollVolumenMusica = FindViewById<SeekBar>(Resource.Id.scrollVolumenMusica);
            if (scrollVolumenMusica != null) scrollVolumenMusica.ProgressChanged += ProgressChangedVolumenMusica;

            textoVolumenMusica = FindViewById<TextView>(Resource.Id.textoVolumenMusica);

            imagenMusica = FindViewById<ImageButton>(Resource.Id.sonidoMusica);
            if (imagenMusica != null) imagenMusica.Click += ChangeButtonMusica;

            atras = FindViewById<ImageButton>(Resource.Id.buttonAtras);
            if (atras != null) atras.Click += Atras;

            guardarCambios = FindViewById<Button>(Resource.Id.guardarCambios);
            if (guardarCambios != null) guardarCambios.Click += GuardarCambios;

            fachada = Facade.GetInstance();
            usuario = await fachada.GetUsuarioLogged();

            sonido = new Sonido();
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);

            volumenApp = usuario.Sonidos;
            volumenMusica = usuario.Musica;

            toggleImagenApp = false;
            toggleImagenMusica = false;

            if (scrollVolumenApp != null) scrollVolumenApp.Progress = volumenApp;

            if (textoVolumenApp != null) textoVolumenApp.Text = volumenApp + "%";

            if (scrollVolumenMusica != null) scrollVolumenMusica.Progress = volumenMusica;
            if (textoVolumenMusica != null) textoVolumenMusica.Text = volumenMusica + "%";
        }

        private void Atras(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            var i = new Intent(this, typeof(MenuViewModel));
            StartActivity(i);
            Finish();
        }

        private async void GuardarCambios(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            UserDialogs.Instance.ShowLoading("Guardando cambios...", MaskType.Clear);

            await fachada.UpdateVolumenSonidos(volumenApp);
            await fachada.UpdateVolumenMusica(volumenMusica);

            UserDialogs.Instance.HideLoading();
            UserDialogs.Instance.Alert(new AlertConfig
            {
                Message = "Cambios guardados de manera satisfactoria.",
                OkText = "Entendido"
            });
        }

        private void ProgressChangedVolumenApp(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            if (toggleImagenApp) return;
            textoVolumenApp.Text = e.Progress + "%";
            volumenApp = e.Progress;
        }

        private void ProgressChangedVolumenMusica(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            if (toggleImagenMusica) return;
            textoVolumenMusica.Text = e.Progress + "%";
            volumenMusica = e.Progress;
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
                scrollVolumenApp.Enabled = true;
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