using preguntaods.BusinessLogic.Retos;

namespace preguntaods.BusinessLogic.FabricaRetos
{
    public abstract class FabricaReto
    {
        public abstract IReto CrearReto(int orden);
    }
}