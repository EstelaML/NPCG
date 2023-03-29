using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.Services;
using System;

namespace preguntaods
{
    [Activity(Label = "", Theme = "@style/AppTheme")]
    public class Menu : AppCompatActivity
    {
        private PreguntadosService servicio;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.menu);
            servicio = new PreguntadosService();

            Button partida = FindViewById<Button>(Resource.Id.partidaB);
            partida.Click += Partida_Click;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.mainMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuItem1:
                    {
                        // add your code  
                        break;
                    }
                case Resource.Id.menuItem2:
                    {
                        // add your code  
                        break;
                    }
                case Resource.Id.menuItem3:
                    {
                        servicio.LogoutAsync();
                        break;
                    }
            }

            return base.OnOptionsItemSelected(item);
        }

        private void Partida_Click(object sender, EventArgs e)
        {
            Sonido s = new Sonido();
            Android.Net.Uri uri = Android.Net.Uri.Parse("android.resource://" + PackageName + "/" + Resource.Raw.click);
            s.HacerSonido(this, uri);
            Intent i = new Intent(this, typeof(RetoPregunta));
            StartActivity(i);
        }
    }
}