namespace preguntaods.BusinessLogic.Partida.Retos
{
    public class FabricaRetoAhorcado : FabricaReto
    {
        public override IReto CrearReto(int orden)
        {
            return new RetoAhorcado(orden);
        }
    }
}