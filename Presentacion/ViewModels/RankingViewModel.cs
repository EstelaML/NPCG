using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.BusinessLogic.EstrategiaSonido;
using preguntaods.BusinessLogic.Fachada;
using preguntaods.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace preguntaods.Presentacion.ViewModels
{
    [Activity(Label = "", Theme = "@style/HiddenTitleTheme")]
    public class RankingViewModel : AppCompatActivity
    {
        protected Sonido Sonido;
        private TableLayout tablaRanking;
        private Facade fachada;
        private TextView textAnimo;
        private TableLayout fueraRanking;
        private const int NumFilas = 10;
        private List<Estadistica> topRanking;
        private List<Estadistica> usuariosOrdenados;
        private Usuario usuarioLogged;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaRanking);
            fachada = new Facade();

            tablaRanking = FindViewById<TableLayout>(Resource.Id.tablaRanking);
            fueraRanking = FindViewById<TableLayout>(Resource.Id.fueraRanking);
            textAnimo = FindViewById<TextView>(Resource.Id.textAnimo);

            Sonido = new Sonido();
            Sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            var atras = FindViewById<ImageButton>(Resource.Id.buttonAtras);
            if (atras != null) { atras.Click += Atras; }

            var botonIzq = FindViewById<Button>(Resource.Id.botonIzq);
            if (botonIzq != null) { botonIzq.Click += BotonIzq; }

            var botonDer = FindViewById<Button>(Resource.Id.botonDer);
            if (botonDer != null) { botonDer.Click += BotonDer; }

            usuariosOrdenados = await fachada.GetAllUsersOrdered();
            topRanking = usuariosOrdenados.Take(NumFilas).ToList();
            usuarioLogged = await fachada.GetUsuarioLogged();

            CrearRanking();
            MensajeAnimo();
        }

        private void BotonIzq(object sender, EventArgs e)
        {
            Sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            Sonido.EjecutarSonido();

            var i = new Intent(this, typeof(RankDiarioViewModel));
            StartActivity(i);
            Finish();
        }

        private void BotonDer(object sender, EventArgs e)
        {
            Sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            Sonido.EjecutarSonido();

            var i = new Intent(this, typeof(RankSemanalViewModel));
            StartActivity(i);
            Finish();
        }

        private void CrearFilaLlena(TableLayout tabla, int indice)
        {
            var fila = new TableRow(this) { TextAlignment = TextAlignment.Center };

            if (indice == GetIndiceLogged())
            {
                var txtPosicion = new TextView(this) { TextAlignment = TextAlignment.Center, TextSize = 18, Typeface = Android.Graphics.Typeface.DefaultBold };
                txtPosicion.SetTextColor(ColorStateList.ValueOf(Android.Graphics.Color.Red));
                txtPosicion.Text = (indice + 1).ToString() + ".";
                fila.AddView(txtPosicion);

                var txtNombre = new TextView(this) { TextAlignment = TextAlignment.Center, TextSize = 18, Typeface = Android.Graphics.Typeface.DefaultBold };
                txtNombre.SetTextColor(ColorStateList.ValueOf(Android.Graphics.Color.Red));
                txtNombre.Text = usuariosOrdenados[indice].Nombre;
                fila.AddView(txtNombre);

                var txtPuntos = new TextView(this) { TextAlignment = TextAlignment.Center, TextSize = 18, Typeface = Android.Graphics.Typeface.DefaultBold };
                txtPuntos.SetTextColor(ColorStateList.ValueOf(Android.Graphics.Color.Red));
                txtPuntos.Text = usuariosOrdenados[indice].Puntuacion.ToString();
                fila.AddView(txtPuntos);
            }
            else
            {
                var txtPosicion = new TextView(this) { TextAlignment = TextAlignment.Center, TextSize = 16 };
                txtPosicion.SetTextColor(ColorStateList.ValueOf(Android.Graphics.Color.Black));
                txtPosicion.Text = (indice + 1).ToString() + ".";
                fila.AddView(txtPosicion);

                var txtNombre = new TextView(this) { TextAlignment = TextAlignment.Center, TextSize = 16 };
                txtNombre.SetTextColor(ColorStateList.ValueOf(Android.Graphics.Color.Black));
                txtNombre.Text = usuariosOrdenados[indice].Nombre;
                fila.AddView(txtNombre);

                var txtPuntos = new TextView(this) { TextAlignment = TextAlignment.Center, TextSize = 16 };
                txtPuntos.SetTextColor(ColorStateList.ValueOf(Android.Graphics.Color.Black));
                txtPuntos.Text = usuariosOrdenados[indice].Puntuacion.ToString();
                fila.AddView(txtPuntos);
            }

            tabla.AddView(fila);
        }

        private void CrearFilaVacia(int indice)
        {
            var fila = new TableRow(this) { TextAlignment = TextAlignment.Center };

            var txtPosicion = new TextView(this) { TextAlignment = TextAlignment.Center, TextSize = 16 };
            txtPosicion.SetTextColor(ColorStateList.ValueOf(Android.Graphics.Color.Black));
            txtPosicion.Text = (indice + 1).ToString() + ".";
            fila.AddView(txtPosicion);

            var txtNombre = new TextView(this) { TextAlignment = TextAlignment.Center, TextSize = 16 };
            txtNombre.SetTextColor(ColorStateList.ValueOf(Android.Graphics.Color.Black));
            txtNombre.Text = "---";
            fila.AddView(txtNombre);

            var txtPuntos = new TextView(this) { TextAlignment = TextAlignment.Center, TextSize = 16 };
            txtPuntos.SetTextColor(ColorStateList.ValueOf(Android.Graphics.Color.Black));
            txtPuntos.Text = "---";
            fila.AddView(txtPuntos);

            tablaRanking.AddView(fila);
        }

        private void CrearRanking()
        {
            for (var i = 0; i < NumFilas; i++)
            {
                if (i < topRanking.Count)
                {
                    CrearFilaLlena(tablaRanking, i);
                }
                else
                {
                    CrearFilaVacia(i);
                }
            }
        }

        private Boolean EstaEnElRanking()
        {
            return topRanking.Any(u => u.Nombre == usuarioLogged.Nombre);
        }

        private int GetIndiceLogged()
        {
            return usuariosOrdenados.FindIndex(u => u.Nombre == usuarioLogged.Nombre);
        }

        private void MensajeAnimo()
        {
            var pos = GetIndiceLogged() + 1;
            if (EstaEnElRanking())
            {
                textAnimo.Text = "Eres el Top " + pos + ". ¡ENHORABUENA!";
            }
            else
            {
                CrearFilaLlena(fueraRanking, GetIndiceLogged());
                textAnimo.Text = "Todavía puedes seguir jugando y sumar puntos para llegar a la cima.";
            }
        }

        private void Atras(object sender, EventArgs e)
        {
            Sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            Sonido.EjecutarSonido();

            var i = new Intent(this, typeof(MenuViewModel));
            StartActivity(i);
            Finish();
        }
    }
}