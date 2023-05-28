namespace preguntaods.BusinessLogic.Retos
{
    public class RetoSopa : IReto
    {
        public RetoSopa(int orden)
        {
            Type = IReto.TypeSopa;
        }

        public Task SetDif(int orden)
        {
            throw new System.NotImplementedException();
        }

        public int Type { get; set; }

        public async Task SetValues()
        {
            await Task.CompletedTask;
        }
    }
}