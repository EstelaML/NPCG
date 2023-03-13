using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using pruebasEF.Entities;
using pruebasEF.Persistencia;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Android.Graphics;
using Android.Animation;
using Android.Views.Animations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Timers;

namespace pruebasEF
{
    [Activity(Label = "Activity1")]
    public class pregunta : AppCompatActivity
    {
        private TextView enunciado;
        private Button b1;
        private Button b2;
        private Button b3;
        private Button b4;
        private List<RetoPregunta> preguntas;
        private int turno;
        private TextView error;
        private RetoPregunta[] a;
        private LinearLayout fondo;
        private int intentos = 2;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.pregunta);

            enunciado = FindViewById<TextView>(Resource.Id.pregunta);
            b1 = FindViewById<Button>(Resource.Id.button1);
            b2 = FindViewById<Button>(Resource.Id.button2);
            b3 = FindViewById<Button>(Resource.Id.button3);
            b4 = FindViewById<Button>(Resource.Id.button4);
            error = FindViewById<TextView>(Resource.Id.text);
            fondo = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            using (var bd = new SupabaseContext())
            {
                preguntas = bd.Reto_preguntas.Take(10).ToList();
            }
            a = preguntas.ToArray();

            b1.Click += B1_Click;
            b2.Click += B2_Click;
            b3.Click += B3_Click;
            b4.Click += B4_Click;

            turno = 0;
            Generarpregunta();


        }

        private void B4_Click(object sender, EventArgs e)
        {
            EsSolucion(b4.Text, b4);
           
        }

        private void B3_Click(object sender, EventArgs e)
        {
            EsSolucion(b3.Text, b3);
        }

        private void B2_Click(object sender, EventArgs e)
        {
            EsSolucion(b2.Text, b2);
        }

        private void B1_Click(object sender, EventArgs e)
        {
            EsSolucion(b1.Text, b1);
        }

        private void EsSolucion(string text, Button b)
        {
            if (turno == 10) return;
            if (text.Equals(a[turno].Correcta))
            {
                Android.Graphics.Color rojoIntenso = new Android.Graphics.Color(213, 250, 121);
                fondo.SetBackgroundColor(rojoIntenso);
                error.Text = "CORRECTA";
            }
            else {
                Android.Graphics.Color rojoIntenso = new Android.Graphics.Color(239, 87, 100);
                error.Text = "INCORRECTA";
                intentos--;
                Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
                alert.SetTitle("Respuesta incorrecta");
                alert.SetMessage("Recuerda que tienes "+ intentos + " intentos");

                alert.SetPositiveButton("Aceptar", (senderAlert, argsAlert) =>
                {
                });
                Dialog dialog = alert.Create();
                dialog.Show();
                fondo.SetBackgroundColor(rojoIntenso);
            }
            turno++;

            Timer timer1 = new Timer();
            timer1.Interval = 2500;
            timer1.Elapsed += Timer1_Elapsed;
            timer1.Start();
            
        }

        private void Timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            Generarpregunta();

        }

        private void Generarpregunta() {
            if (turno == 10) { return; }
            enunciado.Text = a[turno].Pregunta;
            b1.Text = a[turno].Respuesta1;
            b2.Text = a[turno].Respuesta2;
            b3.Text = a[turno].Respuesta3;
            b4.Text = a[turno].Respuesta4;
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