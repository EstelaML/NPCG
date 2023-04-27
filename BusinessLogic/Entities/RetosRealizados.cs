// ReSharper disable once CheckNamespace
using Android.OS;

namespace preguntaods.Entities
{
    public partial class RetosRealizados
    {
        public RetosRealizados()
        { }

        public RetosRealizados(int user, int[] preguntas, int[] ahorcados)
        {
            this.Usuario = user;
            this.PreguntasRealizadas = preguntas;
            this.AhorcadosRealizados = ahorcados;
        }
    }
}