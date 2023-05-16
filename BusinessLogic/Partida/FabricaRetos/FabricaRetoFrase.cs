namespace preguntaods.BusinessLogic.Partida.Retos
{
    public class FabricaRetoFrase : FabricaReto
    {
        public override IReto CrearReto(int orden)
        {
            return new RetoFrase(orden);
        }
    }
}