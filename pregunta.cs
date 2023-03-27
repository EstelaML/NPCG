using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.Entities;
using preguntaods.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace preguntaods
{
    [Activity(Label = "Activity1")]
    public class Pregunta : AppCompatActivity
    {
        private TextView enunciado;
        private Button b1;
        private Button b2;
        private Button b3;
        private Button b4;
        private ProgressBar tb;
        private Button abandonar;
        private List<RetoPregunta> preguntas;
        private int turno;
        private List<RetoPregunta> faciles;
        private List<RetoPregunta> medias;
        private List<RetoPregunta> altas;
        private ObjectAnimator animation;
        private int errores;
        private TextView textValor;
        private TextView textPenalizacion;
        private TextView textPtsTotales;
        private const int ptsAlta = 300;
        private const int ptsMedia = 200;
        private const int ptsBaja = 100;
        private int ptsTotales;
        private Sonido musicaFondo;
        private TextView puntuacionPregunta;
        private RetoPregunta preguntaActual;
        private PreguntaRepositorioSingleton repositorio;
        private ImageView imagenOds;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            errores = 0;
            musicaFondo = new Sonido();
            Android.Net.Uri uri = Android.Net.Uri.Parse("android.resource://" + PackageName + "/" + Resource.Raw.fondo_molon);
            musicaFondo.HacerSonido(this, uri);
            imagenOds = FindViewById<ImageView>(Resource.Id.imagenOds);
            //puntuacionPregunta = FindViewById<TextView>(Resource.Id.puntuacionPreguntaActual);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.pregunta);
            enunciado = FindViewById<TextView>(Resource.Id.pregunta);
            b1 = FindViewById<Button>(Resource.Id.button1);
            b2 = FindViewById<Button>(Resource.Id.button2);
            b3 = FindViewById<Button>(Resource.Id.button3);
            b4 = FindViewById<Button>(Resource.Id.button4);
            tb = FindViewById<ProgressBar>(Resource.Id.timeBar);
            abandonar = FindViewById<Button>(Resource.Id.volver);

            /* repositorio = new PreguntaRepositorioSingleton();
             var preguntas = await repositorio.GetAll();
             a = preguntas.ToArray();
            */

            repositorio = new PreguntaRepositorioSingleton();
            var preguntasFaciles = await repositorio.GetByDificultad("baja");
            var preguntasMedias = await repositorio.GetByDificultad("media");
            var preguntasAltas = await repositorio.GetByDificultad("alta");
            faciles = preguntasFaciles.ToList();
            medias = preguntasMedias.ToList();
            altas = preguntasAltas.ToList();
            Shuffle(faciles);
            Shuffle(medias);
            Shuffle(altas);

            b1.Click += B1_Click;
            b2.Click += B2_Click;
            b3.Click += B3_Click;
            b4.Click += B4_Click;

            abandonar.Click += Atras;

            //Animation of time bar
            animation = ObjectAnimator.OfInt(tb, "Progress", 100, 0);
            animation.SetDuration(30000); //30 secs

            // Initialization vars
            turno = 0;
            ptsTotales = 0;
            Generarpregunta();
        }

        public static void Shuffle<T>(List<T> list)
        {
            Random random = new Random();

            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);

                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }


        private void Atras(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(Menu));
            StartActivity(i);
        }

        private int MostrarPtsPregunta(int turno)
        {
            int res;
            if (turno <= 3) { textValor.Text = "Valor de la pregunta: " + ptsBaja; res = ptsBaja; }
            else if (turno <= 7) { textValor.Text = "Valor de la pregunta: " + ptsMedia; res = ptsMedia; }
            else { textValor.Text = "Valor de la pregunta: " + ptsAlta; res = ptsAlta; }
            return res;
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
            if (ptsTotales < 0) ptsTotales = 0;
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
            bool acertado = false;
            Android.Net.Uri uri = null;
            if (text.Equals(preguntaActual.Correcta))
            {
                acertado = true;
                uri = Android.Net.Uri.Parse("android.resource://" + PackageName + "/" + Resource.Raw.megaman_acierto);

                AnyadirPts(turno);
                b.SetBackgroundResource(Resource.Drawable.preAcierto);
            }
            else {
                uri = Android.Net.Uri.Parse("android.resource://" + PackageName + "/" + Resource.Raw.error_pato);
                errores++;
                QuitarPts(turno);
                b.SetBackgroundResource(Resource.Drawable.preFallo);
            }
            Sonido s = new Sonido();
            s.HacerSonido(this, uri);
            animation.End();
            MostrarAlerta(acertado);
            turno++;
            return true;
        }
        private void Generarpregunta() {
            if (turno == 10) { return; }
            b1.SetBackgroundResource(Resource.Drawable.pre);
            b2.SetBackgroundResource(Resource.Drawable.pre);
            b3.SetBackgroundResource(Resource.Drawable.pre);
            b4.SetBackgroundResource(Resource.Drawable.pre);

            if (turno < 4) { preguntaActual = faciles.First(); faciles.Remove(preguntaActual); }//puntuacionPregunta.Text = "Puntuación de la pregunta: 100"; }
            else if (turno < 8) { preguntaActual = medias.First(); medias.Remove(preguntaActual); }// puntuacionPregunta.Text = "Puntuación de la pregunta: 200"; }
            else { preguntaActual = altas.First(); altas.Remove(preguntaActual); }// puntuacionPregunta.Text = "Puntuación de la pregunta: 300"; }

            //imagenOds.SetImageResource(Resource.Drawable.ods1);

            enunciado.Text = preguntaActual.Pregunta;
            b1.Text = preguntaActual.Respuesta1;
            b2.Text = preguntaActual.Respuesta2;
            b3.Text = preguntaActual.Respuesta3;
            b4.Text = preguntaActual.Respuesta4;
           
            //puntuacionP.Text = "Puntuación de esta pregunta: 100";
            animation.Start();
        }
        public void MostrarAlerta(bool acertado)
        {
            string consolidar = "consolidar";
            string abandonar = "abandonar";
            string seguir = "seguir jugando";
            string titulo = "";

            if (acertado) titulo = "Enhorabuena, ¡has acertado!";
            else
            {
                titulo = "Oooooh, solo te queda una vida, ¡ten cuidado!";
            }

            string mensaje = $"Tienes {ptsTotales} puntos. ¿Deseas consolidarlos, abandonarlos o seguir?";
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this, Resource.Style.AlertDialogCustom);

            if (errores != 2)
            {
                builder.SetTitle(titulo);
                builder.SetMessage($"Tu puntuación es de {ptsTotales} puntos. ¿Deseas {seguir}, {consolidar} o {abandonar}?");
                builder.SetPositiveButton("Seguir", (sender, args) =>
                {
                    Generarpregunta();
                });

                builder.SetNegativeButton("Abandonar", (sender, args) =>
                {
                    Intent i = new Intent(this, typeof(Menu));
                    StartActivity(i);
                    musicaFondo.PararSonido();
                });

                builder.SetNeutralButton("Consolidar", (sender, args) =>
                {
                    // añadir puntos a su usuario en la base de datos
                    Intent i = new Intent(this, typeof(Menu));
                    StartActivity(i);
                    musicaFondo.PararSonido();
                });
            }
            else // si comete dos fallos
            {
                builder.SetTitle("Lo siento, ¡has perdido!");
                builder.SetMessage($"Tu puntuación es 0. Ya no puedes seguir jugando :(");
                builder.SetPositiveButton("Salir", (sender, args) =>
                {
                    Intent i = new Intent(this, typeof(Menu));
                    StartActivity(i);
                    musicaFondo.PararSonido();
                });
            }
            builder.SetCancelable(false);
            Android.App.AlertDialog alertDialog = builder.Create();
            alertDialog.Show();
        }


    }
}