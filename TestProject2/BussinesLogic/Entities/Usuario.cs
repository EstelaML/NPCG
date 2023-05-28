// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    public partial class Usuario
    {
        public Usuario()
        { }

        public Usuario(string uid, string nombre, int sonidos, int musica, int[] volumenActivado)
        {
            Uuid = uid;
            Nombre = nombre;
            Sonidos = sonidos;
            Musica = musica;
            VolumenActivado = volumenActivado;
        }
    }
}