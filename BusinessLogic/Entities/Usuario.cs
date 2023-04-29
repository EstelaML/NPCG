// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    public partial class Usuario
    {
        public Usuario()
        { }

        public Usuario(string uid, string nombre, bool sonidos, int puntos, int musica, int[] preguntasRealizadas)
        {
            Uuid = uid;
            Nombre = nombre;
            Sonidos = sonidos;
            Puntos = puntos;
            Musica = musica;
            PreguntasRealizadas = preguntasRealizadas;
        }
    }
}