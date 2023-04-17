using Android.Content;
using Android.Widget;
using preguntaods.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace preguntaods.Entities
{
    public class Partida
    {
        public Usuario user;
        private List<Reto> listaRetos;
        private Reto retoActual;
        private UserInterface userInterface;
        public Facade _fachada;

        private Android.App.Activity _activity;
        private Button botonAbandonar;
        private TextView textoPuntosTotales;

        private int contadorRetoSiguiente;
        private int fallos;
        private Sonido _sonido;
        private int ptsTotales;
        private int ptsConsolidados;
        private bool falloFacil = false;
        private int numRetos;

        public Partida()
        {
            contadorRetoSiguiente = 0;
            listaRetos = new List<Reto>();
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

        public void NextReto(int fallos, int ptsTotales)
        {
            this.fallos = fallos;
            this.ptsTotales = ptsTotales;

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
                EventoAbandonarAsync(new object(), new EventArgs(), fallos < 2, ptsTotales, UserInterfacePregunta.getPuntosConsolidados());
            }
        }

        public UserInterface GetUI()
        {
            return userInterface;
        }

        private void SetUI(UserInterface userInterface)
        {
            this.userInterface = userInterface;
        }

        public void UpdateUI()
        {
            switch (listaRetos[0].GetType())
            {
                case Reto.typePregunta:
                    {
                        SetUI(new UserInterfacePregunta());
                        break;
                    }
                case Reto.typeAhorcado:
                    {
                        SetUI(new UserInterfaceAhorcado());
                        break;
                    }
                case Reto.typeFrase:
                    {
                        SetUI(new UserInterfaceFrase());
                        break;
                    }
                case Reto.typeSopa:
                    {
                        SetUI(new UserInterfaceSopa());
                        break;
                    }
            }
        }

        public async Task GuardarPreguntaUsuario(Reto reto)
        {
            await _fachada.GuardarPregunta(reto);
        }

        public void SetFacade(Facade fachada)
        {
            _fachada = fachada;
        }

        public Facade GetFacade()
        {
            return _fachada;
        }

        public void SetSonido(Sonido sonido)
        {
            _sonido = sonido;
        }

        public Sonido GetSonido()
        {
            return _sonido;
        }

        public void SetActivity(Android.App.Activity activity)
        {
            _activity = activity;
            userInterface.SetActivity(activity);

            _sonido.SetEstrategia(new EstrategiaSonidoMusica(), _activity);
            _sonido.EjecutarSonido();
        }

        public void InitValues()
        {
            userInterface.SetValues(fallos, ptsTotales);
            userInterface.Init();
            userInterface.SetDatosReto(retoActual);

            textoPuntosTotales = _activity.FindViewById<TextView>(Resource.Id.textView2);
            textoPuntosTotales.Text = "Puntos totales: " + ptsTotales;

            botonAbandonar = _activity.FindViewById<Button>(Resource.Id.volver);
            botonAbandonar.Click += EventoAbandonarBoton;
    }

        public void EventoAbandonarBoton(object sender, EventArgs e)
        {
            string titulo = "";
            string mensaje = "";
            if ((_activity as VistaPartidaViewModel).GetConsolidado())
            {
                titulo = "¿Estás seguro?";
                mensaje = "Si aceptar se te fuardarán los puntos consolidados, pero el resto no.";
            }
            else
            {
                titulo = "¿Estás seguro?";
                mensaje = "Una vez aceptes perderás tu progreso por completo.";
            }
            // preguntar si está seguro antes de abandonar

            Android.App.AlertDialog alertDialog = null;
            Android.App.AlertDialog.Builder alertBuilder = new Android.App.AlertDialog.Builder(_activity, Resource.Style.AlertDialogCustom);

            alertBuilder.SetMessage(mensaje);
            alertBuilder.SetTitle(titulo);
            alertBuilder.SetPositiveButton("Aceptar", (sender, args) =>
            {
                userInterface.FinReto();
                _sonido.PararSonido();

                if ((_activity as VistaPartidaViewModel).GetConsolidado())
                {
                    (_activity as VistaPartidaViewModel).Consolidar(UserInterfacePregunta.getPuntosConsolidados());
                }

                Intent i = new Intent(_activity, typeof(MenuViewModel));
                _activity.StartActivity(i);
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
            if (acertado)
            {
                titulo = "¡Enhorabuena!";
                mensaje = "Has llegado hasta el final y se te suman los puntos a tu puntuación total.";

                await _fachada.UpdatePuntos(puntosFinales - puntosConsolidados);
            }
            else
            {
                titulo = "Has perdido";
                mensaje = "Siempre puedes volver a intentarlo...";
            }
            Android.App.AlertDialog alertDialog = null;
            Android.App.AlertDialog.Builder alertBuilder = new Android.App.AlertDialog.Builder(_activity, Resource.Style.AlertDialogCustom);

            alertBuilder.SetMessage(mensaje);
            alertBuilder.SetTitle(titulo);
            alertBuilder.SetPositiveButton("Salir", (sender, args) =>
            {
                userInterface.FinReto();
                _sonido.PararSonido();
                Intent i = new Intent(_activity, typeof(MenuViewModel));
                _activity.StartActivity(i);
            });
            alertBuilder.SetCancelable(false);

            alertDialog = alertBuilder.Create();
            alertDialog.Window.SetDimAmount(0.8f);
            alertDialog.Show();
        }

        public async void EventoConsolidarBoton(object sender, EventArgs e, int puntosConsolidados)
        {
            await _fachada.UpdatePuntos(puntosConsolidados);
        }
    }
}