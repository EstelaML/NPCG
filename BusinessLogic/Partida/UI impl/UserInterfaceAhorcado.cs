using Android.App;
using Android.Widget;
using Android.Animation;
using preguntaods.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using Android.Database.Sqlite;

namespace preguntaods.Entities
{
    public class UserInterfaceAhorcado : UserInterface
    {
        // Class Elements
        private Activity _activity;

        private Facade fachada;
        private Sonido sonido;
        private int _fallos;
        private int _puntuacionTotal;
        private static int _puntosConsolidados;
        private string palabraAdivinar;
        private char[] guionesPalabra;
        private ImageView imagen;
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


        public UserInterfaceAhorcado()
        { }

        public override void SetActivity(Activity activity)
        {
            _activity = activity;
        }

        public override void Init()
        {
            ahorcadoImg = _activity.FindViewById<ImageView>(Resource.Id.ahorcadoImg);
            enunciado = _activity.FindViewById<TextView>(Resource.Id.enunciado);
            palabra = _activity.FindViewById<TextView>(Resource.Id.palabra);
            barTime = _activity.FindViewById<ProgressBar>(Resource.Id.timeBar);
            imagen = _activity.FindViewById<ImageView>(Resource.Id.ahorcadoImg);
            letrasAcertadas = 0;
            ronda = 1;

            #region buttonletters FindByID
            buttonA = _activity.FindViewById<Button>(Resource.Id.buttonA);
            buttonB = _activity.FindViewById<Button>(Resource.Id.buttonB);
            buttonC = _activity.FindViewById<Button>(Resource.Id.buttonC);
            buttonD = _activity.FindViewById<Button>(Resource.Id.buttonD);
            buttonE = _activity.FindViewById<Button>(Resource.Id.buttonE);
            buttonF = _activity.FindViewById<Button>(Resource.Id.buttonF);
            buttonG = _activity.FindViewById<Button>(Resource.Id.buttonG);
            buttonH = _activity.FindViewById<Button>(Resource.Id.buttonH);
            buttonI = _activity.FindViewById<Button>(Resource.Id.buttonI);
            buttonJ = _activity.FindViewById<Button>(Resource.Id.buttonJ);
            buttonK = _activity.FindViewById<Button>(Resource.Id.buttonK);
            buttonL = _activity.FindViewById<Button>(Resource.Id.buttonL);
            buttonM = _activity.FindViewById<Button>(Resource.Id.buttonM);
            buttonN = _activity.FindViewById<Button>(Resource.Id.buttonN);
            buttonÑ = _activity.FindViewById<Button>(Resource.Id.buttonÑ);
            buttonO = _activity.FindViewById<Button>(Resource.Id.buttonO);
            buttonP = _activity.FindViewById<Button>(Resource.Id.buttonP);
            buttonQ = _activity.FindViewById<Button>(Resource.Id.buttonQ);
            buttonR = _activity.FindViewById<Button>(Resource.Id.buttonR);
            buttonS = _activity.FindViewById<Button>(Resource.Id.buttonS);
            buttonT = _activity.FindViewById<Button>(Resource.Id.buttonT);
            buttonU = _activity.FindViewById<Button>(Resource.Id.buttonU);
            buttonV = _activity.FindViewById<Button>(Resource.Id.buttonV);
            buttonW = _activity.FindViewById<Button>(Resource.Id.buttonW);
            buttonX = _activity.FindViewById<Button>(Resource.Id.buttonX);
            buttonY = _activity.FindViewById<Button>(Resource.Id.buttonY);
            buttonZ = _activity.FindViewById<Button>(Resource.Id.buttonZ);

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
            animation.SetDuration(30000*4); //30*4 = 2mins

        }

        private void Letter_Click(object sender, EventArgs e)
        {
            Button boton = sender as Button;
            char letra = char.Parse(boton.Text);

            if (palabraAdivinar.Contains(letra))
            {
                List<int> indexes = new List<int>();
                char[] aux = palabraAdivinar.ToCharArray();

                // compruebo a ver si está dos veces y añado el indice a una lista
                for (int i = 0; i < palabraAdivinar.Length; i++)
                {
                    if (aux[i] == letra)
                    {
                        indexes.Add(i);
                        letrasAcertadas++;
                    }
                }
                for (int i = 0; i < indexes.Count; i++) 
                {
                    guionesPalabra[indexes[i]*3] = aux[indexes[i]];
                }
                palabra.Text = new string(guionesPalabra);
                boton.Enabled = false;
                if (letrasAcertadas == palabraAdivinar.Length) 
                {
                    FinReto(); 
                    (_activity as VistaPartidaViewModel).RetoSiguiente(_fallos, _puntuacionTotal, _puntosConsolidados);
                }
            }
            else 
            {
                boton.Enabled = false;
                string path = "ahorcado_" + ++ronda;
                var idDeImagen = _activity.Resources.GetIdentifier(path, "drawable", _activity.PackageName); 
                imagen.SetImageResource(idDeImagen);    
            }
        }

        public override void SetDatosReto(Reto reto)
        {
            animation.Start();
            var pregunta = (reto as RetoAhorcado);
            Ahorcado a = pregunta.GetAhorcado();
            enunciado.Text = a.Enunciado;
            palabraAdivinar = a.Palabra;
            char[] chars = a.Palabra.ToCharArray();
            var guiones = string.Join(" ", Enumerable.Repeat("_ ", palabraAdivinar.Length));
            palabra.Text = guiones;
            guionesPalabra = guiones.ToCharArray();
        }

        public override void FinReto()
        {
            animation.Pause();
        }

        public override void SetValues(int fallos, int puntuacion, int ptsConsolidados)
        {
            _fallos = fallos;
            _puntuacionTotal = puntuacion;
            _puntosConsolidados = ptsConsolidados;
        }
    }
}