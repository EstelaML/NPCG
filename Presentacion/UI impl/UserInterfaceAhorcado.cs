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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preguntaods.Presentacion.UI_impl
{
    public class UserInterfaceAhorcado : UserInterface
    {
        // Class Elements
        private Facade fachada;

        private EstrategiaSonidoReloj reloj;
        private Sonido sonido;
        private int fallos;
        private bool tienePista;
        private int pistasUsadas;
        private int puntuacionTotal;
        private int puntuacion;
        private static int _puntosConsolidados;
        private string palabraAdivinar;
        private char[] guionesPalabra;
        private int ronda;
        private int letrasAcertadas;
        private int? odsRelacion;

        // UI
        private ImageView ahorcadoImg;

        private TextView enunciado;
        private TextView palabra;
        private ImageView imagenCorazon1;
        private ImageView imagenCorazon2;

        private ImageButton interroganteButton;
        private ImageButton pistaButton;

        private Dictionary<char, Button> buttonValuePairs;

        // tiempo limite
        private ObjectAnimator animation;

        private ProgressBar barTime;

        public override void Init()
        {
            ahorcadoImg = activity.FindViewById<ImageView>(Resource.Id.ahorcadoImg);
            enunciado = activity.FindViewById<TextView>(Resource.Id.enunciado);
            palabra = activity.FindViewById<TextView>(Resource.Id.palabra);
            barTime = activity.FindViewById<ProgressBar>(Resource.Id.timeBar);
            imagenCorazon1 = activity.FindViewById<ImageView>(Resource.Id.heart1);
            imagenCorazon2 = activity.FindViewById<ImageView>(Resource.Id.heart2);
            interroganteButton = activity.FindViewById<ImageButton>(Resource.Id.interroganteButton);
            pistaButton = activity.FindViewById<ImageButton>(Resource.Id.pistaButton);

            if (fallos == 1)
            {
                imagenCorazon1?.SetImageResource(Resource.Drawable.icon_emptyHeart);
            }

            letrasAcertadas = 0;
            ronda = 1;

            // Initialization of Services
            sonido = new Sonido();

            // Initialization of Vars
            reloj = new EstrategiaSonidoReloj();

            #region buttonletters FindByID

            buttonValuePairs = new Dictionary<char, Button>
            {
                { 'A', activity.FindViewById<Button>(Resource.Id.buttonA) },
                { 'B', activity.FindViewById<Button>(Resource.Id.buttonB) },
                { 'C', activity.FindViewById<Button>(Resource.Id.buttonC) },
                { 'D', activity.FindViewById<Button>(Resource.Id.buttonD) },
                { 'E', activity.FindViewById<Button>(Resource.Id.buttonE) },
                { 'F', activity.FindViewById<Button>(Resource.Id.buttonF) },
                { 'G', activity.FindViewById<Button>(Resource.Id.buttonG) },
                { 'H', activity.FindViewById<Button>(Resource.Id.buttonH) },
                { 'I', activity.FindViewById<Button>(Resource.Id.buttonI) },
                { 'J', activity.FindViewById<Button>(Resource.Id.buttonJ) },
                { 'K', activity.FindViewById<Button>(Resource.Id.buttonK) },
                { 'L', activity.FindViewById<Button>(Resource.Id.buttonL) },
                { 'M', activity.FindViewById<Button>(Resource.Id.buttonM) },
                { 'N', activity.FindViewById<Button>(Resource.Id.buttonN) },
                { 'Ñ', activity.FindViewById<Button>(Resource.Id.buttonÑ) },
                { 'O', activity.FindViewById<Button>(Resource.Id.buttonO) },
                { 'P', activity.FindViewById<Button>(Resource.Id.buttonP) },
                { 'Q', activity.FindViewById<Button>(Resource.Id.buttonQ) },
                { 'R', activity.FindViewById<Button>(Resource.Id.buttonR) },
                { 'S', activity.FindViewById<Button>(Resource.Id.buttonS) },
                { 'T', activity.FindViewById<Button>(Resource.Id.buttonT) },
                { 'U', activity.FindViewById<Button>(Resource.Id.buttonU) },
                { 'V', activity.FindViewById<Button>(Resource.Id.buttonV) },
                { 'W', activity.FindViewById<Button>(Resource.Id.buttonW) },
                { 'X', activity.FindViewById<Button>(Resource.Id.buttonX) },
                { 'Y', activity.FindViewById<Button>(Resource.Id.buttonY) },
                { 'Z', activity.FindViewById<Button>(Resource.Id.buttonZ) }
            };

            #endregion buttonletters FindByID

            #region buttonLetters Handler

            buttonValuePairs['A'].Click += Letter_Click;
            buttonValuePairs['B'].Click += Letter_Click;
            buttonValuePairs['C'].Click += Letter_Click;
            buttonValuePairs['D'].Click += Letter_Click;
            buttonValuePairs['E'].Click += Letter_Click;
            buttonValuePairs['F'].Click += Letter_Click;
            buttonValuePairs['G'].Click += Letter_Click;
            buttonValuePairs['H'].Click += Letter_Click;
            buttonValuePairs['I'].Click += Letter_Click;
            buttonValuePairs['J'].Click += Letter_Click;
            buttonValuePairs['K'].Click += Letter_Click;
            buttonValuePairs['L'].Click += Letter_Click;
            buttonValuePairs['M'].Click += Letter_Click;
            buttonValuePairs['N'].Click += Letter_Click;
            buttonValuePairs['Ñ'].Click += Letter_Click;
            buttonValuePairs['O'].Click += Letter_Click;
            buttonValuePairs['P'].Click += Letter_Click;
            buttonValuePairs['Q'].Click += Letter_Click;
            buttonValuePairs['R'].Click += Letter_Click;
            buttonValuePairs['S'].Click += Letter_Click;
            buttonValuePairs['T'].Click += Letter_Click;
            buttonValuePairs['U'].Click += Letter_Click;
            buttonValuePairs['V'].Click += Letter_Click;
            buttonValuePairs['W'].Click += Letter_Click;
            buttonValuePairs['X'].Click += Letter_Click;
            buttonValuePairs['Y'].Click += Letter_Click;
            buttonValuePairs['Z'].Click += Letter_Click;

            #endregion buttonLetters Handler

            if (interroganteButton != null) interroganteButton.Click += InterroganteClick;
            if (pistaButton != null) pistaButton.Click += PistaClick;

            animation = animation = ObjectAnimator.OfInt(barTime, "Progress", 100, 0);
            animation.SetDuration(120000); //30*4 = 2min

            animation.AnimationPause += (sender, e) =>
            {
                sonido.SetEstrategia(reloj, activity);
                sonido.PararSonido();
            };
            if (animation == null) return;
            animation.Update += (sender, e) =>
            {
                var playtime = animation.CurrentPlayTime;
                if (playtime < 100000 || playtime >= 100020) return;
                sonido.SetEstrategia(reloj, activity);
                sonido.EjecutarSonido();
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
        }

        private void InterroganteClick(object sender, EventArgs e)
        {
            if (odsRelacion >= 1 && odsRelacion <= 17) { ((VistaPartidaViewModel)activity).AbrirApoyo((int)odsRelacion); }
            else ((VistaPartidaViewModel)activity).AbrirApoyo(0);
        }

        private void PistaClick(object sender, EventArgs e)
        {
            if (pistasUsadas < 2 && tienePista)
            {
                UserDialogs.Instance.Confirm(new ConfirmConfig
                {
                    Message = "¿Estás seguro de usar una pista? Te quedan un total de " + (2 - pistasUsadas) + " pistas por usar.",
                    OkText = "Si, estoy seguro",
                    CancelText = "No quiero",
                    OnAction = (confirmed) =>
                    {
                        if (confirmed)
                        {
                            SeleccionarLetra();

                            pistasUsadas++;
                            tienePista = false;

                            puntuacion /= 2;
                        }
                    }
                });
            }
        }

        public void SeleccionarLetra()
        {
            int pos = Array.IndexOf(guionesPalabra, '_');
            if (pos != -1)
            {
                char letter = palabraAdivinar.ElementAt(pos);

                MostrarLetras(letter);
            }
        }

        public void MostrarLetras(char letter)
        {
            var indexes = GetIndexesOfLetter(palabraAdivinar, letter);
            ActualizaProgresoPalabra(indexes);

            buttonValuePairs[letter].Enabled = false;
        }

        private async void Letter_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            char letter = char.Parse(button.Text);

            if (palabraAdivinar.Contains(letter))
            {
                sonido.SetEstrategia(new EstrategiaSonidoLetraAcierto(), activity);
                sonido.EjecutarSonido();

                MostrarLetras(letter);

                if (guionesPalabra.Contains('_')) return;

                puntuacionTotal += puntuacion;

                await MostrarAlerta(true, false);

                ((VistaPartidaViewModel)activity).GuardarPreguntaAcertada();
                FinReto();
                ((VistaPartidaViewModel)activity).RetoSiguiente(fallos, pistasUsadas, puntuacionTotal, _puntosConsolidados);
            }
            else
            {
                button.Enabled = false;

                ActualizaImagenAhorcado();

                if (ronda != 10) return;
                fallos++;
                await MostrarAlerta(false, fallos == 2);

                ((VistaPartidaViewModel)activity).RetoSiguiente(fallos, pistasUsadas, puntuacionTotal, _puntosConsolidados);
            }
        }

        private List<int> GetIndexesOfLetter(string word, char letter)
        {
            var indexes = new List<int>();
            var aux = word.ToCharArray();

            for (int i = 0; i < word.Length; i++)
            {
                if (aux[i] == letter)
                {
                    indexes.Add(i);
                }
            }
            return indexes;
        }

        private void ActualizaProgresoPalabra(List<int> indexes)
        {
            foreach (var t in indexes)
            {
                guionesPalabra[t * 2] = palabraAdivinar[t];
            }
            palabra.Text = new string(guionesPalabra);
        }

        private void ActualizaImagenAhorcado()
        {
            var path = "ahorcado_" + ++ronda;
            if (activity.Resources != null)
            {
                var idDeImagen = activity.Resources.GetIdentifier(path, "drawable", activity.PackageName);
                ahorcadoImg.SetImageResource(idDeImagen);
            }
        }

        public override void SetDatosReto(Reto reto)
        {
            // comienza cuentra atrás
            animation.Start();

            var pregunta = (reto as RetoAhorcado);
            var a = pregunta?.GetAhorcado();
            odsRelacion = a?.OdsRelacionada;

            switch (a?.Dificultad)
            {
                case Ahorcado.DifBaja: puntuacion = 100; ronda = 0; break;
                case Ahorcado.DifMedia: puntuacion = 200; ronda = 3; break;
                case Ahorcado.DifAlta: puntuacion = 300; ronda = 5; break;
            }

            var path = "ahorcado_" + ronda;
            if (activity.Resources != null)
            {
                var idDeImagen = activity.Resources.GetIdentifier(path, "drawable", activity.PackageName);
                ahorcadoImg.SetImageResource(idDeImagen);
            }

            enunciado.Text = a?.Enunciado;
            palabraAdivinar = a?.Palabra;

            var guiones = palabraAdivinar?.Replace(" ", "  ").Replace("A", "_ ").Replace("B", "_ ").Replace("C", "_ ")
                                                 .Replace("D", "_ ").Replace("E", "_ ").Replace("F", "_ ")
                                                 .Replace("A", "_ ").Replace("B", "_ ").Replace("C", "_ ")
                                                 .Replace("G", "_ ").Replace("H", "_ ").Replace("I", "_ ")
                                                 .Replace("J", "_ ").Replace("K", "_ ").Replace("L", "_ ")
                                                 .Replace("M", "_ ").Replace("N", "_ ").Replace("Ñ", "_ ")
                                                 .Replace("O", "_ ").Replace("P", "_ ").Replace("Q", "_ ")
                                                 .Replace("R", "_ ").Replace("S", "_ ").Replace("T", "_ ")
                                                 .Replace("U", "_ ").Replace("V", "_ ").Replace("W", "_ ")
                                                 .Replace("X", "_ ").Replace("Y", "_ ").Replace("Z", "_ ");
            palabra.Text = guiones;

            guionesPalabra = guiones?.ToCharArray();
        }

        public override void FinReto()
        {
            letrasAcertadas = 0;

            animation.Pause();
        }

        public override void SetValues(int newFallos, int newPistasUsadas, int newPuntuacion, int newPtsConsolidados)
        {
            fallos = newFallos;
            puntuacionTotal = newPuntuacion;
            _puntosConsolidados = newPtsConsolidados;
            pistasUsadas = newPistasUsadas;

            tienePista = true;
        }

        private async Task MostrarAlerta(bool acertado, bool fin)
        {
            var tcs = new TaskCompletionSource<bool>();
            var alertBuilder = new AlertDialog.Builder(activity, Resource.Style.AlertDialogCustom);
            string titulo;
            string mensaje;

            switch (acertado)
            {
                case true when !fin:
                    {
                        sonido.SetEstrategia(new EstrategiaSonidoAcierto(), activity);
                        sonido.EjecutarSonido();
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

#pragma warning disable CS0618
                        new Handler().PostDelayed(() =>
#pragma warning restore CS0618
                        {
                            // Acciones a realizar cuando quedan 10 segundos o menos
                            if (alertDialog is { IsShowing: true })
                            {
                                sonido.SetEstrategia(reloj, activity);
                                sonido.PararSonido();
                                alertDialog.GetButton((int)DialogButtonType.Positive)?.PerformClick();
                            }
                        }, 15000);
                        await tcs.Task;
                        break;
                    }
                case false when !fin:
                    {
                        sonido.SetEstrategia(new EstrategiaSonidoError(), activity);
                        sonido.EjecutarSonido();
                        // sumar los consolidados
                        titulo = "Vuelve a intentarlo";
                        mensaje = $"Tienes {puntuacionTotal} puntos.";
                        alertBuilder.SetMessage(mensaje);
                        alertBuilder.SetTitle(titulo);
                        alertBuilder.SetPositiveButton("Seguir", (sender, args) =>
                        {
                            tcs.TrySetResult(true);
                            FinReto();
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
                            if (!(alertDialog is { IsShowing: true })) return;
                            sonido.SetEstrategia(reloj, activity);
                            sonido.PararSonido();
                            alertDialog.GetButton((int)DialogButtonType.Positive)?.PerformClick();
                        }, 15000);
                        await tcs.Task;
                        break;
                    }
                default:
                    ((VistaPartidaViewModel)activity).AbandonarFallido(puntuacionTotal);
                    break;
            }
        }
    }
}