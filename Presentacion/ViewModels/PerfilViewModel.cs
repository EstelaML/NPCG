using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.BusinessLogic.EstrategiaSonido;
using preguntaods.BusinessLogic.Services;
using preguntaods.Entities;
using System;
using System.Collections.Generic;
using System.IO;
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
            if (avatar != null) avatar.Click += CambiarFoto;

            nombre = FindViewById<TextView>(Resource.Id.textViewNombre);
            aciertos = FindViewById<TextView>(Resource.Id.textViewAciertos);
            fallos = FindViewById<TextView>(Resource.Id.textViewFallos);
            puntuacion = FindViewById<TextView>(Resource.Id.textViewPuntuacion);

            Init();
        }

        private void Init()
        {
            nombre.Text = usuario.Nombre;

            iniciarFoto();

            puntuacion.Text = estadisticas.Puntuacion.ToString();

            retosFallados = estadisticas.Fallos.Length;
            retosAcertados = estadisticas.Aciertos.Length;

            retosTotales = retosFallados + retosAcertados;
            probAcierto = (int)((retosAcertados / (float)retosTotales) * 100);

            aciertos.Text = probAcierto < 0 ? "0" : probAcierto.ToString();

            probFallo = (100 - probAcierto);

            fallos.Text = probAcierto < 0 ? "0" : probFallo.ToString();
        }

        public async void CambiarFoto(object sender, EventArgs e) 
        {
            // Obtener una lista de IDs de recursos de imágenes predefinidas
            List<int> profilePictureIds = new List<int>
            {
                 Resource.Drawable.icon_hombre,
                 Resource.Drawable.icon_mujer,
                 // Agregar aquí más IDs de recursos de imágenes predefinidas
            };

            // Crear una lista de miniaturas de las imágenes predefinidas
            List<Bitmap> profilePictures = new List<Bitmap>();
            foreach (int id in profilePictureIds)
            {
                profilePictures.Add(BitmapFactory.DecodeResource(Resources, id));
            }

            // Mostrar un cuadro de diálogo emergente con la lista de miniaturas de las imágenes predefinidas
            List<string> pictureTitles = new List<string> { "Hombre", "Mujer" }; // Aquí puede agregar títulos para las imágenes
            string selectedPictureTitle = await UserDialogs.Instance.ActionSheetAsync("Seleccionar imagen", "Cancelar", null, null , pictureTitles.ToArray());
            int selectedPictureIndex = pictureTitles.IndexOf(selectedPictureTitle);

            if (selectedPictureIndex >= 0)
            {
                // Convertir la imagen seleccionada en un arreglo de bytes
                byte[] photoData = ConvertBitmapToByteArray(profilePictures[selectedPictureIndex]);


                // Convertir el arreglo de bytes en un Bitmap
                Bitmap selectedPhoto = BitmapFactory.DecodeByteArray(photoData, 0, photoData.Length);

                // Actualizar la imagen de perfil del usuario con la imagen seleccionada
                avatar.SetImageBitmap(selectedPhoto);

                // Actualizar la imagen de perfil del usuario con la imagen seleccionada
                await fachada.CambiarFoto(usuario.Uuid,photoData);
            }

            
        }

        private void iniciarFoto()
        {
            var uf = usuario.Foto;

            if (uf != null) 
            { 
                var foto = Convert.FromBase64String(uf);

                avatar.SetImageBitmap(BitmapFactory.DecodeByteArray(foto, 0, foto.Length));
            }
        }

        private void Atras(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            var i = new Intent(this, typeof(MenuViewModel));
            StartActivity(i);
        }

        public byte[] ConvertBitmapToByteArray(Bitmap bitmap)
        {
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);
                return stream.ToArray();
            }
        }
    }
}