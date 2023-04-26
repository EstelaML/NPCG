using Android.App;
using Android.OS;
using AndroidX.AppCompat.App;

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