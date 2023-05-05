using Acr.UserDialogs;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using preguntaods.BusinessLogic.EstrategiaSonido;
using preguntaods.BusinessLogic.Partida.Retos;
using preguntaods.BusinessLogic.Services;
using preguntaods.Entities;
using preguntaods.Presentacion.ViewModels;
using System;
using System.Threading.Tasks;

namespace preguntaods.Presentacion.UI_impl
{
    public class UserInterfacePregunta : UserInterface
    {
        // Class Elements
        private Facade fachada;

        private Sonido sonido;
        private int fallos;
        private int pistasUsadas;
        private int puntuacionTotal;
        private static int _puntosConsolidados;
        private int puntuacion;
        private string correcta;
        private EstrategiaSonidoReloj reloj;
        private int? odsRelacion;
        private bool tienePista;

        // UI Elements
        private TextView enunciado;

        private Button botonPregunta1;
        private Button botonPregunta2;
        private Button botonPregunta3;
        private Button botonPregunta4;
        private ProgressBar barTime;
        private ImageView imagenOds;
        private ImageView imagenCorazon1;
        private ImageView imagenCorazon2;
        private ImageButton interroganteButton;
        private ImageButton pistaButton;

        // Interactive Elements
        private ObjectAnimator animation;

        public override void SetValues(int newFallos, int newPistasUsadas, int newPuntuacion, int newPtsConsolidados)
        {
            fallos = newFallos;
            puntuacionTotal = newPuntuacion;
            _puntosConsolidados = newPtsConsolidados;
            pistasUsadas = newPistasUsadas;

            tienePista = true;
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
            imagenOds = activity.FindViewById<ImageView>(Resource.Id.imagenOds);
            imagenCorazon1 = activity.FindViewById<ImageView>(Resource.Id.heart1);
            imagenCorazon2 = activity.FindViewById<ImageView>(Resource.Id.heart2);
            interroganteButton = activity.FindViewById<ImageButton>(Resource.Id.interroganteButton);
            pistaButton = activity.FindViewById<ImageButton>(Resource.Id.pistaButton);

            if (fallos == 1)
            {
                imagenCorazon1?.SetImageResource(Resource.Drawable.icon_emptyHeart);
            }

            // Initialization of Services
            fachada = new Facade();
            sonido = new Sonido();

            // Initialization of Vars
            reloj = new EstrategiaSonidoReloj();

            // Animation
            animation = ObjectAnimator.OfInt(barTime, "Progress", 100, 0);
            animation?.SetDuration(30000); //30 secs

            // Listeners
            if (botonPregunta1 != null) botonPregunta1.Click += ButtonClickAsync;
            if (botonPregunta2 != null) botonPregunta2.Click += ButtonClickAsync;
            if (botonPregunta3 != null) botonPregunta3.Click += ButtonClickAsync;
            if (botonPregunta4 != null) botonPregunta4.Click += ButtonClickAsync;

            if (interroganteButton != null) interroganteButton.Click += InterroganteClick;
            if (pistaButton != null) pistaButton.Click += PistaClick;

            if (animation == null) return;
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
                switch (fallos)
                {
                    case 1:
                        imagenCorazon1.SetImageResource(Resource.Drawable.icon_emptyHeart);
                        break;

                    case 2:
                        imagenCorazon2.SetImageResource(Resource.Drawable.icon_emptyHeart);
                        break;
                }

                sonido.SetEstrategia(reloj, activity);
                sonido.PararSonido();

                await MostrarAlerta(false, fallos == 2);

                FinReto();
                ((VistaPartidaViewModel)activity).RetoSiguiente(fallos, pistasUsadas, puntuacionTotal, _puntosConsolidados);
            };
            animation.AnimationPause += (sender, e) =>
            {
                sonido.SetEstrategia(reloj, activity);
                sonido.PararSonido();
            };
        }

        private void InterroganteClick(object sender, EventArgs e)
        {
            if (odsRelacion == null) { ((VistaPartidaViewModel)activity).AbrirApoyo(0); }
            else ((VistaPartidaViewModel)activity).AbrirApoyo((int)odsRelacion);
        }

        private void PistaClick(object sender, EventArgs e)
        {
            if (pistasUsadas < 2 && tienePista)
            {
                UserDialogs.Instance.Confirm(new ConfirmConfig {
                    Message = "¿Estás seguro de usar una pista? Te quedan un total de " + (2 - pistasUsadas) + " pistas por usar.",
                    OkText = "Si, estoy seguro",
                    CancelText = "No quiero",
                    OnAction = (confirmed) =>
                    {
                        if (confirmed)
                        {
                            OcultarBoton();
                            OcultarBoton();

                            pistasUsadas++;
                            tienePista = false;

                            puntuacion /= 2;
                        }
                    }
                });
            }
        }

        public void OcultarBoton()
        {
            while (true)
            {
                var random = new Random();
                var n = random.Next(1, 4);
                Button boton = n switch
                {
                    1 => botonPregunta1,
                    2 => botonPregunta2,
                    3 => botonPregunta3,
                    _ => botonPregunta4,
                };

                if (boton.Enabled == true && !boton.Text.Equals(correcta))
                {
                    boton.SetBackgroundResource(Resource.Drawable.style_preNotEnabled);
                    boton.Enabled = false;
                    break;
                }
            }
        }

