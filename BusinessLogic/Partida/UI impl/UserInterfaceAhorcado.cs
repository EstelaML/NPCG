using System;
using System.Collections.Generic;
using System.Linq;
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
    public class UserInterfaceAhorcado : UserInterface
    {
        // Class Elements
        private Activity activity;

        private Facade fachada;
        private Sonido sonido;
        private int fallos;
        private int puntuacionTotal;
        private int puntuacion;
        private static int _puntosConsolidados;
        private string palabraAdivinar;
        private char[] guionesPalabra;
        private int ronda;
        private int letrasAcertadas; 

        // UI
        private ImageView ahorcadoImg;
        private TextView enunciado;
        private TextView palabra;

        #region Button letters
        private Button buttonA;
        private Button buttonB;
        private Button buttonC;
        private Button buttonD;
        private Button buttonE;
        private Button buttonF;
        private Button buttonG;
        private Button buttonH;
        private Button buttonI;
        private Button buttonJ;
        private Button buttonK;
        private Button buttonL;
        private Button buttonM;
        private Button buttonN;
        private Button buttonÑ;
        private Button buttonO;
        private Button buttonP;
        private Button buttonQ;
        private Button buttonR;
        private Button buttonS;
        private Button buttonT;
        private Button buttonU;
        private Button buttonV;
        private Button buttonW;
        private Button buttonX;
        private Button buttonY;
        private Button buttonZ;
        #endregion
        
        // tiempo limite
        private ObjectAnimator animation;
        private ProgressBar barTime;


        public override void SetActivity(Activity newActivity)
        {
            this.activity = newActivity;
        }

        public override void Init()
        {
            ahorcadoImg = activity.FindViewById<ImageView>(Resource.Id.ahorcadoImg);
            enunciado = activity.FindViewById<TextView>(Resource.Id.enunciado);
            palabra = activity.FindViewById<TextView>(Resource.Id.palabra);
            barTime = activity.FindViewById<ProgressBar>(Resource.Id.timeBar);
            letrasAcertadas = 0;
            ronda = 1;

            #region buttonletters FindByID
            buttonA = activity.FindViewById<Button>(Resource.Id.buttonA);
            buttonB = activity.FindViewById<Button>(Resource.Id.buttonB);
            buttonC = activity.FindViewById<Button>(Resource.Id.buttonC);
            buttonD = activity.FindViewById<Button>(Resource.Id.buttonD);
            buttonE = activity.FindViewById<Button>(Resource.Id.buttonE);
            buttonF = activity.FindViewById<Button>(Resource.Id.buttonF);
            buttonG = activity.FindViewById<Button>(Resource.Id.buttonG);
            buttonH = activity.FindViewById<Button>(Resource.Id.buttonH);
            buttonI = activity.FindViewById<Button>(Resource.Id.buttonI);
            buttonJ = activity.FindViewById<Button>(Resource.Id.buttonJ);
            buttonK = activity.FindViewById<Button>(Resource.Id.buttonK);
            buttonL = activity.FindViewById<Button>(Resource.Id.buttonL);
            buttonM = activity.FindViewById<Button>(Resource.Id.buttonM);
            buttonN = activity.FindViewById<Button>(Resource.Id.buttonN);
            buttonÑ = activity.FindViewById<Button>(Resource.Id.buttonÑ);
            buttonO = activity.FindViewById<Button>(Resource.Id.buttonO);
            buttonP = activity.FindViewById<Button>(Resource.Id.buttonP);
            buttonQ = activity.FindViewById<Button>(Resource.Id.buttonQ);
            buttonR = activity.FindViewById<Button>(Resource.Id.buttonR);
            buttonS = activity.FindViewById<Button>(Resource.Id.buttonS);
            buttonT = activity.FindViewById<Button>(Resource.Id.buttonT);
            buttonU = activity.FindViewById<Button>(Resource.Id.buttonU);
            buttonV = activity.FindViewById<Button>(Resource.Id.buttonV);
            buttonW = activity.FindViewById<Button>(Resource.Id.buttonW);
            buttonX = activity.FindViewById<Button>(Resource.Id.buttonX);
            buttonY = activity.FindViewById<Button>(Resource.Id.buttonY);
            buttonZ = activity.FindViewById<Button>(Resource.Id.buttonZ);
            #endregion

            #region buttonLetters Handler
            buttonA.Click += Letter_Click;
            buttonB.Click += Letter_Click;
            buttonC.Click += Letter_Click;
            buttonD.Click += Letter_Click;
            buttonE.Click += Letter_Click;
            buttonF.Click += Letter_Click;
            buttonG.Click += Letter_Click;
            buttonH.Click += Letter_Click;
            buttonI.Click += Letter_Click;
            buttonJ.Click += Letter_Click;  
            buttonK.Click += Letter_Click;
            buttonL.Click += Letter_Click;
            buttonM.Click += Letter_Click;
            buttonN.Click += Letter_Click;
            buttonÑ.Click += Letter_Click;
            buttonO.Click += Letter_Click;
            buttonP.Click += Letter_Click;
            buttonQ.Click += Letter_Click;
            buttonR.Click += Letter_Click;
            buttonS.Click += Letter_Click;
            buttonT.Click += Letter_Click;
            buttonU.Click += Letter_Click;
            buttonV.Click += Letter_Click;
            buttonW.Click += Letter_Click;
            buttonX.Click += Letter_Click;
            buttonY.Click += Letter_Click;
            buttonZ.Click += Letter_Click;
            #endregion

            animation = animation = ObjectAnimator.OfInt(barTime, "Progress", 100, 0);
            animation.SetDuration(30000*4); //30*4 = 2min

        }

        private async void Letter_Click(object sender, EventArgs e)
        {
            var boton = sender as Button;
            var letra = char.Parse(boton?.Text ?? string.Empty);

            if (palabraAdivinar.Contains(letra))
            {
                List<int> indexes = new List<int>();
                char[] aux = palabraAdivinar.ToCharArray();
                
                //Compruebo a ver si está dos veces y añado el indice a una lista
                for (int i = 0; i < palabraAdivinar.Length; i++)
                {
                    if (aux[i] == letra)
                    {
                        indexes.Add(i);
                        letrasAcertadas++;
                    }
                }
                foreach (var t in indexes)
                {
                    guionesPalabra[t*2] = aux[t];
                }
                palabra.Text = new string(guionesPalabra);
                if (boton != null) boton.Enabled = false;

                if (guionesPalabra.Contains('_')) return;
                puntuacionTotal += puntuacion;
                await MostrarAlerta(true, false);
                FinReto();
                ((VistaPartidaViewModel)activity).RetoSiguiente(fallos, puntuacionTotal, _puntosConsolidados);
            }
            else 
            {
                if (boton != null) boton.Enabled = false;
                string path = "ahorcado_" + ++ronda;
                var idDeImagen = activity.Resources.GetIdentifier(path, "drawable", activity.PackageName);
                ahorcadoImg.SetImageResource(idDeImagen);

                if (ronda == 10)
                {
                    //_puntuacionTotal -= puntuacion * 2;
                    fallos++;
                    await MostrarAlerta(false, fallos == 2);
                    ((VistaPartidaViewModel)activity).RetoSiguiente(fallos, puntuacionTotal, _puntosConsolidados);
                }

            }
        }

        public override void SetDatosReto(Reto reto) 
        {
            // comienza cuentra atrás
            animation.Start();
           
            var pregunta = (reto as RetoAhorcado);
            Ahorcado a = pregunta?.GetAhorcado();

            switch (a?.Dificultad)
            {
                case Ahorcado.DifBaja: puntuacion = 100; ronda = 0; break;
                case Ahorcado.DifMedia: puntuacion = 200; ronda = 3;  break;
                case Ahorcado.DifAlta: puntuacion = 300; ronda = 5; break;
            }

            var path = "ahorcado_" + ronda;
            var idDeImagen = activity.Resources.GetIdentifier(path, "drawable", activity.PackageName);
            ahorcadoImg.SetImageResource(idDeImagen);

            enunciado.Text = a?.Enunciado;
            palabraAdivinar = a.Palabra;

            var guiones = palabraAdivinar.Replace(" ", "  ").Replace("A", "_ ").Replace("B", "_ ").Replace("C", "_ ")
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
            
            guionesPalabra = guiones.ToCharArray();
        }

        public override void FinReto()
        {
            letrasAcertadas = 0;

            /*buttonA.Enabled = true; 
            buttonB.Enabled = true;
            buttonC.Enabled= true;
            buttonD.Enabled = true;
            buttonE.Enabled = true;
            buttonF.Enabled = true;
            buttonG.Enabled = true;
            buttonH.Enabled = true;
            buttonI.Enabled = true;
            buttonJ.Enabled = true;
            buttonK.Enabled = true;
            buttonL.Enabled = true;
            buttonM.Enabled = true;
            buttonN.Enabled = true;
            buttonO.Enabled = true;
            buttonP.Enabled = true;
            buttonQ.Enabled = true;
            buttonR.Enabled = true;
            buttonS.Enabled = true;
            buttonT.Enabled = true;
            buttonU.Enabled = true;
            buttonV.Enabled = true;
            buttonW.Enabled = true;
            buttonX.Enabled = true;
            buttonY.Enabled = true;
            buttonZ.Enabled = true;*/


            animation.Pause();
           
        }

        public override void SetValues(int newFallos, int newPuntuacion, int newPtsConsolidados)
        {
            this.fallos = newFallos;
            puntuacionTotal = newPuntuacion;
            _puntosConsolidados = newPtsConsolidados;
            
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
                        if (alertDialog.IsShowing)
                        {
                            //sonido.SetEstrategia(reloj, _activity);
                            //sonido.PararSonido();
                            alertDialog.GetButton((int)DialogButtonType.Positive)?.PerformClick();
                        }
                    }, 15000);
                    await tcs.Task;
                    break;
                }
                case false when !fin:
                {
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
                        if (alertDialog.IsShowing)
                        {
                            //sonido.SetEstrategia(reloj, _activity);
                            //sonido.PararSonido();
                            alertDialog.GetButton((int)DialogButtonType.Positive).PerformClick();
                        }
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