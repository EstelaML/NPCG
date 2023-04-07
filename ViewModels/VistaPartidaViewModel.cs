using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.Entities;
using preguntaods.Services;
using System.Diagnostics;
using static Android.Provider.CallLog;

namespace preguntaods
{
    [Activity(Label = "", Theme = "@style/HiddenTitleTheme")]
    public class VistaPartidaViewModel : AppCompatActivity
    {
        // Vars
        private Reto reto;
        private Partida partida;

        private Animator animation;
        private ProgressBar progressBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Inicio de la vista
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaPartida);

            // Cargar partida
            var director = new PartidaDirector();
            var builder = new PartidaBuilder();
            director.ConstructPartida(builder);
            partida = builder.GetPartida();

            // Animar Circulo Loading
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            animation = ObjectAnimator.OfInt(progressBar, "ProgressBar", 100, 0);
            animation.SetDuration(5000); //5 secs
            animation.Start();

            // Cuando termine el tiempo de carga
            animation.AnimationEnd += (sender, e) =>
            {
                // Poner la vista del tipo de reto concreto
                RetoSiguiente(0, 0);
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

        public void RetoSiguiente(int fallos, int ptsTotales)
        {
            partida.NextReto(fallos, ptsTotales);

            reto = partida.GetRetoActual();

            UpdateView();
            partida.UpdateUI();
            partida.SetActivity(this);
            partida.InitValues();
        }
    }
}