using preguntaods.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace preguntaods.Entities
{
    public class RetoAhorcado : Reto
    {
        private readonly int type;
        private Ahorcado ahorcado;
        private static PreguntadosService servicio;

        public RetoAhorcado(int dificultad)
        {
            type = TypeAhorcado;
            servicio = new PreguntadosService();
            servicio.InitAhorcadoList().ContinueWith(t => { _ = SetDiff(dificultad); });
        }

        public override int GetType()
        {
            return type;
        }

        public Ahorcado GetAhorcado() { return ahorcado;  }

        public async Task SetDiff(int orden)
        {
            if (orden < 4 || orden == 10)
            {
                ahorcado = await servicio.SolicitarAhorcado(Ahorcado.difBaja);
            }
            else if (4 <= orden && orden < 7 || orden == 11)
            {
                ahorcado = await servicio.SolicitarAhorcado(Ahorcado.difMedia);
            }
            else
            {
                ahorcado = await servicio.SolicitarAhorcado(Ahorcado.difAlta);
            }
        }
    }
}