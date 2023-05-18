using preguntaods.BusinessLogic.Retos;

namespace preguntaods.BusinessLogic.FabricaRetos
{
    public class FabricaRetoFrase : FabricaReto
    {
        public override IReto CrearReto(int orden)
        {
            return new RetoFrase(orden);
        }
    }
}