using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.BusinessLogic.EstrategiaSonido;
using preguntaods.BusinessLogic.Services;
using preguntaods.Entities;

namespace preguntaods.ViewModels
{
    [Activity(Label = "", Theme = "@style/HiddenTitleTheme")]
    public class RankingViewModel : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaRanking);
        }
    }
}