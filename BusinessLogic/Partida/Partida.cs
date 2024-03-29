﻿using Acr.UserDialogs;
using Android.Content;
using Android.Widget;
using preguntaods.BusinessLogic.Fachada;
using preguntaods.BusinessLogic.Retos;
using preguntaods.Entities;
using preguntaods.Presentacion.UI_impl;
using preguntaods.Presentacion.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using preguntaods.BusinessLogic.Sonidos;

namespace preguntaods.BusinessLogic.Partida
{
    public class Partida
    {
        public Usuario User;
        private readonly List<IReto> listaRetos;
        private IReto retoActual;
        private UserInterface userInterface;
        private Facade fachada;

        private Android.App.Activity activity;
        private Button botonAbandonar;

        private int contadorRetoSiguiente;
        private int fallos;
        private int pistasUsadas;
        private Sonido sonido;
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

        public void SetActivity(Android.App.Activity newActivity)
        {
            activity = newActivity;
            userInterface.SetActivity(newActivity);

            if (!primeraVez) return;
            sonido.SetEstrategia(new EstrategiaSonidoMusica(), activity);
            sonido.EjecutarSonido();
            primeraVez = false;
        }

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

        private void SetUi(UserInterface newUserInterface)
        {
            userInterface = newUserInterface;
        }

        public UserInterface GetUi()
        {
            return userInterface;
        }

        public void SetSonido(Sonido newSonido)
        {
            sonido = newSonido;
        }

        public Sonido GetSonido()
        {
            return sonido;
        }

        #endregion Setters/Getters

        public void InitValues()
        {
            userInterface.InitializeUi(fallos, pistasUsadas, ptsTotales, ptsConsolidados, retoActual, contadorRetoSiguiente);

            botonAbandonar = activity.FindViewById<Button>(Resource.Id.volver);
            if (botonAbandonar != null) botonAbandonar.Click += EventoAbandonarBoton;
        }

        public void NextReto(int newFallos, int newPtsTotales, int newPtsConsolidados, int newPistasUsadas)
        {
            fallos = newFallos;
            ptsTotales = newPtsTotales;
            ptsConsolidados = newPtsConsolidados;
            pistasUsadas = newPistasUsadas;

            if (contadorRetoSiguiente == 9)
            {
                ((VistaPartidaViewModel)activity).ConsolidarUltimaPregunta();
            }

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
                _ = EventoAbandonarAsync(new object(), EventArgs.Empty, newFallos < 2, newPtsTotales, UserInterfacePregunta.GetPuntosConsolidados());
            }
        }

        public void UpdateUi()
        {
            switch (listaRetos[contadorRetoSiguiente - 1].GetType())
            {
                case IReto.TypePregunta:
                    {
                        SetUi(new UserInterfacePregunta());
                        break;
                    }
                case IReto.TypeAhorcado:
                    {
                        SetUi(new UserInterfaceAhorcado());
                        break;
                    }
                case IReto.TypeFrase:
                    {
                        SetUi(new UserInterfaceFrase());
                        break;
                    }
                case IReto.TypeSopa:
                    {
                        SetUi(new UserInterfaceSopa());
                        break;
                    }
            }
        }

        public void EventoAbandonarBoton(object sender, EventArgs e)
        {
            string titulo;
            string mensaje;
            if (((VistaPartidaViewModel)activity).GetConsolidado())
            {
                var puntosP = UserInterfacePregunta.GetPuntosConsolidados();
                if (puntosP == 0) { puntosP = UserInterfaceAhorcado.GetPuntosConsolidados(); }
                titulo = "¿Estás seguro?";
                mensaje = "Si aceptas se te guardarán los puntos consolidados: " + (puntosP - puntosRestadosConsol * 2);
            }
            else
            {
                titulo = "¿Estás seguro?";
                mensaje = "Una vez aceptes perderás tu progreso por completo.";
            }
            // preguntar si está seguro antes de abandonar

            var alertBuilder = new Android.App.AlertDialog.Builder(activity, Resource.Style.AlertDialogCustom);

            alertBuilder.SetMessage(mensaje);
            alertBuilder.SetTitle(titulo);
            alertBuilder.SetPositiveButton("Aceptar", (o, args) =>
            {
                userInterface.FinReto();
                sonido.PararSonido();

                if (((VistaPartidaViewModel)activity).GetConsolidado())
                {
                    // guardamos puntos consolidados
                    var dialogoMal = new Android.App.AlertDialog.Builder(activity, Resource.Style.AlertDialogCustom);
                    dialogoMal.SetTitle("No está mal");
                    dialogoMal.SetMessage($"Te llevas {UserInterfacePregunta.GetPuntosConsolidados()} puntos");
                    dialogoMal.SetPositiveButton("Salir", (o1, eventArgs) =>
                    {
                        var i = new Intent(activity, typeof(MenuViewModel));
                        activity.StartActivity(i);
                        activity.Finish();
                    });
                    dialogoMal.Create()?.Show();
                }
                else
                {
                    var i = new Intent(activity, typeof(MenuViewModel));
                    activity.StartActivity(i);
                    activity.Finish();
                }
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

        public async Task EventoAbandonarAsync(object sender, EventArgs e, bool acertado, int puntosFinales, int puntosConsolidados)
        {
            string titulo;
            string mensaje;

            var stats = await fachada.PedirEstadisticas(User.Uuid);
            int partidasGanadas = stats.PartidasGanadas;

            sonido.PararSonido();
            if (acertado)
            {
                titulo = "¡Enhorabuena!";
                mensaje = $"Has llegado hasta el final y se te suman {puntosFinales} puntos";

                sonido.SetEstrategia(new EstrategiaSonidoVictoria(), activity);

                await fachada.UpdatePuntos(puntosFinales - puntosConsolidados - puntosRestadosConsol);

                await SubirNivel(partidasGanadas);

                await fachada.UpdatePartidasGanadas();
            }
            else
            {
                titulo = "Has perdido";
                mensaje = "Siempre puedes volver a intentarlo...";

                sonido.SetEstrategia(new EstrategiaSonidoDerrota(), activity);
            }

            sonido.EjecutarSonido();
            var alertBuilder = new Android.App.AlertDialog.Builder(activity, Resource.Style.AlertDialogCustom);

            alertBuilder.SetMessage(mensaje);
            alertBuilder.SetTitle(titulo);
            alertBuilder.SetPositiveButton("Salir", (o, args) =>
            {
                userInterface.FinReto();
                sonido.PararSonido();

                var i = new Intent(activity, typeof(MenuViewModel));
                activity.StartActivity(i);
                activity.Finish();
            });
            alertBuilder.SetCancelable(false);

            var alertDialog = alertBuilder.Create();
            alertDialog?.Window?.SetDimAmount(0.8f);
            alertDialog?.Show();
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

                await UserDialogs.Instance.AlertAsync(new AlertConfig
                {
                    Title = "¡¡Felicidades!!",
                    Message = "Has subido de nivel, has conseguido un nuevo modo de juego.",
                    OkText = "Entendido",
                });
            }
        }
    }
}