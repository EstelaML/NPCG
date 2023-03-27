using Android.Animation;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.Entities;
using preguntaods.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using static Android.Icu.Text.CaseMap;

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
        public ImageView imagenOds;
        public TextView puntosText;
        public TextView puntosTotalesText;
        public bool consolidado;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            // musica 
            musicaFondo = new Sonido();
            Android.Net.Uri uri = Android.Net.Uri.Parse("android.resource://" + PackageName + "/" + Resource.Raw.fondo_molon);
            musicaFondo.HacerSonido(this, uri);

            // inicializacion de todo lo necesario
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.pregunta);
            enunciado = FindViewById<TextView>(Resource.Id.pregunta);
            b1 = FindViewById<Button>(Resource.Id.button1);
            b2 = FindViewById<Button>(Resource.Id.button2);
            b3 = FindViewById<Button>(Resource.Id.button3);
            b4 = FindViewById<Button>(Resource.Id.button4);
            tb = FindViewById<ProgressBar>(Resource.Id.timeBar);
            abandonar = FindViewById<Button>(Resource.Id.volver);
            puntosText = FindViewById<TextView>(Resource.Id.textView1);
            puntosTotalesText = FindViewById<TextView>(Resource.Id.textView2);
            imagenOds = FindViewById<ImageView>(Resource.Id.imagenOds);
            repositorio = new PreguntaRepositorioSingleton();
            

            // Conseguir preguntas 
            var preguntasFaciles = await repositorio.GetByDificultad("baja");
            var preguntasMedias = await repositorio.GetByDificultad("media");
            var preguntasAltas = await repositorio.GetByDificultad("alta");
            faciles = preguntasFaciles.ToList();
            medias = preguntasMedias.ToList();
            altas = preguntasAltas.ToList();
            Shuffle(faciles);
            Shuffle(medias);
            Shuffle(altas);

            // Initialization vars
            turno = 0;
            ptsTotales = 0;
            consolidado = false;
            errores = 0;

            // botones soluciones
            b1.Click += B1_Click;
            b2.Click += B2_Click;
            b3.Click += B3_Click;
            b4.Click += B4_Click;

            //Animation of time bar
            animation = ObjectAnimator.OfInt(tb, "Progress", 100, 0);
            animation.SetDuration(30000); //30 secs

            Generarpregunta();

            abandonar.Click += Atras;
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
            Android.App.AlertDialog alertDialog = null;
            string titulo = "¿Estás seguro?";
            string mensaje = "Una vez aceptes perderás tu progreso por completo.";
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this, Resource.Style.AlertDialogCustom);
            builder.SetMessage(mensaje);
            builder.SetTitle(titulo);
            builder.SetPositiveButton("Aceptar", (sender, args) =>
            {
                Intent i = new Intent(this, typeof(Menu));
                StartActivity(i);
                musicaFondo.PararSonido();
            });
            builder.SetNegativeButton("Cancelar", (sender, args) =>
            {
                
            });
            builder.SetCancelable(false);
            alertDialog = builder.Create();
            alertDialog.Show();
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
            MostrarAlerta(acertado, false);
            turno++;
            puntosTotalesText.Text = "Puntos totales: " + ptsTotales;
            return true;
        }
        private void Generarpregunta() {
            if (turno == 10) {
                MostrarAlerta(false, true);
            
            }
            b1.SetBackgroundResource(Resource.Drawable.pre);
            b2.SetBackgroundResource(Resource.Drawable.pre);
            b3.SetBackgroundResource(Resource.Drawable.pre);
            b4.SetBackgroundResource(Resource.Drawable.pre);

            if (turno < 4) { preguntaActual = faciles.First(); faciles.Remove(preguntaActual); puntosText.Text = "Puntuación de la pregunta: 100"; }
            else if (turno < 8) { preguntaActual = medias.First(); medias.Remove(preguntaActual);  puntosText.Text = "Puntuación de la pregunta: 200"; }
            else { preguntaActual = altas.First(); altas.Remove(preguntaActual); puntosText.Text = "Puntuación de la pregunta: 300"; }

            var a = preguntaActual.OdsRelacionada;
            String imagePath = "Resources.Drawable.ods" + a;
            Bitmap bitmap = BitmapFactory.DecodeFile(imagePath);

            imagenOds.SetImageResource(Resource.Drawable.ods1);
            //imagenOds.SetImageResource((int) bitmap);   
            //imagenOds.SetImageResource(Resource.Drawable.ods1);

            enunciado.Text = preguntaActual.Pregunta;
            b1.Text = preguntaActual.Respuesta1;
            b2.Text = preguntaActual.Respuesta2;
            b3.Text = preguntaActual.Respuesta3;
            b4.Text = preguntaActual.Respuesta4;
           
            //puntuacionP.Text = "Puntuación de esta pregunta: 100";
            animation.Start();
        }
        public void MostrarAlerta(bool acertado, bool fin)
        {
            if (!fin)
            {
                string titulo = "";
                Android.App.AlertDialog alertDialog = null;
                if (acertado)
                {
                    string mensaje;
                    titulo = "Enhorabuena, ¡has acertado!";
                    if (consolidado) 
                    {
                        mensaje = $"Tienes {ptsTotales} puntos. ¿Deseas abandonar o seguir?";
                    } else mensaje = $"Tienes {ptsTotales} puntos. ¿Deseas consolidarlos (solo una vez por partida), abandonar o seguir?";
                    Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this, Resource.Style.AlertDialogCustom);
                    builder.SetMessage(mensaje);
                    builder.SetTitle(titulo);
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
                    if (!consolidado) {
                        builder.SetNeutralButton("Consolidar", (sender, args) =>
                        {
                            // añadir puntos a su usuario en la base de datos
                            consolidado = true;
                            Generarpregunta();
                        });
                    }

                    builder.SetCancelable(false);
                    alertDialog = builder.Create();
                    alertDialog.Show();
                }
                else
                {
                    if (errores == 2)
                    {
                        titulo = "Lo siento, ¡has perdido!";
                        string mensaje = $"Tienes 0 puntos.";
                        Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this, Resource.Style.AlertDialogCustom);
                        builder.SetMessage(mensaje);
                        builder.SetTitle(titulo);
                        builder.SetNegativeButton("Salir", (sender, args) =>
                        {
                            Intent i = new Intent(this, typeof(Menu));
                            StartActivity(i);
                            musicaFondo.PararSonido();
                        });

                        builder.SetCancelable(false);
                        alertDialog = builder.Create();
                        alertDialog.Show();
                    }
                    else
                    {
                        titulo = "Ooooooh, solo te queda una vida, ¡cuidado!";
                        string mensaje = $"Tienes {ptsTotales} puntos. ¿Deseas abandonar o seguir?";
                        Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this, Resource.Style.AlertDialogCustom);
                        builder.SetMessage(mensaje);
                        builder.SetTitle(titulo);
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

                        builder.SetCancelable(false);
                        alertDialog = builder.Create();
                        alertDialog.Show();

                    }
                }
            }
            else
            {
                Android.App.AlertDialog alertDialog = null;
                string titulo = "Ole mi arma, ¡lo has conseguido!";
                string mensaje = $"Te llevas {ptsTotales} puntos.";
                Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this, Resource.Style.AlertDialogCustom);
                builder.SetMessage(mensaje);
                builder.SetTitle(titulo);
                builder.SetNegativeButton("Salir", (sender, args) =>
                {
                    Intent i = new Intent(this, typeof(Menu));
                    StartActivity(i);
                    musicaFondo.PararSonido();
                });

                builder.SetCancelable(false);
                alertDialog = builder.Create();
                alertDialog.Show();
            }
        }
    }
}