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

        public RetoAhorcado()
        {
            type = TypeAhorcado;
            servicio = new PreguntadosService();
            servicio.InitAhorcadoList().ContinueWith(t => { _ = SetDiff(); });
        }

        public override int GetType()
        {
            return type;
        }

        public Ahorcado GetAhorcado() { return ahorcado;  }

        public async Task SetDiff()
        {
            ahorcado = await servicio.SolicitarAhorcado();
        }
    }
}