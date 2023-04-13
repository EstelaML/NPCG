using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using preguntaods.Services;
using System;
using System.Threading.Tasks;

namespace preguntaods.Entities
{
    public class UserInterfacePregunta : UserInterface
    {
        // Class Elements
        private Activity _activity;
        private Facade fachada;
        private int _fallos;
        private int _puntuacionTotal;
        private int _puntosConsolidados;
        private int puntuacion;
        private string correcta;
        private EstrategiaSonidoReloj reloj;
        private int numRetos = 10;
        private bool consolidado = false;

        // UI Elements
        private TextView enunciado;
        private Button botonPregunta1;
        private Button botonPregunta2;
        private Button botonPregunta3;
        private Button botonPregunta4;
        private ProgressBar barTime;
        private ImageView imagenOds;
        private TextView textoPuntos;
        private ImageView imagenCorazon1;
        private ImageView imagenCorazon2;
       
        // Interactive Elements
        private ObjectAnimator animation;

        public UserInterfacePregunta() { }

        public override void SetActivity(Activity activity)
        {
            _activity = activity;
        }

        public override void SetValues(int fallos, int puntuacion)
        {
            _fallos = fallos;
            _puntuacionTotal = puntuacion;
        }

        public override void Init()
        {
            // Initialization of UI Elements
            enunciado = _activity.FindViewById<TextView>(Resource.Id.pregunta);
            botonPregunta1 = _activity.FindViewById<Button>(Resource.Id.button1);
            botonPregunta2 = _activity.FindViewById<Button>(Resource.Id.button2);
            botonPregunta3 = _activity.FindViewById<Button>(Resource.Id.button3);
            botonPregunta4 = _activity.FindViewById<Button>(Resource.Id.button4);
            barTime = _activity.FindViewById<ProgressBar>(Resource.Id.timeBar);
            textoPuntos = _activity.FindViewById<TextView>(Resource.Id.textView1);
            imagenOds = _activity.FindViewById<ImageView>(Resource.Id.imagenOds);
            imagenCorazon1 = _activity.FindViewById<ImageView>(Resource.Id.heart1);
            imagenCorazon2 = _activity.FindViewById<ImageView>(Resource.Id.heart2);
            _puntosConsolidados = 0;

            if (_fallos == 1) {
                imagenCorazon1.SetImageResource(Resource.Drawable.icon_emptyHeart);
            }
                

            // Initialization of Services
            fachada = new Facade();

            // Initialization of Vars
            reloj = new EstrategiaSonidoReloj();

            // Animation
            animation = ObjectAnimator.OfInt(barTime, "Progress", 100, 0);
            animation.SetDuration(30000); //30 secs

            // Listeners
            botonPregunta1.Click += ButtonClickAsync;
            botonPregunta2.Click += ButtonClickAsync;
            botonPregunta3.Click += ButtonClickAsync;
            botonPregunta4.Click += ButtonClickAsync;

            animation.Update += (sender, e) =>
            {
                var playtime = animation.CurrentPlayTime;
                if (playtime >= 20000 && playtime < 20020)
                {
                    fachada.EjecutarSonido(_activity, reloj);
                }
            };
            animation.AnimationEnd += async (sender, e) =>
            {
                if (_fallos == 1)        imagenCorazon1.SetImageResource(Resource.Drawable.icon_emptyHeart);
                else if (_fallos == 2)   imagenCorazon2.SetImageResource(Resource.Drawable.icon_emptyHeart);
                
                fachada.PararSonido(reloj);
            };
            animation.AnimationCancel += (sender, e) => { fachada.PararSonido(reloj); };
        }

        public override void SetDatosReto(Reto reto)
        {
            var pregunta = (reto as RetoPre).GetPregunta();

            enunciado.Text = pregunta.Enunciado;
            botonPregunta1.Text = pregunta.Respuesta1;
            botonPregunta2.Text = pregunta.Respuesta2;
            botonPregunta3.Text = pregunta.Respuesta3;
            botonPregunta4.Text = pregunta.Respuesta4;

            switch(pregunta.Dificultad)
            {
                case Pregunta.difBaja: puntuacion = 100; break;
                case Pregunta.difMedia: puntuacion = 200; break;
                case Pregunta.difAlta: puntuacion = 300; break;
            }

            textoPuntos.Text = "Puntuación de la pregunta: " + puntuacion;

            correcta = pregunta.Correcta;

            var nombreDeImagen = "icon_ods" + pregunta.OdsRelacionada; // construir el nombre del recurso dinámicamente
            var idDeImagen = _activity.Resources.GetIdentifier(nombreDeImagen, "drawable", _activity.PackageName); // obtener el identificador de recurso correspondiente
            imagenOds.SetImageResource(idDeImagen);

            animation.Start();
        }

