using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
{
    public class RetoFrase : IReto
    {
        public RetoFrase(int orden)
        {
            Type = IReto.TypeFrase;
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