using preguntaods.BusinessLogic.Partida;
using preguntaods.BusinessLogic.Retos;
using preguntaods.BusinessLogic.Services;

namespace TestProject2.TestBuilder
{
    [TestClass]
    public class BuilderTestMix
    {
        [TestMethod]
        public async Task TestMethodMix()
        {
            // hacemos login
            PreguntadosService servicio = PreguntadosService.GetInstance();
            await servicio.LoginAsync("estelalinezz@gmail.com", "estela");
            // creamos la partida
            var director = new PartidaDirector();
            var builder = new PartidaBuilder();
            await director.ConstructPartida(builder, 5);
            Partida p = builder.GetPartida();
            var retos = p.GetRetos();
            // comprobamos que al menos hay un reto de cada tipo
            bool pregunta = retos.Any(reto => reto.Type == 100);
            bool ahorcado = retos.Any(reto => reto.Type == 101);
            // comprobamos que ni ahorcado ni pregunta sean null en todos los retos
            bool notNullAhorcado = retos.OfType<RetoAhorcado>().All(reto => reto.GetAhorcado() != null);
            bool notNullPregunta = retos.OfType<RetoPre>().All(reto => reto.GetPregunta() != null);
            Assert.IsTrue(pregunta);
            Assert.IsTrue(ahorcado);
            Assert.IsTrue(notNullPregunta);
            Assert.IsTrue(notNullAhorcado);
        }
    }
}