        private async void ButtonClickAsync(object sender, EventArgs e)
        {
            numRetos--;
            animation.Cancel();
            Button boton = sender as Button;
            
            if(boton.Text.Equals(correcta))
            {
                fachada.EjecutarSonido(_activity, new EstrategiaSonidoAcierto());
                boton.SetBackgroundResource(Resource.Drawable.style_preAcierto);
                (_activity as VistaPartidaViewModel).GuardarPreguntaAcertada();
                _puntuacionTotal += puntuacion;
                await MostrarAlerta(true, numRetos == 1);
            }
            else
            {
                fachada.EjecutarSonido(_activity, new EstrategiaSonidoError());
                boton.SetBackgroundResource(Resource.Drawable.style_preFallo);

                _puntuacionTotal -= puntuacion * 2;
                if(_puntuacionTotal < 0) _puntuacionTotal = 0;

                _fallos++;
                await MostrarAlerta(false, _fallos == 2);
            }


            FinReto();
            
            (_activity as VistaPartidaViewModel).RetoSiguiente(_fallos, _puntuacionTotal);
        }

        public override void FinReto()
        {
            botonPregunta1.Click += null;
            botonPregunta2.Click += null;
            botonPregunta3.Click += null;
            botonPregunta4.Click += null;   

            animation.Cancel();
           

            //actualizar datos usuario
        }

        public async Task GuardarPuntosUsuarioAsync() {
            await fachada.UpdatePuntos(_puntuacionTotal);
        }

        private async Task<bool> MostrarAlerta(bool acertado, bool fin)
        {
            var tcs = new TaskCompletionSource<bool>();
            Android.App.AlertDialog.Builder alertBuilder = new Android.App.AlertDialog.Builder(_activity, Resource.Style.AlertDialogCustom);
            string titulo = "";
            string mensaje = "";
            bool result = false;


            if (acertado && !fin)
            {
                titulo = "Has acertado";
                if (consolidado)
                {
                    mensaje = $"Tienes {_puntuacionTotal} puntos. ¿Deseas botonAbandonar o seguir?";
                }
                else
                {
                    mensaje = $"Tienes {_puntuacionTotal} puntos. ¿Deseas consolidarlos (solo una vez por partida), abandonar o seguir?";
                }

                alertBuilder.SetMessage(mensaje);
                alertBuilder.SetTitle(titulo);
                alertBuilder.SetPositiveButton("Seguir", (sender, args) =>
                {
                    tcs.TrySetResult(true);

                    // sigue generando pregunta
                });
                alertBuilder.SetNeutralButton("Abandonar", (sender, args) =>
                {
                    // vuelves a menu principal
                    (_activity as VistaPartidaViewModel).Abandonar();
                });
                if (!consolidado)
                {
                    alertBuilder.SetNegativeButton("Consolidar", (sender, args) =>
                    {
                        consolidado = true;
                        _puntosConsolidados = _puntuacionTotal;
                        animation.Cancel();
                    });
                }
                alertBuilder.SetCancelable(false);
                Android.App.AlertDialog alertDialog = alertBuilder.Create();
                alertDialog.Show();

                new Handler().PostDelayed(() =>
                {
                    // Acciones a realizar cuando quedan 10 segundos o menos
                    if (alertDialog.IsShowing)
                    {
                        fachada.PararSonido(new EstrategiaSonidoReloj());
                        alertDialog.GetButton((int)DialogButtonType.Positive).PerformClick();
                    }
                }, 10000);
                result = await tcs.Task;
            }
            else if (!acertado && !fin)
            {
                // sumar los consolidados
                titulo = "Vuelve a intentarlo";
                mensaje = $"Tienes {_puntuacionTotal} puntos.";
                alertBuilder.SetMessage(mensaje);
                alertBuilder.SetTitle(titulo);
                alertBuilder.SetPositiveButton("Seguir", (sender, args) =>
                {
                    tcs.TrySetResult(true);

                    // se genera nueva pregunta
                });
                alertBuilder.SetNeutralButton("Abandonar", (sender, args) =>
                {
                    (_activity as VistaPartidaViewModel).Abandonar();
                });
                alertBuilder.SetCancelable(false);
                Android.App.AlertDialog alertDialog = alertBuilder.Create();
                alertDialog.Show();

                new Handler().PostDelayed(() =>
                {
                    // Acciones a realizar cuando quedan 10 segundos o menos
                    if (alertDialog.IsShowing)
                    {
                        fachada.PararSonido(new EstrategiaSonidoReloj());
                        alertDialog.GetButton((int)DialogButtonType.Negative).PerformClick();
                    }
                }, 10000);
                result = await tcs.Task;
                return result;
            }
            else {
                if (!consolidado) { (_activity as VistaPartidaViewModel).AbandonarFallido(_puntuacionTotal); }
            }
            return result;
        }
    }
}