using preguntaods.BusinessLogic.Retos;

namespace preguntaods.BusinessLogic.FabricaRetos
{
    public class FabricaRetoAhorcado : FabricaReto
    {
        public override IReto CrearReto(int orden)
        {
            return new RetoAhorcado(orden);
        }
    }
}