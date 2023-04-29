using Android.App;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.Entities;
using preguntaods.Persistencia.Repository.impl;
using System.Collections.Generic;
using System.Linq;

namespace preguntaods.Presentacion.ViewModels
{
    [Activity(Label = "", Theme = "@style/HiddenTitleTheme")]
    public class RankingViewModel : AppCompatActivity
    {
        private GridView rankingGridView;
        private RepositorioUsuario repositorioUser;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaRanking);

            rankingGridView = FindViewById<GridView>(Resource.Id.rankingGridView);

            
            var respuesta = await repositorioUser.GetAll();
            var listaUsuarios = (List<Usuario>)respuesta.ToList().OrderBy(usuario => usuario.Puntos).Take(20);
        }
    }
}