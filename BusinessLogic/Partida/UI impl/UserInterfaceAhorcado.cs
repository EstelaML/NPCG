using Android.App;
using Android.Widget;
using Android.Animation;
using preguntaods.Services;

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

            #region buttonletters
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
            animation = animation = ObjectAnimator.OfInt(barTime, "Progress", 100, 0);
            animation.SetDuration(30000); //30*4 = 2mins secs

        }

        public override void SetDatosReto(Reto reto)
        {
            animation.Start();
            var pregunta = (reto as RetoAhorcado);
            Ahorcado a = pregunta.GetAhorcado();
            var enun = a.Enunciado;
            //enunciado.Text = enun.ToString();
            /*palabra.Text = a.Palabra.Replace('A', '_').Replace('B', '_').Replace('C', '_')
                                                 .Replace('D', '_').Replace('E', '_').Replace('F', '_')
                                                 .Replace('G', '_').Replace('H', '_').Replace('I', '_')
                                                 .Replace('J', '_').Replace('K', '_').Replace('L', '_')
                                                 .Replace('M', '_').Replace('N', '_').Replace('Ñ', '_')
                                                 .Replace('O', '_').Replace('P', '_').Replace('Q', '_')
                                                 .Replace('R', '_').Replace('S', '_').Replace('T', '_')
                                                 .Replace('U', '_').Replace('V', '_').Replace('W', '_')
                                                 .Replace('X', '_').Replace('Y', '_').Replace('Z', '_');*/

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