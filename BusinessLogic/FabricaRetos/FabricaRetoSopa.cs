using preguntaods.BusinessLogic.Retos;

namespace preguntaods.BusinessLogic.FabricaRetos
{
    public class FabricaRetoSopa : FabricaReto
    {
        public override IReto CrearReto(int orden)
        {
            return new RetoSopa(orden);
        }
    }
}