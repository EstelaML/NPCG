using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.BusinessLogic.EstrategiaSonido;
using preguntaods.BusinessLogic.Services;
using preguntaods.Entities;
using System;
using Xamarin.Essentials;

namespace preguntaods.Presentacion.ViewModels
{
    [Activity(Label = "", Theme = "@style/HiddenTitleTheme")]
    public class PerfilViewModel : AppCompatActivity
    {
        private Sonido sonido;
        private Facade fachada;
        private Usuario usuario;
        private TextView nombre;
        private TextView aciertos;
        private TextView fallos;
        private TextView puntuacion;
        private ImageButton avatar;
        private Estadistica estadisticas;


        private int retosAcertados;
        private int retosFallados;
        private int retosTotales;
        private int probAcierto;
        private int probFallo;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaPerfil);

            fachada = new Facade();

            usuario = await fachada.GetUsuarioLogged();

            estadisticas = await fachada.PedirEstadisticas(usuario.Uuid);

            sonido = new Sonido();
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);

            var atras = FindViewById<ImageButton>(Resource.Id.buttonAtras);
            if (atras != null) atras.Click += Atras;

            var editar = FindViewById<ImageButton>(Resource.Id.buttonEditarTexto);
            if (editar != null) editar.Click += Atras;

            avatar = FindViewById<ImageButton>(Resource.Id.buttonAvatar);
            if (avatar != null) avatar.Click += cambiarFoto;

            nombre = FindViewById<TextView>(Resource.Id.textViewNombre);
            aciertos = FindViewById<TextView>(Resource.Id.textViewAciertos);
            fallos = FindViewById<TextView>(Resource.Id.textViewFallos);
            puntuacion = FindViewById<TextView>(Resource.Id.textViewPuntuacion);

            Init();
        }

        private void Init()
        {
            nombre.Text = usuario.Nombre;

            puntuacion.Text = estadisticas.Puntuacion.ToString();

            retosFallados = estadisticas.Fallos.Length;
            retosAcertados = estadisticas.Aciertos.Length;

            retosTotales = retosFallados + retosAcertados;
            probAcierto = (int)((retosAcertados / (float)retosTotales) * 100);

            aciertos.Text = probAcierto < 0 ? "0" : probAcierto.ToString();

            probFallo = (100 - probAcierto);

            fallos.Text = probAcierto < 0 ? "0" : probFallo.ToString();
        }

        public async void cambiarFoto(object sender, EventArgs e) 
        {

            try
            {
                // Pedir al usuario que seleccione una imagen de la galería
                FileResult fileResult = await MediaPicker.PickPhotoAsync();

                // Si el usuario seleccionó una imagen, mostrarla en la ImageView de la foto de perfil
                if (fileResult != null)
                {
                    Bitmap bitmap = await BitmapFactory.DecodeFileAsync(fileResult.FullPath);
                    bitmap.
                    avatar.SetImageBitmap(bitmap);
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que se produzca al seleccionar la imagen
                // por ejemplo, si el usuario cancela la selección
                Console.WriteLine(ex.Message);
            }

        }

        private void Atras(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            var i = new Intent(this, typeof(MenuViewModel));
            StartActivity(i);
        }
    }
}