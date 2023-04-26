// ReSharper disable once CheckNamespace
using Android.OS;

namespace preguntaods.Entities
{
    public partial class RetosRealizados
    {
        public RetosRealizados()
        { }

        public RetosRealizados(int pregunta, int ahorcado, int user, int[] preguntas, int[] ahorcados)
        {
            this.Usuario = user;
            this.Pregunta = pregunta;
            this.Ahorcado = ahorcado;
            this.Pregunta2 = preguntas;
            this.Ahorcado2 = ahorcados;
        }
    }
}