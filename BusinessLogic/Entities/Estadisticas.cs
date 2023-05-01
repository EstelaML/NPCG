
namespace preguntaods.Entities
{
    public partial class Estadisticas
    {
        public Estadisticas() 
        { }

        public Estadisticas(int user, int puntuacion, int[] aciertos, int[] fallos)
        { 
        
            Usuario = user;
            Puntuacion = puntuacion;
            Aciertos = aciertos;
            Fallos = fallos;
        
        }
    }
}