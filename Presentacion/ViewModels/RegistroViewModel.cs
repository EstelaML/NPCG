using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using AndroidX.AppCompat.App;
using preguntaods.BusinessLogic.EstrategiaSonido;
using preguntaods.BusinessLogic.Fachada;
using preguntaods.Entities;
using System;
using System.Text.RegularExpressions;

namespace preguntaods.Presentacion.ViewModels
{
    [Activity(Label = "")]
    public class RegistroViewModel : AppCompatActivity
    {
        private EditText username;
        private EditText password;
        private EditText email;
        private EditText password2;
        private bool userCorrect;
        private bool passwordCorrect;
        private bool emailCorrect;
        private Button registroB;
        private TextView error;
        private bool vistaContraseña;
        private bool vistaContraseña2;
        private ImageView ojo;
        private ImageView ojo2;
        private ImageView popup1;
        private ImageView popupCorreo;

        private Facade fachada;
        private Sonido sonido;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaRegistro);
            sonido = new Sonido();
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);

            fachada = Facade.GetInstance();

            passwordCorrect = false;
            emailCorrect = false;
            userCorrect = false;

            vistaContraseña = false;
            vistaContraseña2 = false;

            var atras = FindViewById<ImageButton>(Resource.Id.button1);
            if (atras != null) atras.Click += Atras;

            username = FindViewById<EditText>(Resource.Id.nombreUsuario);
            if (username != null) username.TextChanged += User_TextChanged;

            email = FindViewById<EditText>(Resource.Id.correo);
            if (email != null)
            {
                email.TextChanged += Email_TextChanged;
                email.FocusChange += Email_FocusChange;
            }

            password = FindViewById<EditText>(Resource.Id.contraseña);
            if (password != null)
            {
                password.InputType = InputTypes.TextVariationPassword | InputTypes.ClassText;
                password.FocusChange += Password_FocusChange;
            }

            password2 = FindViewById<EditText>(Resource.Id.contraseña2);
            if (password2 != null)
            {
                password2.TextChanged += Password_Click;
                password2.InputType = InputTypes.TextVariationPassword | InputTypes.ClassText;
            }

            error = FindViewById<TextView>(Resource.Id.error);

            registroB = FindViewById<Button>(Resource.Id.registroB);
            if (registroB != null) registroB.Click += Registrar;

            ojo = FindViewById<ImageView>(Resource.Id.ojoContraseña);
            if (ojo != null) ojo.Click += Ojo_Click1;

            ojo2 = FindViewById<ImageView>(Resource.Id.ojoContraseña2);
            if (ojo2 != null) ojo2.Click += Ojo_Click2;

            popup1 = FindViewById<ImageView>(Resource.Id.imageView1);
            if (popup1 != null) popup1.Visibility = ViewStates.Invisible;

            popupCorreo = FindViewById<ImageView>(Resource.Id.popupCorreo);
            if (popupCorreo != null) popupCorreo.Visibility = ViewStates.Invisible;
        }

        private void Email_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (email.HasFocus)
            {
                var animacion = new AlphaAnimation(0f, 1f);
                animacion.Duration = 500; // Duración de la animación en milisegundos

                // Asignar la animación a la imagen y hacerla invisible
                popupCorreo.StartAnimation(animacion);
                popupCorreo.Visibility = ViewStates.Visible;
            }
            else
            {
                var animacion = new AlphaAnimation(1f, 0f);
                animacion.Duration = 500; // Duración de la animación en milisegundos

                // Asignar la animación a la imagen y hacerla invisible
                popupCorreo.StartAnimation(animacion);
                popupCorreo.Visibility = ViewStates.Invisible;
            }
        }

        private void Password_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (password.HasFocus)
            {
                var animacion = new AlphaAnimation(0f, 1f);
                animacion.Duration = 500; // Duración de la animación en milisegundos

                // Asignar la animación a la imagen y hacerla invisible
                popup1.StartAnimation(animacion);
                popup1.Visibility = ViewStates.Visible;
            }
            else
            {
                var animacion = new AlphaAnimation(1f, 0f);
                animacion.Duration = 500; // Duración de la animación en milisegundos

                // Asignar la animación a la imagen y hacerla invisible
                popup1.StartAnimation(animacion);
                popup1.Visibility = ViewStates.Invisible;
            }
        }

        private void Ojo_Click1(object sender, EventArgs e)
        {
            vistaContraseña = !vistaContraseña;
            if (vistaContraseña)
            {
                // se muestra
                password.InputType = InputTypes.TextVariationVisiblePassword;
                ojo.SetImageResource(Resource.Drawable.icon_ojo_cerrado);
            }
            else
            {
                // no se muestra
                password.InputType = InputTypes.TextVariationPassword | InputTypes.ClassText;
                ojo.SetImageResource(Resource.Drawable.icon_ojo_abierto);
            }
        }

        private void Ojo_Click2(object sender, EventArgs e)
        {
            vistaContraseña2 = !vistaContraseña2;
            if (vistaContraseña2)
            {
                // se muestra
                password2.InputType = InputTypes.TextVariationVisiblePassword;
                ojo2.SetImageResource(Resource.Drawable.icon_ojo_cerrado);
            }
            else
            {
                // no se muestra
                password2.InputType = InputTypes.TextVariationPassword | InputTypes.ClassText;
                ojo2.SetImageResource(Resource.Drawable.icon_ojo_abierto);
            }
        }

        private void User_TextChanged(object sender, TextChangedEventArgs e)
        {
            userCorrect = true;
            error.Text = "";
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            emailCorrect = true;
            error.Text = "";
        }

        private void Password_Click(object sender, EventArgs e)
        {
            if (password.Text != null && password2.Text != null) { passwordCorrect = true; }
            error.Text = "";
        }

        private async void Registrar(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            // Locks a pasar para el registro

            // Si ha sido incorrecta, que se haya cambiado
            if (!passwordCorrect || !emailCorrect || !userCorrect) return;

            // El correo contenga @gmail.com
            if (email.Text != null && !email.Text.Contains("@gmail.com"))
            {
                error.Text = "Elija un correo electrónico válido";
                emailCorrect = false;
                return;
            }

            var regexPuntuacion = new Regex("[.,@#*!?¿¡`·_-]");
            var regexNumero = new Regex("\\d");

            // que la contraseña cumpla los requisitos
            if (password.Text != null && (!regexPuntuacion.IsMatch(password.Text) || !regexNumero.IsMatch(password.Text) || password.Text.Length < 8))
            {
                error.Text = "La contraseña no cumple los requisitos";
                passwordCorrect = false;
                return;
            }

            // Las 2 contraseñas sean iguales
            if (password.Text != password2.Text)
            {
                error.Text = "Las contraseñas no coinciden";
                passwordCorrect = false;
                return;
            }

            // El usuario no está en la base de datos registrado

            if (userCorrect)
            {
                UserDialogs.Instance.ShowLoading("Comprobando...", MaskType.Clear);
                var respuesta = await fachada.ComprobarUsuario(username.Text);

                if (!respuesta)
                {
                    error.Text = "El nombre de usuario está ya en uso, utiliza otro.";
                    userCorrect = false;

                    UserDialogs.Instance.HideLoading();

                    return;
                }
            }

            try
            {
                var userAux = await fachada.SignUpAsync(email.Text, password.Text);

                if (userAux != null)
                {
                    var user = new Usuario(userAux.Id, username.Text, 100, 100);
                    await fachada.NewUsuario(user);
                    await fachada.CrearEstadisticas(user);

                    UserDialogs.Instance.HideLoading();
                    UserDialogs.Instance.Alert(new AlertConfig
                    {
                        Message = "Te has registrado con exito! Verifica tu cuenta con el correo que se te ha enviado",
                        OkText = "Entendido",
                        OnAction = () =>
                        {
                            var i = new Intent(this, typeof(InicioSesionViewModel));
                            StartActivity(i);
                            Finish();
                        }
                    });
                }
                else
                {
                    UserDialogs.Instance.HideLoading();

                    error.Text = "Ese correo ya está en uso, utiliza otro o inicia sesión";
                }
            }
            catch (Supabase.Gotrue.RequestException)
            {
                UserDialogs.Instance.HideLoading();

                error.Text = "La contraseña debe estar formada como mínimo de 8 caracteres";
            }
        }

        private void Atras(object sender, EventArgs e)
        {
            sonido.SetEstrategia(new EstrategiaSonidoClick(), this);
            sonido.EjecutarSonido();

            var i = new Intent(this, typeof(InicioSesionViewModel));
            StartActivity(i);
            Finish();
        }
    }
}