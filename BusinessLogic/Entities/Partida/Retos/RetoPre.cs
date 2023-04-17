using Android.Media;
using Java.Util;
using preguntaods.Services;
using System.Collections.Generic;

namespace preguntaods.Entities
{
    public class RetoPre : Reto
    {
        private Pregunta pregunta;
        private PreguntadosService servicio;
        private readonly int type;
        private List<Reto> retos;
        private List<Pregunta> preguntas;
        private int orden;
        public RetoPre(List<Reto> listRetos, int orden)
        {
            servicio = new PreguntadosService();
            this .orden = orden;
            setDif();
            retos = listRetos;
            type = typePregunta;
        }

        public override int GetType()
        {
            return type;
        }

        public Pregunta GetPregunta()
        {
            return pregunta;
        }

        private void setDif()
        {

            if (orden < 4 || orden == 10)
            {

                SetPregunta(1);

            } else if (orden < 7 || orden == 11)
            {

                SetPregunta(2);

            } else { SetPregunta(3); }

        }

        private async void SetPregunta(int dif)
        {
            switch (dif)
            {
                case 1:
                    {
                        pregunta = await servicio.SolicitarPregunta(Pregunta.difBaja, retos);
                        break;
                    }
                case 2:
                    {
                        pregunta = await servicio.SolicitarPregunta(Pregunta.difMedia, retos);
                        break;
                    }
                case 3:
                    {
                        pregunta = await servicio.SolicitarPregunta(Pregunta.difAlta, retos);
                        break;
                    }
            }
        }
    }
}