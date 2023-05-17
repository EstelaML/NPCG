using preguntaods.BusinessLogic.Partida.Retos;

namespace preguntaods.BusinessLogic.Partida.FabricaRetos
{
    public class FabricaRetoSopa : FabricaReto
    {
        public override IReto CrearReto(int orden)
        {
            return new RetoSopa(orden);
        }
    }
}