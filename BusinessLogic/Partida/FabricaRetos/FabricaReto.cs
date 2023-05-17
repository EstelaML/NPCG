using preguntaods.BusinessLogic.Partida.Retos;

namespace preguntaods.BusinessLogic.Partida.FabricaRetos
{
    public abstract class FabricaReto
    {
        public abstract IReto CrearReto(int orden);
    }
}