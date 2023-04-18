using Android.Animation;
using Android.App;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.Entities;
using System.Diagnostics;

namespace preguntaods
{
    [Activity(Label = "", Theme = "@style/HiddenTitleTheme")]
    public class VistaPartidaViewModel : AppCompatActivity
    {
        // Vars
        private Reto reto;

        private Partida partida;
        private bool consolidado;
        private Animator animation;
        private ProgressBar progressBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Inicio de la vista
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaPartida);
            consolidado = false;

            // Cargar partida
            var director = new PartidaDirector();
            var builder = new PartidaBuilder();
            director.ConstructPartida(builder);
            partida = builder.GetPartida();

            // Animar Circulo Loading
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            animation = ObjectAnimator.OfInt(progressBar, "ProgressBar", 100, 0);
            animation.SetDuration(3000); //volver a 5 segundos en caso de que de error al cargar la partida
            animation.Start();

            // Cuando termine el tiempo de carga
            animation.AnimationEnd += (sender, e) =>
            {
                // Iniciar el reto
                RetoSiguiente(0, 0, 0);
            };
        }

        public void UpdateView()
        {
            switch (reto.GetType())
            {
                case Reto.typePregunta:
                    {
                        SetContentView(Resource.Layout.vistaRetoPregunta);
                        break;
                    }
                case Reto.typeAhorcado:
                    {
                        //SetContentView(Resource.Layout.vistaRetoAhorcado);
                        break;
                    }
                case Reto.typeFrase:
                    {
                        //SetContentView(Resource.Layout.vistaRetoFrase);
                        break;
                    }
                case Reto.typeSopa:
                    {
                        //SetContentView(Resource.Layout.vistaRetoSopa);
                        break;
                    }
            }
        }

        public async void GuardarPreguntaAcertada()
        {
            await partida.GuardarPreguntaUsuario(partida.GetRetoActual());
        }

        public void RetoSiguiente(int fallos, int ptsTotales, int ptsConsolidados)
        {
            partida.NextReto(fallos, ptsTotales, ptsConsolidados);

            reto = partida.GetRetoActual();

            UpdateView();
            partida.UpdateUI();
            partida.SetActivity(this);
            partida.InitValues();
        }

        public void Abandonar()
        {
            partida.EventoAbandonarBoton(new object(), new System.EventArgs());
        }

        public void AbandonarFallido(int puntos)
        {
            _ = partida.EventoAbandonarAsync(new object(), new System.EventArgs(), false, puntos, UserInterfacePregunta.getPuntosConsolidados());
        }

        public void Consolidar(int puntosConsolidados)
        {
            consolidado = true;
            partida.EventoConsolidarBoton(new object(), new System.EventArgs(), puntosConsolidados);
        }

        public bool GetConsolidado()
        {
            return consolidado;
        }
    }
}