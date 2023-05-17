using preguntaods.Entities;

namespace preguntaods.BusinessLogic.Partida.FabricaRetos
{
    public class FabricaRetoFrase : FabricaReto
    {
        public override IReto CrearReto(int orden)
        {
            return new RetoFrase(orden);
        }
    }
}