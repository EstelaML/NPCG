using preguntaods.BusinessLogic.Partida;
using preguntaods.BusinessLogic.Retos;
using preguntaods.BusinessLogic.Services;
using System.Diagnostics;

namespace TestProject2.TestBuilder
{
    [TestClass]
    public class BuilderTest101Ahorcado
    {
        [TestMethod]
        public async Task TestMethodAhorcado()
        {
            // hacemos login
            PreguntadosService servicio = PreguntadosService.GetInstance();
            await servicio.LoginAsync("estelalinezz@gmail.com", "estela");
            // creas la partida
            var director = new PartidaDirector();
            var builder = new PartidaBuilder();
            await director.ConstructPartida(builder, 2);
            Partida p = builder.GetPartida();
            var retos = p.GetRetos();
            // compruebas que todas sean de tipo ahorcado (101)
            bool ahorcado = retos.All(reto => reto.Type == 101);
            // y compruebas que todos tengan un Ahorcado asociado que no sea null
            bool notNull = retos.OfType<RetoAhorcado>().All(reto => (reto).GetAhorcado() != null);
            Assert.IsTrue(ahorcado);
            Assert.IsTrue(notNull);
        }
    }
}
