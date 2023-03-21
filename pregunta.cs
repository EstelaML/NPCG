using Android.Animation;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.Entities;
using preguntaods.Persistencia;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace preguntaods
{
    [Activity(Label = "Activity1")]
    public class pregunta : AppCompatActivity
    {
        private TextView enunciado;
        private Button b1;
        private Button b2;
        private Button b3;
        private Button b4;
        private ProgressBar tb;
        private List<RetoPregunta> preguntas;
        private int turno;
        private RetoPregunta[] a;
        private MediaPlayer mp;
        private ObjectAnimator animation;

        private TextView textValor;
        private TextView textPenalizacion;
        private TextView textPtsTotales;
        private const int ptsAlta = 300;
        private const int ptsMedia = 200;
        private const int ptsBaja = 100;
        private int ptsTotales;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.pregunta);
            enunciado = FindViewById<TextView>(Resource.Id.pregunta);
            b1 = FindViewById<Button>(Resource.Id.button1);
            b2 = FindViewById<Button>(Resource.Id.button2);
            b3 = FindViewById<Button>(Resource.Id.button3);
            b4 = FindViewById<Button>(Resource.Id.button4);
            tb = FindViewById<ProgressBar>(Resource.Id.timeBar);
            // Create your application here

            using (var bd = new SupabaseContext())
            {
                preguntas = bd.Reto_preguntas.Take(10).ToList();
            }
            a = preguntas.ToArray();
            textPtsTotales = FindViewById<TextView>(Resource.Id.ptsTotales);
            textValor = FindViewById<TextView>(Resource.Id.valor);
            textPenalizacion = FindViewById<TextView>(Resource.Id.penalizacion);

            b1.Click += B1_Click;
            b2.Click += B2_Click;
            b3.Click += B3_Click;
            b4.Click += B4_Click;

            //Animation of time bar
            animation = ObjectAnimator.OfInt(tb, "Progress", 100, 0);
            animation.SetDuration(30000); //30 secs

            // Initialization vars
            turno = 0;
            ptsTotales = 0;
            Generarpregunta();
        }

        private void MostrarPtsPregunta(int turno)
        {
            if (turno <= 3) { textValor.Text = "Valor de la pregunta: " + ptsBaja; }
            else if (turno <= 7) { textValor.Text = "Valor de la pregunta: " + ptsMedia; }
            else { textValor.Text = "Valor de la pregunta: " + ptsAlta; }
        }

        private void MostrarPtsError(int turno)
        {
            if (turno <= 3) { textPenalizacion.Text = "Por cada error: " + -ptsBaja * 2; }
            else if (turno <= 7) { textPenalizacion.Text = "Por cada error: " + -ptsMedia * 2; }
            else { textPenalizacion.Text = "Por cada error: " + -ptsAlta * 2; }
        }
        private void MostrarPtsTotales()
        {
            textPtsTotales.Text = "Puntuación total: " + ptsTotales;
        }
        private void AnyadirPts(int turno)
        {
            if (turno <= 3) { ptsTotales += ptsBaja; }
            else if (turno <= 7) { ptsTotales += ptsMedia; }
            else { ptsTotales += ptsAlta; }
        }

        private void QuitarPts(int turno)
        {
            if (turno <= 3) { ptsTotales += -ptsBaja * 2; }
            else if (turno <= 7) { ptsTotales += -ptsMedia * 2; }
            else { ptsTotales += -ptsAlta * 2; }
        }

        private void hacerSonidoAcierto() {
            Android.Net.Uri uri = Android.Net.Uri.Parse ("android.resource://" + PackageName + "/" + Resource.Raw.megaman_acierto);

            // Configurar un objeto MediaPlayer para reproducir el sonido
            mp = new MediaPlayer();
            mp.SetDataSource(this, uri);
            mp.Prepare();
            mp.Start();
        }

        private void hacerSonidoError()
        {
            Android.Net.Uri uri = Android.Net.Uri.Parse("android.resource://" + PackageName + "/" + Resource.Raw.megaman_error);

            // Configurar un objeto MediaPlayer para reproducir el sonido
            mp = new MediaPlayer();
            mp.SetDataSource(this, uri);
            mp.Prepare();
            mp.Start();
        }


        private void B4_Click(object sender, EventArgs e)
        {
            if (turno != 10)
                EsSolucion(b4.Text, b4);
        }

        private void B3_Click(object sender, EventArgs e)
        {
            if (turno != 10)
                EsSolucion(b3.Text, b3);
        }

        private void B2_Click(object sender, EventArgs e)
        {
            if (turno != 10)
                EsSolucion(b2.Text, b2);
        }

        private void B1_Click(object sender, EventArgs e)
        {
            if (turno != 10)
                EsSolucion(b1.Text, b1);
        }

        private Boolean EsSolucion(string text, Button b)
        {
            if (text.Equals(a[turno].Correcta))
            {
                hacerSonidoAcierto();
                AnyadirPts(turno);
                return true;
            }
            else {
                
                hacerSonidoError();
                QuitarPts(turno);
                return false;
            }
            turno++;
            
            // time out de unos 2/3 segundos para que vea la respuesta correcta
            Generarpregunta();
        }


        private void Generarpregunta() {
            //textAciertos.Text = "Aciertos: " + aciertos + "/" + (aciertos+errores);
            MostrarPtsTotales();
            MostrarPtsPregunta(turno);
            MostrarPtsError(turno);
            if (turno == 10) { return; }
            enunciado.Text = a[turno].Pregunta;
            b1.Text = a[turno].Respuesta1;
            b2.Text = a[turno].Respuesta2;
            b3.Text = a[turno].Respuesta3;
            b4.Text = a[turno].Respuesta4;

            animation.Start();
        }


        public static void Shuffle<T>(List<T> list)
        {
            Random random = new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

    }
}