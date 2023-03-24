using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.Entities;
using preguntaods.Persistencia;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace preguntaods
{
    [Activity(Label = "menu")]
    public class menu : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.menu);

            Button partida = FindViewById<Button>(Resource.Id.partidaB);
            partida.Click += Partida_Click;
        }

        private void Partida_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(pregunta));
            StartActivity(i);
        }
    }
}