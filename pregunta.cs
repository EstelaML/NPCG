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
using System.Text;

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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.pregunta);

            enunciado = FindViewById<TextView>(Resource.Id.pregunta);
            b1 = FindViewById<Button>(Resource.Id.button1);
            b2 = FindViewById<Button>(Resource.Id.button2);
            b3 = FindViewById<Button>(Resource.Id.button3);
            b4 = FindViewById<Button>(Resource.Id.button4);
            // Create your application here
            error = FindViewById<TextView>(Resource.Id.text);

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
                error.Text = "CORRECTA";
            }
            else {
                error.Text = "Incorrecta";
            }
            turno++;

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