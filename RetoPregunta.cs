using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using Org.Apache.Commons.Logging;
using preguntaods.Entities;
using preguntaods.Persistencia.Repository;
using preguntaods.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Android.Icu.Text.CaseMap;

namespace preguntaods
{
    [Activity(Label = "Activity1", Theme = "@style/HiddenTitleTheme")]
    public class RetoPregunta : AppCompatActivity
    {
        private TextView enunciado;
        private Button b1;
        private Button b2;
        private Button b3;
        private Button b4;
        private ProgressBar tb;
        private Button abandonar;
        private ImageView imagenOds;
        private TextView puntosText;
        private TextView puntosTotalesText;
        private ImageView heart1;
        private ImageView heart2;

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
        private bool consolidado;
        private int turno;
        private int errores;
        private int ptsTotales;
        private bool contesta;
        private Android.App.AlertDialog alertDialog;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this, Resource.Style.AlertDialogCustom);
            alertDialog = builder.Create(); ;
            // inicializacion de todo lo necesario
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.pregunta);
            repositorio = new RepositorioPregunta();
            fachada = new Facade();

            fachada.EjecutarSonido(this, new EstrategiaSonidoMusica());

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
            heart1 = FindViewById<ImageView>(Resource.Id.heart1);
            heart2 = FindViewById<ImageView>(Resource.Id.heart2);


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
            animation.AnimationEnd += (sender, e) =>
            {
                fachada.PararSonido(new EstrategiaSonidoReloj());
                if (!contesta)
                {
                    turno++;
                    errores++;
                    if (errores < 1)
                        heart1.SetImageResource(Resource.Drawable.icon_emptyHeart);
                    else
                        heart2.SetImageResource(Resource.Drawable.icon_emptyHeart);
                    MostrarAlerta(false, errores == 2);
                }
            };
            animation.AnimationCancel += (sender, e) =>
            {
                contesta = true;

            };
            // sonido de tic tac
            new Handler().PostDelayed(() => {
                // Acciones a realizar cuando quedan 10 segundos o menos
                if (!alertDialog.IsShowing) {
                    fachada.EjecutarSonido(this, new EstrategiaSonidoReloj());
                }
            }, 20000);

            //Abandonar
            abandonar.Click += Atras;

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
                fachada.PararSonido(new EstrategiaSonidoMusica());
            });
            builder.SetNegativeButton("Cancelar", (sender, args) =>
            {
                
            });
            builder.SetCancelable(false);
            alertDialog = builder.Create();
            alertDialog.Window.SetDimAmount(0.8f);
            alertDialog.Show();
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
            if (text.Equals(preguntaActual.Correcta))
            {
                acertado = true;
                fachada.EjecutarSonido(this, new EstrategiaSonidoAcierto());

                AnyadirPts(turno);
                b.SetBackgroundResource(Resource.Drawable.style_preAcierto);
            }
            else {
                fachada.EjecutarSonido(this, new EstrategiaSonidoError());

                if (errores < 1)
                    heart1.SetImageResource(Resource.Drawable.icon_emptyHeart);
                else
                    heart2.SetImageResource(Resource.Drawable.icon_emptyHeart);

                errores++;
                QuitarPts(turno);
                b.SetBackgroundResource(Resource.Drawable.style_preFallo);
            }

            animation.Cancel();
            MostrarAlerta(acertado, ++turno == 9 || errores == 2);
            puntosTotalesText.Text = "Puntos totales: " + ptsTotales;
            return true;
        }
        private void Generarpregunta() {
            b1.SetBackgroundResource(Resource.Drawable.style_pregunta);
            b2.SetBackgroundResource(Resource.Drawable.style_pregunta);
            b3.SetBackgroundResource(Resource.Drawable.style_pregunta);
            b4.SetBackgroundResource(Resource.Drawable.style_pregunta);

            if (turno < 4) { preguntaActual = faciles.First(); faciles.Remove(preguntaActual); puntosText.Text = "Puntuación de la pregunta: 100"; }
            else if (turno < 8) { preguntaActual = medias.First(); medias.Remove(preguntaActual);  puntosText.Text = "Puntuación de la pregunta: 200"; }
            else { preguntaActual = altas.First(); altas.Remove(preguntaActual); puntosText.Text = "Puntuación de la pregunta: 300"; }

            string a = preguntaActual.OdsRelacionada;
            string nombreDeImagen = "icon_ods" + a; // construir el nombre del recurso dinámicamente
            int idDeImagen = Resources.GetIdentifier(nombreDeImagen, "drawable", PackageName); // obtener el identificador de recurso correspondiente
            imagenOds.SetImageResource(idDeImagen);
            

            enunciado.Text = preguntaActual.Enunciado;
            b1.Text = preguntaActual.Respuesta1;
            b2.Text = preguntaActual.Respuesta2;
            b3.Text = preguntaActual.Respuesta3;
            b4.Text = preguntaActual.Respuesta4;
           
            //puntuacionP.Text = "Puntuación de esta pregunta: 100";
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
                    Intent i = new Intent(this, typeof(Menu));
                    StartActivity(i);
                    fachada.PararSonido(new EstrategiaSonidoMusica());
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
                    Intent i = new Intent(this, typeof(Menu));
                    StartActivity(i);
                    fachada.PararSonido(new EstrategiaSonidoMusica());
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
                    mensaje = $"Tienes {ptsTotales} puntos. ¿Deseas abandonar o seguir?";
                }
                else
                {
                    mensaje = $"Tienes {ptsTotales} puntos. ¿Deseas consolidarlos (solo una vez por partida), abandonar o seguir?";
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
                    Intent i = new Intent(this, typeof(Menu));
                    StartActivity(i);
                    fachada.PararSonido(new EstrategiaSonidoMusica());
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
            }
        }
    }
}