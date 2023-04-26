// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    public partial class RetosRealizados
    {
        public RetosRealizados()
        { }

        public RetosRealizados(int pregunta, int ahorcado, int user)
        {
            this.Usuario = user;
            this.Pregunta = pregunta;
            this.Ahorcado = ahorcado;
        }
    }
}