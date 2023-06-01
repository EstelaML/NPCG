using preguntaods.BusinessLogic.Retos;
using preguntaods.BusinessLogic.Services;
using preguntaods.Entities;
using Supabase.Gotrue;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace preguntaods.BusinessLogic.Fachada
{
    public class Facade
    {
        private static Facade _instance;

        private PreguntadosService servicio;

        private Facade()
        {
            servicio = PreguntadosService.GetInstance();
        }

        public static Facade GetInstance()
        {
            return _instance ??= new Facade();
        }

        #region Usuario

        public async Task LoginAsync(string correo, string password)
        {
            await servicio.LoginAsync(correo, password);
        }

        public async Task LogoutAsync()
        {
            await servicio.LogoutAsync();
        }

        public async Task<User> SignUpAsync(string correo, string password)
        {
            var respuesta = await servicio.SignUpAsync(correo, password);
            return respuesta;
        }

        public async Task<Usuario> GetUsuarioLogged()
        {
            var respuesta = await servicio.GetUsuarioLogged();
            return respuesta;
        }

        public async Task UpdatePuntos(int puntos)
        {
            await servicio.UpdatePuntos(puntos);
        }

        public async Task UpdateVolumenMusica(int volumenMusica)
        {
            await servicio.UpdateVolumenMusica(volumenMusica);
        }

        public async Task UpdateVolumenSonidos(int volumenSonidos)
        {
            await servicio.UpdateVolumenSonidos(volumenSonidos);
        }

        public async Task UpdateVolumenActivado(int[] volumenActivado)
        {
            await servicio.UpdateVolumenActivado(volumenActivado);
        }

        public async Task UpdateNivel(int nivel)
        {
            await servicio.UpdateNivel(nivel);
        }

        public async Task UpdatePartidasGanadas()
        {
            await servicio.UpdatePartidasGanadas();
        }

        public async Task CambiarNombre(string nombre)
        {
            await servicio.CambiarNombre(nombre);
        }

        public async Task CambiarFoto(string uuid, byte[] foto)
        {
            await servicio.CambiarFoto(uuid, foto);
        }

        public async Task NewUsuario(Usuario user)
        {
            await servicio.NewUsuario(user);
        }

        public async Task GuardarPregunta(IReto reto)
        {
            await servicio.GuardarPregunta(reto);
        }

        public async Task GuardarPreguntaFallada(IReto reto)
        {
            await servicio.GuardarPreguntaFallada(reto);
        }

        public async Task<bool> ComprobarUsuario(string nombre)
        {
            var respuesta = await servicio.ComprobarUsuario(nombre);

            return respuesta;
        }

        #endregion Usuario

        #region Estadisticas

        public async Task<List<Estadistica>> GetAllUsersOrdered()
        {
            var listaUsuarios = await servicio.GetAllUsersOrdered();

            return listaUsuarios;
        }

        // lo mismo pero por día
        public async Task<List<Estadistica>> GetAllUsersOrderedByDay()
        {
            var listaUsuarios = await servicio.GetAllUsersOrderedByDay();

            return listaUsuarios;
        }

        // lo mismo pero por semana
        public async Task<List<Estadistica>> GetAllUsersOrderedByWeek()
        {
            var listaUsuarios = await servicio.GetAllUsersOrderedByWeek();

            return listaUsuarios;
        }

        public async Task CrearEstadisticas(Usuario user)
        {
            await servicio.CrearEstadisticas(user);
        }

        public async Task<Estadistica> PedirEstadisticas(string uuid)
        {
            var respuesta = await servicio.PedirEstadisticas(uuid);
            return respuesta;
        }

        public async Task GuardarTiempo()
        {
            await servicio.GuardarTiempo();
        }

        public void PonerPuntuacionDiaria()
        {
            servicio.PonerPuntuacionDiaria();
        }

        public void PonerPuntuacionSemanal()
        {
            servicio.PonerPuntuacionSemanal();
        }

        #endregion Estadisticas

        #region Retos

        public async Task<List<Pregunta>> GetPreguntasByODS(int ods)
        {
            var res = await servicio.GetPreguntasByODS(ods);
            return res;
        }

        public async Task<List<Ahorcado>> GetAhorcadoByODS(int ods)
        {
            var res = await servicio.GetAhorcadoByODS(ods);
            return res;
        }

        #endregion Retos
    }
}