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
        private Facade _fachada;

        private Activity _activity;
        private Button botonAbandonar;

        private int contadorRetoSiguiente;

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

        public void NextReto()
        {
            retoActual = listaRetos[contadorRetoSiguiente];
            contadorRetoSiguiente++;
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
            switch (retoActual.GetType())
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
            userInterface.Init();
            userInterface.SetDatosReto(retoActual);

            _fachada.EjecutarSonido(_activity, new EstrategiaSonidoMusica());

            botonAbandonar = _activity.FindViewById<Button>(Resource.Id.volver);
            botonAbandonar.Click += EventoAbandonar;
        }

        public void EventoAbandonar(object sender, EventArgs e)
        {
            userInterface.FinReto();

            _fachada.PararSonido(new EstrategiaSonidoMusica());

            Intent i = new Intent(_activity, typeof(MenuViewModel));
            _activity.StartActivity(i);
        }
    }
}