using preguntaods.Entities;

namespace preguntaods.BusinessLogic.Partida.FabricaRetos
{
    public class FabricaRetoAhorcado : FabricaReto
    {
        public override IReto CrearReto(int orden)
        {
            return new RetoAhorcado(orden);
        }
    }
}