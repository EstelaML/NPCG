using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;
using preguntaods.Entities;
using preguntaods.Services;
using System.Diagnostics;

namespace preguntaods
{
    [Activity(Label = "", Theme = "@style/AppTheme")]
    public class VistaPartidaViewModel : AppCompatActivity
    {
        // Vars
        private PartidaDirector director;
        private Facade fachada;
        private Reto reto;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Inicio de la vista
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaPartida);

            // Cargar partida
            director = new PartidaDirector();
            PartidaBuilder builder = new PartidaBuilder();
            director.ConstructPartida(builder);
            Partida partida = builder.GetPartida();

            reto = partida.GetRetoActual();

            //poner barra de carga 


            // Poner la vista del tipo de reto concreto
            UpdateView();

            // BORRAR
            Intent i = new Intent(this, typeof(RetoPreguntaViewModel));
            StartActivity(i);
            // BORRAR
        }

        public void UpdateView()
        {
            switch (reto.GetType())
            {
                case Reto.typePregunta:
                    {
                        SetContentView(Resource.Layout.vistaRetoPregunta);
                        break;
                    }
                case Reto.typeAhorcado:
                    {
                        //SetContentView(Resource.Layout.vistaRetoAhorcado);
                        break;
                    }
                case Reto.typeFrase:
                    {
                        //SetContentView(Resource.Layout.vistaRetoFrase);
                        break;
                    }
                case Reto.typeSopa:
                    {
                        //SetContentView(Resource.Layout.vistaRetoSopa);
                        break;
                    }
            }
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
                        fachada.LogoutAsync();

                        Intent i = new Intent(this, typeof(InicioSesionViewModel));
                        StartActivity(i);

                        break;
                    }
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}