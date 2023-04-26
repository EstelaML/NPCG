using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content;
using Android.Widget;
using preguntaods.BusinessLogic.EstrategiaSonido;
using preguntaods.BusinessLogic.Partida.Retos;
using preguntaods.BusinessLogic.Partida.UI_impl;
using preguntaods.BusinessLogic.Services;
using preguntaods.Entities;
using preguntaods.ViewModels;

namespace preguntaods.BusinessLogic.Partida
{
    public class Partida
    {
        public Usuario user;
        private List<Reto> listaRetos;
        private Reto retoActual;
        private UserInterface userInterface;
        public Facade fachada;

        private Android.App.Activity activity;
        private Button botonAbandonar;
        private TextView textoPuntosTotales;
        private TextView textoPuntosConsolidados;

        private int contadorRetoSiguiente;
        private int fallos;
        private Sonido sonido;
        private int ptsTotales;
        private int ptsConsolidados;
        private bool falloFacil;
        private int numRetos;
        private bool primeraVez;

        public Partida()
        {
            contadorRetoSiguiente = 0;
            listaRetos = new List<Reto>();
            falloFacil = false;
            primeraVez = true;
            ptsTotales = 0;
            ptsConsolidados = 0;
            fallos = 0;
        }

        #region Setters/Getters

        public void SetActivity(Android.App.Activity activity)
        {
            this.activity = activity;
            userInterface.SetActivity(activity);

            if (primeraVez)
            {
                sonido.SetEstrategia(new EstrategiaSonidoMusica(), this.activity);
                sonido.EjecutarSonido();
                primeraVez = false;
            }
        }

        public Reto GetRetoActual()
        {
            return retoActual;
        }

        public List<Reto> GetRetos()
        {
            return listaRetos;
        }

        public void AddReto(Reto reto)
        {
            listaRetos.Add(reto);
        }

        public void SetFacade(Facade fachada)
        {
            this.fachada = fachada;
        }

        public Facade GetFacade()
        {
            return fachada;
        }

        private void SetUi(UserInterface userInterface)
        {
            this.userInterface = userInterface;
        }

        public UserInterface GetUi()
        {
            return userInterface;
        }

        public void SetSonido(Sonido sonido)
        {
            this.sonido = sonido;
        }

        public Sonido GetSonido()
        {
            return sonido;
        }

        #endregion Setters/Getters

        public void InitValues()
        {
            userInterface.SetValues(fallos, ptsTotales, ptsConsolidados);
            userInterface.Init();
            userInterface.SetDatosReto(retoActual);

            textoPuntosTotales = activity.FindViewById<TextView>(Resource.Id.textView2);
            textoPuntosTotales.Text = "Puntos totales: " + ptsTotales;

            textoPuntosConsolidados = activity.FindViewById<TextView>(Resource.Id.textPtsConsolidados);
            textoPuntosConsolidados.Text = "Puntos consolidados: " + ptsConsolidados;

            botonAbandonar = activity.FindViewById<Button>(Resource.Id.volver);
            botonAbandonar.Click += EventoAbandonarBoton;
        }

        public void NextReto(int fallos, int ptsTotales, int ptsConsolidados)
        {
            this.fallos = fallos;
            this.ptsTotales = ptsTotales;
            this.ptsConsolidados = ptsConsolidados;

            if (contadorRetoSiguiente == 9) { 
                (activity as VistaPartidaViewModel).ConsolidarUltimaPregunta(); 
            }

            if (fallos < 2 && contadorRetoSiguiente != listaRetos.Count - 2)
            {
                if (fallos == 1 && contadorRetoSiguiente == 4)
                {
                    retoActual = listaRetos[10];
                    contadorRetoSiguiente++;
                    falloFacil = true;
                }
                else if (fallos == 1 && !falloFacil && contadorRetoSiguiente == 7)
                {
                    retoActual = listaRetos[11];
                    contadorRetoSiguiente++;
                }
                else
                {
                    retoActual = listaRetos[contadorRetoSiguiente];
                    contadorRetoSiguiente++;
                }
            }
            else
            {
                _ = EventoAbandonarAsync(new object(), new EventArgs(), fallos < 2, ptsTotales, UserInterfacePregunta.getPuntosConsolidados());
            }
        }

