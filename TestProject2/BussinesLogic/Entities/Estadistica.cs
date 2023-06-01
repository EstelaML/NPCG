// ReSharper disable once CheckNamespace

namespace preguntaods.Entities
{
    public partial class Estadistica
    {
        public Estadistica()
        { }

        public Estadistica(string user, int puntuacion, int[] aciertos, int[] fallos, string nombre, int p, DateTime d, int s)
        {
            Usuario = user;
            Puntuacion = puntuacion;
            Aciertos = aciertos;
            Fallos = fallos;
            Nombre = nombre;
            Tiempo = null;
            PuntuacionDiaria = p;
            FechaDiaria = d;
            PuntuacionSemanal = s;
        }
    }
}