using Android.Animation;
using Android.App;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.BusinessLogic.Partida;
using preguntaods.BusinessLogic.Partida.Retos;
using preguntaods.BusinessLogic.Partida.UI_impl;
using System;

namespace preguntaods.ViewModels
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

            var botonPulsado = int.Parse(Intent?.GetStringExtra("BOTON_PULSADO") ?? throw new InvalidOperationException());

            // Cargar partida
            var director = new PartidaDirector();
            var builder = new PartidaBuilder();
            director.ConstructPartida(builder, botonPulsado);
            partida = builder.GetPartida();

            // Animar Circulo Loading
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            animation = ObjectAnimator.OfInt(progressBar, "ProgressBar", 100, 0);
            if (animation == null) return;
            animation.SetDuration(4500); //volver a 5 segundos en caso de que de error al cargar la partida
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
                case Reto.TypePregunta:
                    {
                        SetContentView(Resource.Layout.vistaRetoPregunta);
                        break;
                    }
                case Reto.TypeAhorcado:
                    {
                        SetContentView(Resource.Layout.vistaRetoAhorcado);
                        break;
                    }
                case Reto.TypeFrase:
                    {
                        //SetContentView(Resource.Layout.vistaRetoFrase);
                        break;
                    }
                case Reto.TypeSopa:
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
            partida.UpdateUi();
            partida.SetActivity(this);
            partida.InitValues();
        }

        public void Abandonar()
        {
            partida.EventoAbandonarBoton(new object(), EventArgs.Empty);
        }

        public void AbandonarFallido(int puntos)
        {
            _ = partida.EventoAbandonarAsync(new object(), EventArgs.Empty, false, puntos, UserInterfacePregunta.GetPuntosConsolidados());
        }

        public void Consolidar(int puntosConsolidados)
        {
            consolidado = true;
            partida.EventoConsolidarBoton(new object(), EventArgs.Empty, puntosConsolidados);
        }

        public void ConsolidarUltimaPregunta()
        {
            consolidado = true;
        }

        public bool GetConsolidado()
        {
            return consolidado;
        }
    }
}