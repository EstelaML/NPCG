using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using pruebasEF.Entities;
using pruebasEF.Persistencia;
using System.Linq;
using System.Threading.Tasks;

namespace pruebasEF
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //probarAsync();

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void probarAsync() {
            using (var bd = new SupabaseContext()) {
                Usuario e = new Usuario { nombre = "probando", email = "naa", contraseña = "nada" };
                bd.User.Add(e);
                bd.SaveChanges();


                var lista = bd.User.ToList();
                
            }
        }

    }
}