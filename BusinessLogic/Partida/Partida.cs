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
        private TextView textoPuntosConsolidados;

        private int contadorRetoSiguiente;
        private int fallos;
        private Sonido _sonido;
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
        }

        #region Setters/Getters

        public void SetActivity(Android.App.Activity activity)
        {
            _activity = activity;
            userInterface.SetActivity(activity);

            if (primeraVez)
            {
                _sonido.SetEstrategia(new EstrategiaSonidoMusica(), _activity);
                _sonido.EjecutarSonido();
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
            _fachada = fachada;
        }

        public Facade GetFacade()
        {
            return _fachada;
        }

        private void SetUI(UserInterface userInterface)
        {
            this.userInterface = userInterface;
        }

        public UserInterface GetUI()
        {
            return userInterface;
        }

        public void SetSonido(Sonido sonido)
        {
            _sonido = sonido;
        }

        public Sonido GetSonido()
        {
            return _sonido;
        }

        #endregion Setters/Getters

        public void InitValues()
        {
            userInterface.SetValues(fallos, ptsTotales, ptsConsolidados);
            userInterface.Init();
            userInterface.SetDatosReto(retoActual);

            textoPuntosTotales = _activity.FindViewById<TextView>(Resource.Id.textView2);
            textoPuntosTotales.Text = "Puntos totales: " + ptsTotales;

            textoPuntosConsolidados = _activity.FindViewById<TextView>(Resource.Id.textPtsConsolidados);
            textoPuntosConsolidados.Text = "Puntos consolidados: " + ptsConsolidados;

            botonAbandonar = _activity.FindViewById<Button>(Resource.Id.volver);
            botonAbandonar.Click += EventoAbandonarBoton;
        }

        public void NextReto(int fallos, int ptsTotales, int ptsConsolidados)
        {
            this.fallos = fallos;
            this.ptsTotales = ptsTotales;
            this.ptsConsolidados = ptsConsolidados;
      
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

        public void EventoAbandonarBoton(object sender, EventArgs e)
        {
            string titulo = "";
            string mensaje = "";
            if ((_activity as VistaPartidaViewModel).GetConsolidado())
            {
                titulo = "¿Estás seguro?";
                mensaje = "Si aceptas se te guardaarán los puntos consolidados: " + UserInterfacePregunta.getPuntosConsolidados();
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
                    // guardamos puntos consolidados
                    (_activity as VistaPartidaViewModel).Consolidar(UserInterfacePregunta.getPuntosConsolidados());
                    Android.App.AlertDialog.Builder dialogoMal = new Android.App.AlertDialog.Builder(_activity, Resource.Style.AlertDialogCustom);
                    dialogoMal.SetTitle("No está mal");
                    dialogoMal.SetMessage($"Te llevas {UserInterfacePregunta.getPuntosConsolidados()} puntos");
                    dialogoMal.SetPositiveButton("Salir", (sender, args) =>
                    {
                        Intent i = new Intent(_activity, typeof(MenuViewModel));
                        _activity.StartActivity(i);
                    });
                    dialogoMal.Create().Show();
                }
                else {
                    Intent i = new Intent(_activity, typeof(MenuViewModel));
                    _activity.StartActivity(i);
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
            _sonido.PararSonido();
            if (acertado)
            {
                titulo = "¡Enhorabuena!";
                mensaje = $"Has llegado hasta el final y se te suman {puntosFinales} puntos";

                _sonido.SetEstrategia(new EstrategiaSonidoVictoria(), _activity);

                await _fachada.UpdatePuntos(puntosFinales - puntosConsolidados);
            }
            else
            {
                titulo = "Has perdido";
                mensaje = "Siempre puedes volver a intentarlo...";

                _sonido.SetEstrategia(new EstrategiaSonidoDerrota(), _activity);
            }

            _sonido.EjecutarSonido();
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