using System;
using System.Threading.Tasks;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using preguntaods.BusinessLogic.EstrategiaSonido;
using preguntaods.BusinessLogic.Partida.Retos;
using preguntaods.BusinessLogic.Services;
using preguntaods.Entities;
using preguntaods.ViewModels;

namespace preguntaods.BusinessLogic.Partida.UI_impl
{
    public class UserInterfacePregunta : UserInterface
    {
        // Class Elements
        private Activity activity;

        private Facade fachada;
        private Sonido sonido;
        private int fallos;
        private int puntuacionTotal;
        private static int _puntosConsolidados;
        private int puntuacion;
        private string correcta;
        private EstrategiaSonidoReloj reloj;
        private int numRetos = 10;

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

        public override void SetActivity(Activity newActivity)
        {
            this.activity = newActivity;
        }

        public override void SetValues(int newFallos, int newPuntuacion, int newPtsConsolidados)
        {
            this.fallos = newFallos;
            puntuacionTotal = newPuntuacion;
            _puntosConsolidados = newPtsConsolidados;
        }

        public override void Init()
        {
            // Initialization of UI Elements
            enunciado = activity.FindViewById<TextView>(Resource.Id.pregunta);
            botonPregunta1 = activity.FindViewById<Button>(Resource.Id.button1);
            botonPregunta2 = activity.FindViewById<Button>(Resource.Id.button2);
            botonPregunta3 = activity.FindViewById<Button>(Resource.Id.button3);
            botonPregunta4 = activity.FindViewById<Button>(Resource.Id.button4);
            barTime = activity.FindViewById<ProgressBar>(Resource.Id.timeBar);
            textoPuntos = activity.FindViewById<TextView>(Resource.Id.textView1);
            imagenOds = activity.FindViewById<ImageView>(Resource.Id.imagenOds);
            imagenCorazon1 = activity.FindViewById<ImageView>(Resource.Id.heart1);
            imagenCorazon2 = activity.FindViewById<ImageView>(Resource.Id.heart2);

            if (fallos == 1)
            {
                imagenCorazon1.SetImageResource(Resource.Drawable.icon_emptyHeart);
            }

            // Initialization of Services
            fachada = new Facade();
            sonido = new Sonido();

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
                    sonido.SetEstrategia(reloj, activity);
                    sonido.EjecutarSonido();
                }
            };
            animation.AnimationEnd += async (sender, e) =>
            {
                fallos++;
                if (fallos == 1) imagenCorazon1.SetImageResource(Resource.Drawable.icon_emptyHeart);
                else if (fallos == 2) imagenCorazon2.SetImageResource(Resource.Drawable.icon_emptyHeart);

                sonido.SetEstrategia(reloj, activity);
                sonido.PararSonido();

                await MostrarAlerta(false, fallos == 2);

                FinReto();
                (activity as VistaPartidaViewModel).RetoSiguiente(fallos, puntuacionTotal, _puntosConsolidados);
            };
            animation.AnimationPause += (sender, e) =>
            {
                sonido.SetEstrategia(reloj, activity);
                sonido.PararSonido();
            };
        }

        public override void SetDatosReto(Reto reto)
        {
            var pregunta = (reto as RetoPre).GetPregunta();

            enunciado.Text = pregunta.Enunciado;
            botonPregunta1.Text = pregunta.Respuesta1;
            botonPregunta2.Text = pregunta.Respuesta2;
            botonPregunta3.Text = pregunta.Respuesta3;
            botonPregunta4.Text = pregunta.Respuesta4;

            switch (pregunta.Dificultad)
            {
                case Pregunta.DifBaja: puntuacion = 100; break;
                case Pregunta.DifMedia: puntuacion = 200; break;
                case Pregunta.DifAlta: puntuacion = 300; break;
            }

            textoPuntos.Text = "Puntuación de la pregunta: " + puntuacion;

            correcta = pregunta.Correcta;

            if (pregunta.OdsRelacionada == null)
            {
                var idDeImagen = activity.Resources.GetIdentifier("icon_logo", "drawable", activity.PackageName); 
                imagenOds.SetImageResource(idDeImagen);
            }
            else
            {
                var nombreDeImagen = "icon_ods" + pregunta.OdsRelacionada; // construir el nombre del recurso dinámicamente
                var idDeImagen = activity.Resources.GetIdentifier(nombreDeImagen, "drawable", activity.PackageName); // obtener el identificador de recurso correspondiente
                imagenOds.SetImageResource(idDeImagen);
            }
            animation.Start();
        }

        private async void ButtonClickAsync(object sender, EventArgs e)
        {
            numRetos--;
            animation.Pause();
            Button boton = sender as Button;
            Boolean acierto;
            Boolean condicion;

            if (boton.Text.Equals(correcta))
            {
                sonido.SetEstrategia(new EstrategiaSonidoAcierto(), activity);
                boton.SetBackgroundResource(Resource.Drawable.style_preAcierto);
                (activity as VistaPartidaViewModel).GuardarPreguntaAcertada();
                puntuacionTotal += puntuacion;
                acierto = true; condicion = numRetos == 1;
            }
            else
            {
                sonido.SetEstrategia(new EstrategiaSonidoError(), activity);
                boton.SetBackgroundResource(Resource.Drawable.style_preFallo);

                puntuacionTotal -= puntuacion * 2;
                if (puntuacionTotal < 0) puntuacionTotal = 0;

                fallos++;
                acierto = false; condicion = fallos == 2;
            }

            sonido.EjecutarSonido();
            await MostrarAlerta(acierto, condicion);

            FinReto();

            (activity as VistaPartidaViewModel).RetoSiguiente(fallos, puntuacionTotal, _puntosConsolidados);
        }

        public override void FinReto()
        {
            botonPregunta1.Click += null!;
            botonPregunta2.Click += null!;
            botonPregunta3.Click += null!;
            botonPregunta4.Click += null!;

            animation.Pause();

            //actualizar datos usuario
        }

        public static int GetPuntosConsolidados()
        {
            return _puntosConsolidados;
        }

        private async Task MostrarAlerta(bool acertado, bool fin)
        {
            var tcs = new TaskCompletionSource<bool>();
            var alertBuilder = new AlertDialog.Builder(activity, Resource.Style.AlertDialogCustom);
            string titulo;
            string mensaje;

            if (acertado && !fin)
            {
                titulo = "Felicitaciones";
                mensaje = (activity as VistaPartidaViewModel).GetConsolidado() ? $"Tienes {puntuacionTotal} puntos. ¿Deseas abandonar o seguir?" : $"Sumas {puntuacion} a tus {puntuacionTotal-puntuacion} puntos. ¿Deseas consolidarlos (solo una vez por partida), abandonar o seguir?";

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
                    (activity as VistaPartidaViewModel).Abandonar();
                });
                if (!(activity as VistaPartidaViewModel).GetConsolidado())
                {
                    alertBuilder.SetNegativeButton("Consolidar", (sender, args) =>
                    {
                        _puntosConsolidados = puntuacionTotal;
                        animation.Pause();
                        (activity as VistaPartidaViewModel).Consolidar(_puntosConsolidados);
                        tcs.TrySetResult(true);
                    });
                }
                alertBuilder.SetCancelable(false);
                var alertDialog = alertBuilder.Create();
                alertDialog.Show();

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                new Handler().PostDelayed(() =>
                {
                    // Acciones a realizar cuando quedan 10 segundos o menos
                    if (alertDialog.IsShowing)
                    {
                        sonido.SetEstrategia(reloj, activity);
                        sonido.PararSonido();
                        alertDialog.GetButton((int)DialogButtonType.Positive).PerformClick();
                    }
                }, 15000);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                await tcs.Task;
            }
            else if (!acertado && !fin)
            {
                // sumar los consolidados
                titulo = "Vuelve a intentarlo";
                mensaje = $"Tienes {puntuacionTotal} puntos.";
                alertBuilder.SetMessage(mensaje);
                alertBuilder.SetTitle(titulo);
                alertBuilder.SetPositiveButton("Seguir", (sender, args) =>
                {
                    tcs.TrySetResult(true);

                    // se genera nueva pregunta
                });
                alertBuilder.SetNeutralButton("Abandonar", (sender, args) =>
                {
                    (activity as VistaPartidaViewModel).Abandonar();
                });
                alertBuilder.SetCancelable(false);
                var alertDialog = alertBuilder.Create();
                alertDialog.Show();

#pragma warning disable CS0618
                new Handler().PostDelayed(() =>
#pragma warning restore CS0618
                {
                    // Acciones a realizar cuando quedan 10 segundos o menos
                    if (alertDialog.IsShowing)
                    {
                        sonido.SetEstrategia(reloj, activity);
                        sonido.PararSonido();
                        alertDialog.GetButton((int)DialogButtonType.Positive).PerformClick();
                    }
                }, 15000);
                await tcs.Task;
            }
            else
            {
               ((VistaPartidaViewModel)activity).AbandonarFallido(puntuacionTotal);
            }
        }
    }
}