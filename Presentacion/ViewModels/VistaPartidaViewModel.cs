using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.AppCompat.App;
using preguntaods.BusinessLogic.Partida;
using preguntaods.BusinessLogic.Partida.Retos;
using preguntaods.Presentacion.UI_impl;
using System;
using System.Threading.Tasks;

namespace preguntaods.Presentacion.ViewModels
{
    [Activity(Label = "", Theme = "@style/HiddenTitleTheme")]
    public class VistaPartidaViewModel : AppCompatActivity
    {
        // Vars
        private IReto reto;

        private Partida partida;
        private bool consolidado;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            // Inicio de la vista
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaPartida);
            consolidado = false;

            var botonPulsado = int.Parse(Intent?.GetStringExtra("BOTON_PULSADO") ?? throw new InvalidOperationException());

            // Mostrar dialogo
            UserDialogs.Instance.ShowLoading("Iniciando...", MaskType.Clear);
            await Task.Delay(1);

            // Cargar partida
            var director = new PartidaDirector();
            var builder = new PartidaBuilder();
            await director.ConstructPartida(builder, botonPulsado);
            partida = builder.GetPartida();

            //Ocultar dialogo
            UserDialogs.Instance.HideLoading();

            RetoSiguiente(0, 0, 0, 0);
        }

        public void UpdateView()
        {
            switch (reto.GetType())
            {
                case IReto.TypePregunta:
                    {
                        SetContentView(Resource.Layout.vistaRetoPregunta);
                        break;
                    }
                case IReto.TypeAhorcado:
                    {
                        SetContentView(Resource.Layout.vistaRetoAhorcado);
                        break;
                    }
                case IReto.TypeFrase:
                    {
                        SetContentView(Resource.Layout.vistaRetoFrase);
                        break;
                    }
                case IReto.TypeSopa:
                    {
                        SetContentView(Resource.Layout.vistaRetoSopa);
                        break;
                    }
            }
        }

        public async void GuardarPreguntaAcertada()
        {
            await partida.GuardarPreguntaUsuario(partida.GetRetoActual());
        }

        public async void GuardarPreguntaFallada()
        {
            await partida.GuardarPreguntaFalladaUsuario(partida.GetRetoActual());
        }

        public void RetoSiguiente(int fallos, int pistasUsadas, int ptsTotales, int ptsConsolidados)
        {
            partida.NextReto(fallos, ptsTotales, ptsConsolidados, pistasUsadas);

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

        public void AbrirApoyo(int ods)
        {
            var alertBuilder = new Android.App.AlertDialog.Builder(this, Resource.Style.AlertDialogCustom);

            alertBuilder.SetMessage("¿Estás seguro? El tiempo no parará.");
            alertBuilder.SetTitle("Enlace de apoyo");
            alertBuilder.SetPositiveButton("Aceptar", (o, args) =>
            {
                string path;
                if (ods == 0)
                {
                    path = "https://www.fao.org/sustainable-development-goals/overview/es/";
                }
                else path = "https://www.fao.org/sustainable-development-goals/goals/goal-" + ods + "/es/";
                var uri = Android.Net.Uri.Parse(path);
                var intent = new Intent(Intent.ActionView, uri);
                intent.SetFlags(ActivityFlags.NewTask);
                Application.Context.StartActivity(intent);
            });
            alertBuilder.SetNegativeButton("Cancelar", (o, args) =>
            {
            });
            alertBuilder.SetCancelable(false);

            var alertDialog = alertBuilder.Create();
            if (alertDialog == null) return;
            alertDialog.Window?.SetDimAmount(0.8f);
            alertDialog.Show();
        }
    }
}