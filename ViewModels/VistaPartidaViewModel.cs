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
        private PartidaDirector director;
        private Reto reto;

        private Animator animation;
        private ProgressBar progressBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Inicio de la vista
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaPartida);

            // Cargar partida
            director = new PartidaDirector();
            PartidaBuilder builder = new PartidaBuilder();
            director.ConstructPartida(builder);
            Partida partida = builder.GetPartida();

            reto = partida.GetRetoActual();

            // Animar Circulo Loading
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            animation = ObjectAnimator.OfInt(progressBar, "ProgressBar", 100, 0);
            animation.SetDuration(4000); //4 secs
            animation.Start();

            // Cuando termine el tiempo de carga
            animation.AnimationEnd += (sender, e) =>
            {
                // Poner la vista del tipo de reto concreto
                UpdateView();
                partida.UpdateUI();
                partida.SetActivity(this);
                partida.InitValues();
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
    }
}