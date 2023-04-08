using Android.App;
using Android.Content;
using Android.Widget;
using Org.Apache.Http.Conn;
using preguntaods.Services;
using System;
using System.Collections.Generic;

namespace preguntaods.Entities
{
    public class Partida
    {
        public Usuario user;
        private List<Reto> listaRetos;
        private Reto retoActual;
        private UserInterface userInterface;
        public Facade _fachada;

        private Activity _activity;
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
                EventoAbandonar(new object(), new EventArgs());
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

        public void SetFacade(Facade fachada)
        {
            _fachada = fachada;
        }

        public void SetActivity(Activity activity)
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

            // Primera vez solo
            if (musica == null) 
            {
                musica = new EstrategiaSonidoMusica(); _fachada.EjecutarSonido(_activity, musica);

                botonAbandonar = _activity.FindViewById<Button>(Resource.Id.volver);
                botonAbandonar.Click += EventoAbandonar;
            }
        }

        public void EventoAbandonar(object sender, EventArgs e)
        {
            userInterface.FinReto();

            _fachada.PararSonido(musica);
            // posible solución a implementar bug reloj

            Intent i = new Intent(_activity, typeof(MenuViewModel));
            _activity.StartActivity(i);
        }
    }
}