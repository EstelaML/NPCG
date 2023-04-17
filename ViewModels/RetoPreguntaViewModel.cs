using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.Entities;
using preguntaods.Persistencia.Repository;
using preguntaods.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace preguntaods
{
    [Activity(Label = "Activity1", Theme = "@style/HiddenTitleTheme")]
    public class RetoPreguntaViewModel : AppCompatActivity
    {
        private TextView enunciado;
        private Button botonPregunta1;
        private Button botonPregunta2;
        private Button botonPregunta3;
        private Button botonPregunta4;
        private ProgressBar barTime;
        private Button botonAbandonar;
        private ImageView imagenOds;
        private TextView textoPuntos;
        private TextView textoPuntosTotales;
        private ImageView imagenCorazon1;
        private ImageView imagenCorazon2;
        private List<Pregunta> faciles;
        private List<Pregunta> medias;
        private List<Pregunta> altas;
        private ObjectAnimator animation;
        private Pregunta preguntaActual;
        private RepositorioPregunta repositorio;
        private Facade fachada;
        private const int ptsAlta = 300;
        private const int ptsMedia = 200;
        private const int ptsBaja = 100;
        private const int añadir = 10000;
        private const int restar = 10001;
        private bool consolidado;
        private int turno;
        private int errores;
        private int ptsTotales;
        private bool contesta;
        private Android.App.AlertDialog alertDialog;
        private EstrategiaSonidoMusica musica;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            musica = new EstrategiaSonidoMusica();
            // inicializacion de todo lo necesario
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaRetoPregunta);
            repositorio = new RepositorioPregunta();
            fachada = new Facade();

            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this, Resource.Style.AlertDialogCustom);
            alertDialog = builder.Create();

            //inicialización de los UI Elements
            enunciado = FindViewById<TextView>(Resource.Id.pregunta);
            botonPregunta1 = FindViewById<Button>(Resource.Id.button1);
            botonPregunta2 = FindViewById<Button>(Resource.Id.button2);
            botonPregunta3 = FindViewById<Button>(Resource.Id.button3);
            botonPregunta4 = FindViewById<Button>(Resource.Id.button4);
            barTime = FindViewById<ProgressBar>(Resource.Id.timeBar);
            botonAbandonar = FindViewById<Button>(Resource.Id.volver);
            textoPuntos = FindViewById<TextView>(Resource.Id.textView1);
            textoPuntosTotales = FindViewById<TextView>(Resource.Id.textView2);
            imagenOds = FindViewById<ImageView>(Resource.Id.imagenOds);
            imagenCorazon1 = FindViewById<ImageView>(Resource.Id.heart1);
            imagenCorazon2 = FindViewById<ImageView>(Resource.Id.heart2);

            // Conseguir preguntas 
           /* var preguntasFaciles = await repositorio.GetByDificultad(Pregunta.difBaja);
            var preguntasMedias = await repositorio.GetByDificultad(Pregunta.difMedia);
            var preguntasAltas = await repositorio.GetByDificultad(Pregunta.difAlta);

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
            errores = 0; */

            // botones soluciones
            botonPregunta1.Click += ButtonClick;
            botonPregunta2.Click += ButtonClick;
            botonPregunta3.Click += ButtonClick;
            botonPregunta4.Click += ButtonClick;

            //Animation of time bar
            animation = ObjectAnimator.OfInt(barTime, "Progress", 100, 0);
            animation.SetDuration(30000); //30 secs
            animation.AnimationEnd += (sender, e) =>
            {
                //fachada.PararSonido(new EstrategiaSonidoReloj());
                if (!contesta)
                {
                    turno++;
                    errores++;
                    if (errores < 1)
                        imagenCorazon1.SetImageResource(Resource.Drawable.icon_emptyHeart);
                    else
                        imagenCorazon2.SetImageResource(Resource.Drawable.icon_emptyHeart);
                    MostrarAlerta(false, errores == 2);
                }
            };
            animation.AnimationCancel += (sender, e) =>
            {
                //fachada.PararSonido(new EstrategiaSonidoReloj());
                contesta = true;

            };
            animation.Update += (sender, e) =>
            {
                if (animation.CurrentPlayTime == animation.Duration * 0.9f)
                {
                    //fachada.EjecutarSonido(this, new EstrategiaSonidoReloj());
                }
            };

            //Empezar música
            //fachada.EjecutarSonido(this, musica);

            //botonAbandonar
            botonAbandonar.Click += EventoAbandonar;

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

        private void EventoAbandonar(object sender, EventArgs e)
        {
            Android.App.AlertDialog alertDialog = null;
            string titulo = "¿Estás seguro?";
            string mensaje = "Una vez aceptes perderás tu progreso por completo.";
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this, Resource.Style.AlertDialogCustom);
            builder.SetMessage(mensaje);
            builder.SetTitle(titulo);
            builder.SetPositiveButton("Aceptar", (sender, args) =>
            {
                //fachada.PararSonido(musica);
                Intent i = new Intent(this, typeof(MenuViewModel));
                StartActivity(i);
            });
            builder.SetNegativeButton("Cancelar", (sender, args) =>
            {
                
            });
            builder.SetCancelable(false);
            alertDialog = builder.Create();
            alertDialog.Window.SetDimAmount(0.8f);
            alertDialog.Show();
        }

        private int ActualizarPts(int turno, int puntos, int decision)
        {
            switch (decision)
            {
                case añadir:
                {
                    return AnyadirPts(turno, puntos);
                }
                case restar:
                {
                    return QuitarPts(turno, puntos);
                }
            }

            return 0;
        }
        private int AnyadirPts(int turno, int puntos)
        {
            if (turno <= 3) { puntos += ptsBaja; }
            else if (turno <= 7) { puntos += ptsMedia; }
            else { puntos += ptsAlta; }

            return puntos;
        }
        private int QuitarPts(int turno, int puntos)
        {
            if (turno <= 3) { puntos += -ptsBaja * 2; }
            else if (turno <= 7) { puntos += -ptsMedia * 2; }
            else { puntos += -ptsAlta * 2; }

            if (puntos < 0) puntos = 0;

            return puntos;
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Button boton = sender as Button;
            if (turno != 10)
                EsSolucion(boton.Text, boton);
        }

        private Boolean EsSolucion(string text, Button b)
        {
            bool acertado = false;

            if (text.Equals(preguntaActual.Correcta))
            {
                acertado = true;
                //fachada.EjecutarSonido(this, new EstrategiaSonidoAcierto());

                ptsTotales = ActualizarPts(turno, ptsTotales, añadir);
                b.SetBackgroundResource(Resource.Drawable.style_preAcierto);
            }
            else {
                //fachada.EjecutarSonido(this, new EstrategiaSonidoError());

                if (errores < 1)
                    imagenCorazon1.SetImageResource(Resource.Drawable.icon_emptyHeart);
                else
                    imagenCorazon2.SetImageResource(Resource.Drawable.icon_emptyHeart);

                errores++;
                ptsTotales = ActualizarPts(turno, ptsTotales, restar);
                b.SetBackgroundResource(Resource.Drawable.style_preFallo);
            }

            animation.Cancel();
            MostrarAlerta(acertado, ++turno == 9 || errores == 2);
            textoPuntosTotales.Text = "Puntos totales: " + ptsTotales;
            return true;
        }

        private void Generarpregunta() {
            botonPregunta1.SetBackgroundResource(Resource.Drawable.style_pregunta);
            botonPregunta2.SetBackgroundResource(Resource.Drawable.style_pregunta);
            botonPregunta3.SetBackgroundResource(Resource.Drawable.style_pregunta);
            botonPregunta4.SetBackgroundResource(Resource.Drawable.style_pregunta);

            if (turno < 4) { preguntaActual = faciles.First(); faciles.Remove(preguntaActual); textoPuntos.Text = "Puntuación de la pregunta: 100"; }
            else if (turno < 8) { preguntaActual = medias.First(); medias.Remove(preguntaActual);  textoPuntos.Text = "Puntuación de la pregunta: 200"; }
            else { preguntaActual = altas.First(); altas.Remove(preguntaActual); textoPuntos.Text = "Puntuación de la pregunta: 300"; }

            string nombreDeImagen = "icon_ods" + preguntaActual.OdsRelacionada; // construir el nombre del recurso dinámicamente
            int idDeImagen = Resources.GetIdentifier(nombreDeImagen, "drawable", PackageName); // obtener el identificador de recurso correspondiente
            imagenOds.SetImageResource(idDeImagen);

            enunciado.Text = preguntaActual.Enunciado;
            botonPregunta1.Text = preguntaActual.Respuesta1;
            botonPregunta2.Text = preguntaActual.Respuesta2;
            botonPregunta3.Text = preguntaActual.Respuesta3;
            botonPregunta4.Text = preguntaActual.Respuesta4;
           
            animation.Start();
        }
        public void MostrarAlerta(bool acertado, bool fin)
        {
            string titulo = "";
            string mensaje = "";

            if (fin && acertado)
            {
                titulo = "Felicitaciones";
                mensaje = $"Sumas {ptsTotales} a tu puntuación total.";
                Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this, Resource.Style.AlertDialogCustom);
                builder.SetMessage(mensaje);
                builder.SetTitle(titulo);
                builder.SetNegativeButton("Salir", (sender, args) =>
                {
                    Intent i = new Intent(this, typeof(MenuViewModel));
                    StartActivity(i);
                    //fachada.PararSonido(musica);
                });

                builder.SetCancelable(false);
                alertDialog = builder.Create();
                alertDialog.Show();
            }
            else if (fin && !acertado)
            {
                titulo = "Game Over";
                // si ha consolidado suma los que tenia
                mensaje = "No sumas ningún punto.";
                Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this, Resource.Style.AlertDialogCustom);
                builder.SetMessage(mensaje);
                builder.SetTitle(titulo);
                builder.SetNegativeButton("Salir", (sender, args) =>
                {
                    Intent i = new Intent(this, typeof(MenuViewModel));
                    StartActivity(i);
                    //fachada.PararSonido(musica);
                });

                builder.SetCancelable(false);
                alertDialog = builder.Create();
                alertDialog.Show();
            }
            else if (acertado && !fin)
            {
                titulo = "Has acertado";
                if (consolidado)
                {
                    mensaje = $"Tienes {ptsTotales} puntos. ¿Deseas botonAbandonar o seguir?";
                }
                else
                {
                    mensaje = $"Tienes {ptsTotales} puntos. ¿Deseas consolidarlos (solo una vez por partida), botonAbandonar o seguir?";
                }

                Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this, Resource.Style.AlertDialogCustom);
                builder.SetMessage(mensaje);
                builder.SetTitle(titulo);
                builder.SetPositiveButton("Seguir", (sender, args) =>
                {
                    Generarpregunta();
                });
                builder.SetNeutralButton("Abandonar", (sender, args) =>
                {
                    //fachada.PararSonido(musica);

                    Intent i = new Intent(this, typeof(MenuViewModel));
                    StartActivity(i);
                });
                if (!consolidado)
                {
                    builder.SetNegativeButton("Consolidar", (sender, args) =>
                    {
                        // añadir puntos a su usuario en la base de datos
                        consolidado = true;
                        animation.Cancel();
                        Generarpregunta();
                    });
                }
                builder.SetCancelable(false);
                alertDialog = builder.Create();
                alertDialog.Show();

                new Handler().PostDelayed(() => {
                    // Acciones a realizar cuando quedan 10 segundos o menos
                    if (alertDialog.IsShowing)
                    {
                        //fachada.PararSonido(new EstrategiaSonidoReloj());
                        alertDialog.GetButton((int)DialogButtonType.Positive).PerformClick();
                    }
                }, 10000);
            }
            else if (!acertado && !fin)
            {
                // sumar los consolidados
                titulo = "Vuelve a intentarlo";
                mensaje = $"Tienes {ptsTotales} puntos.";
                Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this, Resource.Style.AlertDialogCustom);
                builder.SetMessage(mensaje);
                builder.SetTitle(titulo);
                builder.SetNegativeButton("Seguir", (sender, args) =>
                {
                    Generarpregunta();
                });
                builder.SetCancelable(false);
                alertDialog = builder.Create();
                alertDialog.Show();

                new Handler().PostDelayed(() => {
                    // Acciones a realizar cuando quedan 10 segundos o menos
                    if (alertDialog.IsShowing)
                    {
                        //fachada.PararSonido(new EstrategiaSonidoReloj());
                        alertDialog.GetButton((int)DialogButtonType.Negative).PerformClick();
                    }
                }, 10000);
            }
        }
    }
}