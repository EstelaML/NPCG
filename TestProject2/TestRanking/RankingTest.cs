using preguntaods.BusinessLogic.Partida;
using preguntaods.BusinessLogic.Retos;
using preguntaods.BusinessLogic.Services;
using preguntaods.BusinessLogic;
using preguntaods.Entities;
using Moq;
using preguntaods.Persistencia.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestProject2.TestRanking
{
    [TestClass]
    public class RankingTest
    {
        [TestMethod]
        public async Task TestOrdenRanking()
        {
            // Inicias el servicio
            var service = PreguntadosService.GetInstance(); 

            // Llamas al método
            var users = await service.GetAllUsersOrdered(); // Obtener la lista de usuarios ordenada por puntuación

            // Lo ordenas y compruebas
            var sortedUsers = users.OrderByDescending(user => user.Puntuacion).ToList(); // Ordenar la lista copiada
            CollectionAssert.AreEqual(users, sortedUsers);
        }
    }
}
