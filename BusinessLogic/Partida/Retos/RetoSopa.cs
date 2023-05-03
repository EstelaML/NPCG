using System.Threading.Tasks;

namespace preguntaods.BusinessLogic.Partida.Retos
{
    public class RetoSopa : Reto
    {
        public RetoSopa()
        {
            SetType(TypeSopa);
        }

        public override async Task SetValues()
        {
            await Task.CompletedTask;
        }
    }
}