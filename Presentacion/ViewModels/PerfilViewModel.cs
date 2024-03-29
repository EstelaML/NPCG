﻿using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.BusinessLogic.Fachada;
using preguntaods.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using preguntaods.BusinessLogic.Sonidos;

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
        private TextView tiempo;
        private TextView nivel;
        private TextView requisitos;
        private ImageButton avatar;
        private Button coberturaODS;
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

            fachada = Facade.GetInstance();

            usuario = await fachada.GetUsuarioLogged();

            estadisticas = await fachada.PedirEstadisticas(usuario.Uuid);

            UserDialogs.Init(this);

            sonido = new Sonido();
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);

            var atras = FindViewById<ImageButton>(Resource.Id.buttonAtras);
            if (atras != null) atras.Click += Atras;

            var editar = FindViewById<ImageButton>(Resource.Id.buttonEditarTexto);
            if (editar != null) editar.Click += CambiarNombre;

            avatar = FindViewById<ImageButton>(Resource.Id.buttonAvatar);
            if (avatar != null) avatar.Click += CambiarFoto;

            coberturaODS = FindViewById<Button>(Resource.Id.coberturaOds);
            if (coberturaODS != null) coberturaODS.Click += CoberturaODS_Click;

            nombre = FindViewById<TextView>(Resource.Id.textViewNombre);
            aciertos = FindViewById<TextView>(Resource.Id.textViewAciertos);
            fallos = FindViewById<TextView>(Resource.Id.textViewFallos);
            puntuacion = FindViewById<TextView>(Resource.Id.textViewPuntuacion);
            tiempo = FindViewById<TextView>(Resource.Id.textViewTiempo);
            nivel = FindViewById<TextView>(Resource.Id.textViewNivel);
            requisitos = FindViewById<TextView>(Resource.Id.textViewRequisitos);

            UserDialogs.Instance.ShowLoading("Cargando...");

            await Init();

            UserDialogs.Instance.HideLoading();
        }

        private async Task Init()
        {
            await Task.Delay(500);

            nombre.Text = usuario.Nombre;

            IniciarFoto();

            IniciarTiempo();

            IniciarRequisitos();

            puntuacion.Text = estadisticas.Puntuacion.ToString();

            nivel.Text = "Nivel " + usuario.Nivel;

            retosFallados = estadisticas.Fallos.Length;
            retosAcertados = estadisticas.Aciertos.Length;

            retosTotales = retosFallados + retosAcertados;
            probAcierto = (int)((retosAcertados / (float)retosTotales) * 100);

            aciertos.Text = probAcierto < 0 ? "0" : probAcierto.ToString() + "%";

            probFallo = (100 - probAcierto);

            fallos.Text = probAcierto < 0 ? "0" : probFallo.ToString() + "%";
        }

        public async void CambiarFoto(object sender, EventArgs e)
        {
            // Obtener una lista de IDs de recursos de imágenes predefinidas
            var profilePictureIds = new List<int>
            {
                 Resource.Drawable.icon_hombre,
                 Resource.Drawable.icon_mujer,
                 // Agregar aquí más IDs de recursos de imágenes predefinidas
            };

            // Crear una lista de miniaturas de las imágenes predefinidas
            var profilePictures = profilePictureIds.Select(id => BitmapFactory.DecodeResource(Resources, id)).ToList();

            // Mostrar un cuadro de diálogo emergente con la lista de miniaturas de las imágenes predefinidas
            var pictureTitles = new List<string> { "Hombre", "Mujer" }; // Aquí puede agregar títulos para las imágenes
            var selectedPictureTitle = await UserDialogs.Instance.ActionSheetAsync("Seleccionar imagen", "Cancelar", null, null, pictureTitles.ToArray());
            var selectedPictureIndex = pictureTitles.IndexOf(selectedPictureTitle);

            if (selectedPictureIndex < 0) return;
            // Convertir la imagen seleccionada en un arreglo de bytes
            var photoData = ConvertBitmapToByteArray(profilePictures[selectedPictureIndex]);

            // Convertir el arreglo de bytes en un Bitmap
            // ReSharper disable once MethodHasAsyncOverload
            var selectedPhoto = BitmapFactory.DecodeByteArray(photoData, 0, photoData.Length);

            // Actualizar la imagen de perfil del usuario con la imagen seleccionada
            avatar.SetImageBitmap(selectedPhoto);

            // Actualizar la imagen de perfil del usuario con la imagen seleccionada
            await fachada.CambiarFoto(usuario.Uuid, photoData);
        }

        public async void CambiarNombre(object sender, EventArgs e)
        {
            var config = new PromptConfig
            {
                Title = "Introduce tu nuevo nombre",
                OkText = "Aceptar",
                CancelText = "Cancelar"
            };
            var result = await UserDialogs.Instance.PromptAsync(config);

            var usuarioCorrect = await fachada.ComprobarUsuario(result.Text);

            if (result.Ok && usuarioCorrect)
            {
                var newNombre = result.Text;
                nombre.Text = newNombre;
                await fachada.CambiarNombre(newNombre);
            }
            else
            {
                UserDialogs.Instance.Alert(new AlertConfig
                {
                    Message = "Ese nombre de usuario ya existe o no es válido, prueba con otro",
                    OkText = "Aceptar",
                });
            }
        }

        private void IniciarFoto()
        {
            var uf = usuario.Foto;

            if (uf == null) return;
            var foto = Convert.FromBase64String(uf);

            avatar.SetImageBitmap(BitmapFactory.DecodeByteArray(foto, 0, foto.Length));
        }

        private void IniciarTiempo()
        {
            var tiempoTotal = estadisticas.Tiempo;

            if (tiempoTotal != null)
            {
                var horas = (int)tiempoTotal;
                var minutos = (int)((tiempoTotal - horas) * 60);

                tiempo.Text = $"{horas:D2} h {minutos:D2} min";
            }
            else { tiempo.Text = "0 h 0 min"; }
        }

        private void IniciarRequisitos()
        {
            if (usuario.Nivel < 2)
            {
                int partidasRestantes = estadisticas.PartidasGanadas / 5;
                int res = (partidasRestantes + 1) * 5 - estadisticas.PartidasGanadas;

                requisitos.Text = "Te quedan " + res + " partidas para subir al siguiente nivel";
            }
            else { requisitos.Text = "Has llegado al nivel máximo"; }
        }

        private void Atras(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            var i = new Intent(this, typeof(MenuViewModel));
            StartActivity(i);
            Finish();
        }

        private void CoberturaODS_Click(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            var i = new Intent(this, typeof(CoberturaODSViewModel));
            StartActivity(i);
            Finish();
        }

        public byte[] ConvertBitmapToByteArray(Bitmap bitmap)
        {
            using var stream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);
            return stream.ToArray();
        }
    }
}