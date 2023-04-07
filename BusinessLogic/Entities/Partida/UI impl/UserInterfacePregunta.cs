using Android.Animation;
using Android.App;
using Android.Content.Res;
using Android.Widget;
using Postgrest.Models;
using preguntaods.Persistencia.Repository;
using preguntaods.Services;
using System;

namespace preguntaods.Entities
{
    public class UserInterfacePregunta : UserInterface
    {
        // Class Elements
        private Activity _activity;
        private Facade fachada;
        private int fallos;
        private string correcta;

        // UI Elements
        private TextView enunciado;
        private Button botonPregunta1;
        private Button botonPregunta2;
        private Button botonPregunta3;
        private Button botonPregunta4;
        private ProgressBar barTime;
        private ImageView imagenOds;
        private TextView textoPuntos;
        private TextView textoPuntosTotales;
        private ImageView imagenCorazon1;
        private ImageView imagenCorazon2;

        // Interactive Elements
        private ObjectAnimator animation;

        public UserInterfacePregunta() { }

        public override void SetActivity(Activity activity)
        {
            _activity = activity;
        }

        public override void Init()
        {
            // Initialization of UI Elements
            enunciado           = _activity.FindViewById<TextView>(Resource.Id.pregunta);
            botonPregunta1      = _activity.FindViewById<Button>(Resource.Id.button1);
            botonPregunta2      = _activity.FindViewById<Button>(Resource.Id.button2);
            botonPregunta3      = _activity.FindViewById<Button>(Resource.Id.button3);
            botonPregunta4      = _activity.FindViewById<Button>(Resource.Id.button4);
            barTime             = _activity.FindViewById<ProgressBar>(Resource.Id.timeBar);
            textoPuntos         = _activity.FindViewById<TextView>(Resource.Id.textView1);
            textoPuntosTotales  = _activity.FindViewById<TextView>(Resource.Id.textView2);
            imagenOds           = _activity.FindViewById<ImageView>(Resource.Id.imagenOds);
            imagenCorazon1      = _activity.FindViewById<ImageView>(Resource.Id.heart1);
            imagenCorazon2      = _activity.FindViewById<ImageView>(Resource.Id.heart2);

            // Initialization of Services
            fachada = new Facade();

            // Initialization of Vars
            fallos = 0;

            // Animation
            animation = ObjectAnimator.OfInt(barTime, "Progress", 100, 0);
            animation.SetDuration(30000); //30 secs

            // Listeners
            botonPregunta1.Click += ButtonClick;
            botonPregunta2.Click += ButtonClick;
            botonPregunta3.Click += ButtonClick;
            botonPregunta4.Click += ButtonClick;

            animation.Update += (sender, e) =>
            {
                var playtime = animation.CurrentPlayTime;
                if (playtime >= 20000 && playtime < 20020)
                {
                    fachada.EjecutarSonido(_activity, new EstrategiaSonidoReloj());
                }
            };
            animation.AnimationEnd += (sender, e) =>
            {
                if (fallos == 1)        imagenCorazon1.SetImageResource(Resource.Drawable.icon_emptyHeart);
                else if (fallos == 2)   imagenCorazon2.SetImageResource(Resource.Drawable.icon_emptyHeart);

                fachada.PararSonido(new EstrategiaSonidoReloj());
            };
            animation.AnimationCancel += (sender, e) => { fachada.PararSonido(new EstrategiaSonidoReloj()); };
        }

        public override void SetDatosReto(Reto reto)
        {
            var pregunta = (reto as RetoPre).GetPregunta();

            enunciado.Text = pregunta.Enunciado;
            botonPregunta1.Text = pregunta.Respuesta1;
            botonPregunta2.Text = pregunta.Respuesta2;
            botonPregunta3.Text = pregunta.Respuesta3;
            botonPregunta4.Text = pregunta.Respuesta4;

            correcta = pregunta.Correcta;

            var nombreDeImagen = "icon_ods" + pregunta.OdsRelacionada; // construir el nombre del recurso dinámicamente
            var idDeImagen = _activity.Resources.GetIdentifier(nombreDeImagen, "drawable", _activity.PackageName); // obtener el identificador de recurso correspondiente
            imagenOds.SetImageResource(idDeImagen);

            animation.Start();
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Button boton = sender as Button;
            
            if(boton.Text.Equals(correcta))
            {
                fachada.EjecutarSonido(_activity, new EstrategiaSonidoAcierto());
                boton.SetBackgroundResource(Resource.Drawable.style_preAcierto);
            }
            else
            {
                fachada.EjecutarSonido(_activity, new EstrategiaSonidoError());
                boton.SetBackgroundResource(Resource.Drawable.style_preFallo);
            }
        }

        public override void FinReto()
        {
            botonPregunta1.Click += null;
            botonPregunta2.Click += null;
            botonPregunta3.Click += null;
            botonPregunta4.Click += null;

            animation.Cancel();
            fachada.PararSonido(new EstrategiaSonidoReloj());
        }
    }
}