        public override void SetDatosReto(Reto reto)
        {
            var pregunta = (reto as RetoPre)?.GetPregunta();

            if (pregunta != null)
            {
                enunciado.Text = pregunta.Enunciado;
                botonPregunta1.Text = pregunta.Respuesta1;
                botonPregunta2.Text = pregunta.Respuesta2;
                botonPregunta3.Text = pregunta.Respuesta3;
                botonPregunta4.Text = pregunta.Respuesta4;

                puntuacion = pregunta.Dificultad switch
                {
                    Pregunta.DifBaja => 100,
                    Pregunta.DifMedia => 200,
                    Pregunta.DifAlta => 300,
                    _ => puntuacion
                };

                correcta = pregunta.Correcta;

                if (pregunta.OdsRelacionada == null)
                {
                    if (activity.Resources != null)
                    {
                        var idDeImagen =
                            activity.Resources.GetIdentifier("icon_logo", "drawable", activity.PackageName);
                        imagenOds.SetImageResource(idDeImagen);
                    }
                }
                else
                {
                    odsRelacion = int.Parse(pregunta.OdsRelacionada);
                    var nombreDeImagen =
                        "icon_ods" + pregunta.OdsRelacionada; // construir el nombre del recurso dinámicamente
                    if (activity.Resources != null)
                    {
                        var idDeImagen =
                            activity.Resources.GetIdentifier(nombreDeImagen, "drawable",
                                activity.PackageName); // obtener el identificador de recurso correspondiente
                        imagenOds.SetImageResource(idDeImagen);
                    }
                }
            }

            animation.Start();
        }

        private async void ButtonClickAsync(object sender, EventArgs e)
        {
            animation.Pause();
            var boton = sender as Button;
            bool acierto;
            bool condicion;

            if (boton?.Text != null && boton.Text.Equals(correcta))
            {
                sonido.SetEstrategia(new EstrategiaSonidoAcierto(), activity);
                boton.SetBackgroundResource(Resource.Drawable.style_preAcierto);
                ((VistaPartidaViewModel)activity).GuardarPreguntaAcertada();
                puntuacionTotal += puntuacion;
                acierto = true; condicion = false;
            }
            else
            {
                sonido.SetEstrategia(new EstrategiaSonidoError(), activity);
                boton?.SetBackgroundResource(Resource.Drawable.style_preFallo);

                ((VistaPartidaViewModel)activity).GuardarPreguntaFallada();

                puntuacionTotal -= puntuacion * 2;
                if (puntuacionTotal < 0) puntuacionTotal = 0;

                fallos++;
                acierto = false; condicion = fallos == 2;
            }

            sonido.EjecutarSonido();
            await MostrarAlerta(acierto, condicion);

            FinReto();

            ((VistaPartidaViewModel)activity).RetoSiguiente(fallos, pistasUsadas, puntuacionTotal, _puntosConsolidados);
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
                mensaje = ((VistaPartidaViewModel)activity).GetConsolidado() ? $"Tienes {puntuacionTotal} puntos. ¿Deseas abandonar o seguir?" : $"Sumas {puntuacion} a tus {puntuacionTotal - puntuacion} puntos. ¿Deseas consolidarlos (solo una vez por partida), abandonar o seguir?";

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
                    ((VistaPartidaViewModel)activity).Abandonar();
                });
                if (!((VistaPartidaViewModel)activity).GetConsolidado())
                {
                    alertBuilder.SetNegativeButton("Consolidar", (sender, args) =>
                    {
                        _puntosConsolidados = puntuacionTotal;
                        animation.Pause();
                        ((VistaPartidaViewModel)activity).Consolidar(_puntosConsolidados);
                        tcs.TrySetResult(true);
                    });
                }
                alertBuilder.SetCancelable(false);
                var alertDialog = alertBuilder.Create();
                alertDialog?.Show();

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                new Handler().PostDelayed(() =>
                {
                    // Acciones a realizar cuando quedan 10 segundos o menos
                    if (alertDialog is { IsShowing: false }) return;
                    sonido.SetEstrategia(reloj, activity);
                    sonido.PararSonido();
                    alertDialog?.GetButton((int)DialogButtonType.Positive)?.PerformClick();
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
                    ((VistaPartidaViewModel)activity).Abandonar();
                });
                alertBuilder.SetCancelable(false);
                var alertDialog = alertBuilder.Create();
                alertDialog?.Show();

#pragma warning disable CS0618
                new Handler().PostDelayed(() =>
#pragma warning restore CS0618
                {
                    // Acciones a realizar cuando quedan 10 segundos o menos
                    if (alertDialog is { IsShowing: false }) return;
                    sonido.SetEstrategia(reloj, activity);
                    sonido.PararSonido();
                    alertDialog?.GetButton((int)DialogButtonType.Positive)?.PerformClick();
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