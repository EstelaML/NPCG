// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    public partial class RetosRealizados
    {
        public RetosRealizados()
        { }

        public RetosRealizados(int user, int[] preguntas, int[] ahorcados)
        {
            Usuario = user;
            PreguntasRealizadas = preguntas;
            AhorcadosRealizados = ahorcados;
        }
    }
}