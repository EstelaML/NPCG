using Android.Content;
using Org.Apache.Http.Conn;
using System.Collections.Generic;

namespace preguntaods.Entities
{
    public class Partida
    {
        public Usuario user;
        private List<Reto> listaRetos;
        private Reto retoActual;
        private int contadorRetoSiguiente;
        private UserInterface userInterface;

        public Partida() {}

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
            UpdateUI();

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
    }
}