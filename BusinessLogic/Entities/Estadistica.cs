﻿namespace preguntaods.Entities
{
    public partial class Estadistica
    {
        public Estadistica()
        { }

        public Estadistica(string user, int puntuacion, int[] aciertos, int[] fallos)
        {
            Usuario = user;
            Puntuacion = puntuacion;
            Aciertos = aciertos;
            Fallos = fallos;
        }
    }
}