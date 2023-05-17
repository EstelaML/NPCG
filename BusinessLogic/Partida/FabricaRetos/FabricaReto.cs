using preguntaods.Entities;

namespace preguntaods.BusinessLogic.Partida.FabricaRetos
{
    public abstract class FabricaReto
    {
        public abstract IReto CrearReto(int orden);
    }
}