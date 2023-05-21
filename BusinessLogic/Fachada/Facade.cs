using preguntaods.BusinessLogic.Retos;
using preguntaods.BusinessLogic.Services;
using preguntaods.Entities;
using Supabase.Gotrue;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace preguntaods.BusinessLogic.Fachada
{
    public class Facade : IFacade
    {
        private static PreguntadosService _servicio;

        public Facade()
        {
            _servicio = new PreguntadosService();
        }

        #region Usuario

        public async Task LoginAsync(string correo, string password)
        {
            await _servicio.LoginAsync(correo, password);
        }

        public async Task LogoutAsync()
        {
            await _servicio.LogoutAsync();
        }

        public async Task<User> SignUpAsync(string correo, string password)
        {
            var respuesta = await _servicio.SignUpAsync(correo, password);
            return respuesta;
        }

        public async Task<Usuario> GetUsuarioLogged()
        {
            var respuesta = await _servicio.GetUsuarioLogged();
            return respuesta;
        }

        public async Task UpdatePuntos(int puntos)
        {
            await _servicio.UpdatePuntos(puntos);
        }

        public async Task UpdateVolumenMusica(int volumenMusica)
        {
            await _servicio.UpdateVolumenMusica(volumenMusica);
        }

        public async Task UpdateVolumenSonidos(int volumenSonidos)
        {
            await _servicio.UpdateVolumenMusica(volumenSonidos);
        }

        public async Task UpdateNivel(int nivel)
        {
            await _servicio.UpdateNivel(nivel);
        }

        public async Task UpdatePartidasGanadas()
        {
            await _servicio.UpdatePartidasGanadas();
        }

        public async Task CambiarNombre(string nombre)
        {
            await _servicio.CambiarNombre(nombre);
        }

        public async Task CambiarFoto(string uuid, byte[] foto)
        {
            await _servicio.CambiarFoto(uuid, foto);
        }

        public async Task NewUsuario(Usuario user)
        {
            await _servicio.NewUsuario(user);
        }

        public async Task GuardarPregunta(IReto reto)
        {
            await _servicio.GuardarPregunta(reto);
        }

        public async Task GuardarPreguntaFallada(IReto reto)
        {
            await _servicio.GuardarPreguntaFallada(reto);
        }

        public async Task<bool> ComprobarUsuario(string nombre)
        {
            var respuesta = await _servicio.ComprobarUsuario(nombre);

            return respuesta;
        }

        #endregion Usuario

        #region Estadisticas

        public async Task<List<Estadistica>> GetAllUsersOrdered()
        {
            var listaUsuarios = await _servicio.GetAllUsersOrdered();

            return listaUsuarios;
        }

        public async Task CrearEstadisticas(Usuario user)
        {
            await _servicio.CrearEstadisticas(user);
        }

        public async Task<Estadistica> PedirEstadisticas(string uuid)
        {
            var respuesta = await _servicio.PedirEstadisticas(uuid);
            return respuesta;
        }

        public async Task GuardarTiempo()
        {
            await _servicio.GuardarTiempo();
        }

        #endregion Estadisticas
    }
}