        public void UpdateUI()
        {
            switch (listaRetos[contadorRetoSiguiente].GetType())
            {
                case Reto.TypePregunta:
                    {
                        SetUi(new UserInterfacePregunta());
                        break;
                    }
                case Reto.TypeAhorcado:
                    {
                        SetUi(new UserInterfaceAhorcado());
                        break;
                    }
                case Reto.TypeFrase:
                    {
                        SetUi(new UserInterfaceFrase());
                        break;
                    }
                case Reto.TypeSopa:
                    {
                        SetUi(new UserInterfaceSopa());
                        break;
                    }
            }
        }

        public async Task GuardarPreguntaUsuario(Reto reto)
        {
            await fachada.GuardarPregunta(reto);
        }

        public void EventoAbandonarBoton(object sender, EventArgs e)
        {
            string titulo = "";
            string mensaje = "";
            if ((activity as VistaPartidaViewModel).GetConsolidado())
            {
                titulo = "¿Estás seguro?";
                mensaje = "Si aceptas se te guardarán los puntos consolidados: " + UserInterfacePregunta.getPuntosConsolidados();
            }
            else
            {
                titulo = "¿Estás seguro?";
                mensaje = "Una vez aceptes perderás tu progreso por completo.";
            }
            // preguntar si está seguro antes de abandonar

            Android.App.AlertDialog alertDialog = null;
            Android.App.AlertDialog.Builder alertBuilder = new Android.App.AlertDialog.Builder(activity, Resource.Style.AlertDialogCustom);

            alertBuilder.SetMessage(mensaje);
            alertBuilder.SetTitle(titulo);
            alertBuilder.SetPositiveButton("Aceptar", (sender, args) =>
            {
            userInterface.FinReto();
            sonido.PararSonido();

                if ((activity as VistaPartidaViewModel).GetConsolidado())
                {
                    // guardamos puntos consolidados
                    //(_activity as VistaPartidaViewModel).Consolidar(UserInterfacePregunta.getPuntosConsolidados());
                    Android.App.AlertDialog.Builder dialogoMal = new Android.App.AlertDialog.Builder(activity, Resource.Style.AlertDialogCustom);
                    dialogoMal.SetTitle("No está mal");
                    dialogoMal.SetMessage($"Te llevas {UserInterfacePregunta.getPuntosConsolidados()} puntos");
                    dialogoMal.SetPositiveButton("Salir", (sender, args) =>
                    {
                        Intent i = new Intent(activity, typeof(MenuViewModel));
                        activity.StartActivity(i);
                    });
                    dialogoMal.Create().Show();
                }
                else {
                    Intent i = new Intent(activity, typeof(MenuViewModel));
                    activity.StartActivity(i);
                }

            });
            alertBuilder.SetNegativeButton("Cancelar", (sender, args) =>
            {
            });
            alertBuilder.SetCancelable(false);

            alertDialog = alertBuilder.Create();
            alertDialog.Window.SetDimAmount(0.8f);
            alertDialog.Show();
        }

        public async Task EventoAbandonarAsync(object sender, EventArgs e, bool acertado, int puntosFinales, int puntosConsolidados)
        {
            string titulo = "";
            string mensaje = "";
            sonido.PararSonido();
            if (acertado)
            {
                titulo = "¡Enhorabuena!";
                mensaje = $"Has llegado hasta el final y se te suman {puntosFinales} puntos";

                sonido.SetEstrategia(new EstrategiaSonidoVictoria(), activity);

                await fachada.UpdatePuntos(puntosFinales - puntosConsolidados);
            }
            else
            {
                titulo = "Has perdido";
                mensaje = "Siempre puedes volver a intentarlo...";

                sonido.SetEstrategia(new EstrategiaSonidoDerrota(), activity);
            }

            sonido.EjecutarSonido();
            Android.App.AlertDialog alertDialog = null;
            Android.App.AlertDialog.Builder alertBuilder = new Android.App.AlertDialog.Builder(activity, Resource.Style.AlertDialogCustom);

            alertBuilder.SetMessage(mensaje);
            alertBuilder.SetTitle(titulo);
            alertBuilder.SetPositiveButton("Salir", (sender, args) =>
            {
                userInterface.FinReto();
                sonido.PararSonido();

                Intent i = new Intent(activity, typeof(MenuViewModel));
                activity.StartActivity(i);
            });
            alertBuilder.SetCancelable(false);

            alertDialog = alertBuilder.Create();
            alertDialog.Window.SetDimAmount(0.8f);
            alertDialog.Show();
        }

        public async void EventoConsolidarBoton(object sender, EventArgs e, int puntosConsolidados)
        {
            await fachada.UpdatePuntos(puntosConsolidados);
        }
    }
}