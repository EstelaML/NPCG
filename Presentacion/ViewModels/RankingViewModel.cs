using Android.App;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using Java.Lang;
using Java.Util;
using preguntaods.BusinessLogic.Services;
using preguntaods.Entities;
using System.Collections.Generic;

namespace preguntaods.Presentacion.ViewModels
{
    [Activity(Label = "", Theme = "@style/HiddenTitleTheme")]
    public class RankingViewModel : AppCompatActivity
    {
        private GridLayout rankingGridLayout;
        private Facade fachada;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaRanking);
            fachada = new Facade();
            rankingGridLayout = FindViewById<GridLayout>(Resource.Id.rankingGridLayout);

            var listUsuarios = fachada.Get20OrderedUsers();
            List<string> posiciones = new List<string>();
            int i = 1;
            while (i <= 20)
            {
                posiciones.Add(i.ToString());
                i++;
            }
            for (int j = 0; j < posiciones.Count; j++)
            {
                var textView = new TextView(this);
                textView.Text = posiciones[j];
                rankingGridLayout.AddView(textView);
            }
        }
    }
}