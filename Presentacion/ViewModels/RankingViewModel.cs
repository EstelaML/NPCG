using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.BusinessLogic.EstrategiaSonido;
using preguntaods.BusinessLogic.Services;
using preguntaods.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using static Android.Resource;

namespace preguntaods.Presentacion.ViewModels
{
    [Activity(Label = "", Theme = "@style/HiddenTitleTheme")]
    public class RankingViewModel : AppCompatActivity
    {
        private Sonido sonido;
        private TableLayout tablaRanking;
        private Facade fachada;
        private TextView textAnimo;
        private LinearLayout animoLinearLayout;
        private const int NumFilas = 10;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaRanking);
            fachada = new Facade();
            
            tablaRanking = FindViewById<TableLayout>(Resource.Id.tablaRanking);
            animoLinearLayout = FindViewById<LinearLayout>(Resource.Id.animoLinearLayout);
            textAnimo = FindViewById<TextView>(Resource.Id.textAnimo);

            sonido = new Sonido();
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            var atras = FindViewById<ImageButton>(Resource.Id.buttonAtras);
            if (atras != null) { atras.Click += Atras; }

            var usuarios = await fachada.GetAllUsersOrdered();
            var topRanking = usuarios.Take(NumFilas).ToList();
            
            CrearRanking(topRanking);
            MensajeAnimo(topRanking, usuarios);
        }
        private void CrearFilaLlena(List<Estadistica> topRanking, int indice)
        {
            TableRow fila = new TableRow(this) { TextAlignment = TextAlignment.Center };

            TextView txtPosicion = new TextView(this) { TextAlignment = TextAlignment.Center, TextSize = 16 };
            txtPosicion.SetTextColor(ColorStateList.ValueOf(Android.Graphics.Color.Black));
            txtPosicion.Text = (indice + 1).ToString() + ".";
            fila.AddView(txtPosicion);

            TextView txtNombre = new TextView(this) { TextAlignment = TextAlignment.Center, TextSize = 16 };
            txtNombre.SetTextColor(ColorStateList.ValueOf(Android.Graphics.Color.Black));
            txtNombre.Text = topRanking[indice].Nombre;
            fila.AddView(txtNombre);

            TextView txtPuntos = new TextView(this) { TextAlignment = TextAlignment.Center, TextSize = 16 };
            txtPuntos.SetTextColor(ColorStateList.ValueOf(Android.Graphics.Color.Black));
            txtPuntos.Text = topRanking[indice].Puntuacion.ToString();
            fila.AddView(txtPuntos);

            tablaRanking.AddView(fila);
        }

        private void CrearFilaVacia(List<Estadistica> topRanking, int indice)
        {
            TableRow fila = new TableRow(this) { TextAlignment = TextAlignment.Center };

            TextView txtPosicion = new TextView(this) { TextAlignment = TextAlignment.Center, TextSize = 16 };
            txtPosicion.SetTextColor(ColorStateList.ValueOf(Android.Graphics.Color.Black));
            txtPosicion.Text = (indice + 1).ToString() + ".";
            fila.AddView(txtPosicion);

            TextView txtNombre = new TextView(this) { TextAlignment = TextAlignment.Center, TextSize = 16 };
            txtNombre.SetTextColor(ColorStateList.ValueOf(Android.Graphics.Color.Black));
            txtNombre.Text = "---";
            fila.AddView(txtNombre);

            TextView txtPuntos = new TextView(this) { TextAlignment = TextAlignment.Center, TextSize = 16 };
            txtPuntos.SetTextColor(ColorStateList.ValueOf(Android.Graphics.Color.Black));
            txtPuntos.Text = "---";
            fila.AddView(txtPuntos);

            tablaRanking.AddView(fila);
        }
        private void CrearRanking(List<Estadistica> topRanking)
        {
            // Introducir los datos de ejemplo
            for (int i = 0; i < NumFilas; i++)
            {
                if (i < topRanking.Count)
                {
                    CrearFilaLlena(topRanking, i);
                }
                else
                {
                    CrearFilaVacia(topRanking, i);
                }
            }
        }

        private async void MensajeAnimo(List<Estadistica> topRanking, List<Estadistica> usuarios)
        {
            var usuarioLogged = await fachada.GetUsuarioLogged();
            bool estaEnElRanking = topRanking.Any(u => u.Nombre == usuarioLogged.Nombre);
            int indice = usuarios.FindIndex(u => u.Nombre == usuarioLogged.Nombre);
            int pos = indice + 1;
            if (estaEnElRanking)
            {
                textAnimo.Text = "Eres el Top " + pos + ". ¡ENHORABUENA!";
            }
            else
            {
                textAnimo.Text = "Estás en la posición " + pos + ". Todavía puedes seguir jugando y sumar puntos para llegar a la cima.";
            }
        }

        private void Atras(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            var i = new Intent(this, typeof(MenuViewModel));
            StartActivity(i);
        }
    }
}