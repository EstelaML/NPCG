// ReSharper disable once CheckNamespace
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