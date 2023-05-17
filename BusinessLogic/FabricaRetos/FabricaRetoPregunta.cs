using preguntaods.BusinessLogic.Retos;

namespace preguntaods.BusinessLogic.FabricaRetos
{
    public class FabricaRetoPregunta : FabricaReto
    {
        public override IReto CrearReto(int orden)
        {
            return new RetoPre(orden);
        }
    }
}