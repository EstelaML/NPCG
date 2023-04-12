using Android.App;
using Android.Content;
using Android.Hardware.Usb;
using Android.Widget;
using Org.Apache.Http.Conn;
using preguntaods.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private EstrategiaSonidoMusica musica;
        private int ptsTotales;

        public Partida()
        {
            contadorRetoSiguiente = 0;
            
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
            if (listaRetos == null) listaRetos = new List<Reto>();
            listaRetos.Add(reto);
        }

        public void NextReto(int fallos, int ptsTotales)
        {
            this.fallos = fallos;
            this.ptsTotales = ptsTotales;

            if (fallos < 2 && contadorRetoSiguiente != listaRetos.Count)
            {
                retoActual = listaRetos[contadorRetoSiguiente];
                contadorRetoSiguiente++;
            } 
            else
            {
                EventoAbandonarAsync(new object(), new EventArgs(), fallos < 2, ptsTotales);
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

        public void SetFacade(Facade fachada)
        {
            _fachada = fachada;
        }

        public Facade GetFacade()
        {
            return _fachada;
        }

        public void SetActivity(Android.App.Activity activity)
        {
            _activity = activity;
            userInterface.SetActivity(activity);
        }

        public void InitValues()
        {
            userInterface.SetValues(fallos, ptsTotales);
            userInterface.Init();
            userInterface.SetDatosReto(retoActual);

            textoPuntosTotales = _activity.FindViewById<TextView>(Resource.Id.textView2);
            textoPuntosTotales.Text = "Puntos totales: " + ptsTotales;

            if (musica == null)
            {
                musica = new EstrategiaSonidoMusica(); _fachada.EjecutarSonido(_activity, musica);
            }
            botonAbandonar = _activity.FindViewById<Button>(Resource.Id.volver);
            botonAbandonar.Click += EventoAbandonarBoton;
    }

    public void EventoAbandonarBoton(object sender, EventArgs e)
    {
        // preguntar si está seguro antes de abandonar
        Android.App.AlertDialog alertDialog = null;
        string titulo = "¿Estás seguro?";
        string mensaje = "Una vez aceptes perderás tu progreso por completo.";
        Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(_activity, Resource.Style.AlertDialogCustom);
        builder.SetMessage(mensaje);
        builder.SetTitle(titulo);
        builder.SetPositiveButton("Aceptar", (sender, args) =>
        {
            userInterface.FinReto();
            _fachada.PararSonido(musica);

            Intent i = new Intent(_activity, typeof(MenuViewModel));
            _activity.StartActivity(i);
        });
        builder.SetNegativeButton("Cancelar", (sender, args) =>
        {

        });
        builder.SetCancelable(false);
        alertDialog = builder.Create();
        alertDialog.Window.SetDimAmount(0.8f);
        alertDialog.Show();
    }

    public async Task EventoAbandonarAsync(object sender, EventArgs e, bool acertado, int puntos)
    {
        string titulo = "";
        string mensaje = "";
        if (acertado)
        {
            titulo = "¡Enhorabuena!";
            mensaje = "Has llegado hasta el final y se te suman los puntos a tu puntuación total.";
            await _fachada.UpdatePuntos(puntos);
        }
        else
        {
            titulo = "Has perdido";
            mensaje = "Siempre puedes volver a intentarlo...";
        }
        Android.App.AlertDialog alertDialog = null;

        Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(_activity, Resource.Style.AlertDialogCustom);
        builder.SetMessage(mensaje);
        builder.SetTitle(titulo);
        builder.SetPositiveButton("Salir", (sender, args) =>
        {
            userInterface.FinReto();
            _fachada.PararSonido(musica);
            Intent i = new Intent(_activity, typeof(MenuViewModel));
            _activity.StartActivity(i);
        });
        builder.SetCancelable(false);
        alertDialog = builder.Create();
        alertDialog.Window.SetDimAmount(0.8f);
        alertDialog.Show();
    }
}
}