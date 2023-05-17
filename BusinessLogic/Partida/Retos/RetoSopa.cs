using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace preguntaods.Entities
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