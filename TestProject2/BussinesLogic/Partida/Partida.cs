using preguntaods.BusinessLogic.Fachada;
using preguntaods.BusinessLogic.Retos;
using preguntaods.Entities;
using preguntaods.Presentacion.UI_impl;

namespace preguntaods.BusinessLogic.Partida
{
    public class Partida
    {
        public Usuario User;
        private readonly List<IReto> listaRetos;
        private IReto retoActual;
        private UserInterface userInterface;
        private Facade fachada;

        //private Android.App.Activity activity;

        private int contadorRetoSiguiente;
        private int fallos;
        private int pistasUsadas;
        private int ptsTotales;
        private int ptsConsolidados;
        private bool falloFacil;
        private bool primeraVez;
        private bool falloTrasConsolidado;
        private int puntosRestadosConsol;

        public Partida()
        {
            contadorRetoSiguiente = 0;
            listaRetos = new List<IReto>();
            falloFacil = false;
            primeraVez = true;
            falloTrasConsolidado = false;
            puntosRestadosConsol = 0;
        }

        #region Setters/Getters

        /*     public void SetActivity(Android.App.Activity newActivity)
             {
                 activity = newActivity;
                 userInterface.SetActivity(newActivity);

                 if (!primeraVez) return;
                 primeraVez = false;
             }
        */

        public IReto GetRetoActual()
        {
            return retoActual;
        }

        public List<IReto> GetRetos()
        {
            return listaRetos;
        }

        public void AddReto(IReto reto)
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

        /*    private void SetUi(UserInterface newUserInterface)
            {
                userInterface = newUserInterface;
            }

            public UserInterface GetUi()
            {
                return userInterface;
            } */

        #endregion Setters/Getters

        public void InitValues()
        {
            userInterface.InitializeUi(fallos, pistasUsadas, ptsTotales, ptsConsolidados, retoActual, contadorRetoSiguiente);
        }

        public void NextReto(int newFallos, int newPtsTotales, int newPtsConsolidados, int newPistasUsadas)
        {
            fallos = newFallos;
            ptsTotales = newPtsTotales;
            ptsConsolidados = newPtsConsolidados;
            pistasUsadas = newPistasUsadas;

            if (newFallos < 2 && contadorRetoSiguiente != listaRetos.Count - 2)
            {
                switch (newFallos)
                {
                    case 1 when contadorRetoSiguiente == 4:
                        retoActual = listaRetos[10];
                        contadorRetoSiguiente++;
                        falloFacil = true;
                        break;

                    case 1 when !falloFacil && contadorRetoSiguiente == 7:
                        retoActual = listaRetos[11];
                        contadorRetoSiguiente++;
                        break;

                    default:
                        retoActual = listaRetos[contadorRetoSiguiente];
                        contadorRetoSiguiente++;
                        break;
                }
            }
            else if (contadorRetoSiguiente == listaRetos.Count - 2)
            {
                contadorRetoSiguiente++;
            }
        }

        public void UpdateUi()
        {
            switch (listaRetos[contadorRetoSiguiente - 1].GetType())
            {
                case IReto.TypePregunta:
                    {
                        //SetUi(new UserInterfacePregunta());
                        break;
                    }
                case IReto.TypeAhorcado:
                    {
                        //SetUi(new UserInterfaceAhorcado());
                        break;
                    }
                case IReto.TypeFrase:
                    {
                        //SetUi(new UserInterfaceFrase());
                        break;
                    }
                case IReto.TypeSopa:
                    {
                        //SetUi(new UserInterfaceSopa());
                        break;
                    }
            }
        }

        public async void EventoConsolidarBoton(object sender, EventArgs e, int puntosConsolidados)
        {
            await fachada.UpdatePuntos(puntosConsolidados);
        }

        public bool SetFalloTrasConsolidado(int puntos)
        {
            if (ptsConsolidados == 0) { return false; }
            falloTrasConsolidado = true;
            puntosRestadosConsol = puntos;
            return true;
        }

        public int GetFalloTrasConsolidado()
        {
            return falloTrasConsolidado ? puntosRestadosConsol : 0;
        }

        public async Task SubirNivel(int partidasGanadas)
        {
            if ((partidasGanadas + 1) % 5 == 0 && partidasGanadas < 10)
            {
                await fachada.UpdateNivel(User.Nivel);
            }
        }
    }
}