// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    public partial class Usuario
    {
        public Usuario()
        { }

        public Usuario(string uid, string nombre, bool sonidos, int puntos, int musica, int[] preguntasRealizadas)
        {
            this.Uuid = uid;
            this.Nombre = nombre;
            this.Sonidos = sonidos;
            this.Puntos = puntos;
            this.Musica = musica;
            this.PreguntasRealizadas = preguntasRealizadas;
        }
    }
}