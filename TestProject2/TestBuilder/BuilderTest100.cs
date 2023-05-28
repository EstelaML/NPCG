using preguntaods.BusinessLogic.Partida;
using preguntaods.BusinessLogic.Retos;
using preguntaods.BusinessLogic.Services;

namespace TestProject2.TestBuilder
{
    [TestClass]
    public class BuilderTest100Pregunta
    {
        [TestMethod]
        public async Task TestMethodPregunta()
        {
            // hacemos login
            PreguntadosService servicio = PreguntadosService.GetInstance();
            await servicio.LoginAsync("estelalinezz@gmail.com", "estela");
            // creas la partida
            var director = new PartidaDirector();
            var builder = new PartidaBuilder();
            await director.ConstructPartida(builder, 1);
            Partida p = builder.GetPartida();

            // compruebas que todos son de tipo pregunta (100)
            bool pregunta = p.GetRetos().All(x => x.Type == 100);
            // y que todos tienen una Pregunta asociada que no es null
            bool notNull = p.GetRetos().OfType<RetoPre>().All(x => x.GetPregunta() != null);
            Assert.IsTrue(pregunta);
            Assert.IsTrue(notNull);
        }
    }
}