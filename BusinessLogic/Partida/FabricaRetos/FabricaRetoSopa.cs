namespace preguntaods.BusinessLogic.Partida.Retos
{
    public class FabricaRetoSopa : FabricaReto
    {
        public override IReto CrearReto(int orden)
        {
            return new RetoSopa(orden);
        }
    }
}