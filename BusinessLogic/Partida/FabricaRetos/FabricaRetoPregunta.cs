namespace preguntaods.BusinessLogic.Partida.Retos
{
    public class FabricaRetoPregunta : FabricaReto
    {
        public override IReto CrearReto(int orden)
        {
            return new RetoPre(orden);
        }
    }
}