using preguntaods.BusinessLogic.Partida.Retos;

namespace preguntaods.BusinessLogic.Partida.FabricaRetos
{
    public class FabricaRetoPregunta : FabricaReto
    {
        public override IReto CrearReto(int orden)
        {
            return new RetoPre(orden);
        }
    }
}