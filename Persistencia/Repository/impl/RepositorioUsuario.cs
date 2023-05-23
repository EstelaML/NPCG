using preguntaods.Entities;
using System;
using System.Threading.Tasks;

namespace preguntaods.Persistencia.Repository.impl
{
    public class RepositorioUsuario : Repository<Usuario>
    {
        private ConexionBD conexion;

        public RepositorioUsuario()
        {
            conexion = ConexionBD.GetInstance();
        }

        public async Task<Usuario> GetUserByUUid(string uuid)
        {
            var response = await conexion.Cliente
                .From<Usuario>()
                .Where(x => x.Uuid == uuid)
                .Single();
            return response;
        }

        public async Task<Usuario> GetUserByName(string nombre)
        {
            var response = await conexion.Cliente
                .From<Usuario>()
                .Where(x => x.Nombre == nombre)
                .Single();
            return response;
        }

        public async Task UpdatePuntosUsuario(string uuid, int puntosA, int puntosS, int puntosDiarios)
        {
            var p = puntosA + puntosS;
            await conexion.Cliente
                        .From<Estadistica>()
                        .Where(x => x.Usuario == uuid)
                        .Set(x => x.Puntuacion, p)
                        .Update();

            var puntosD = puntosDiarios+puntosS;
            await conexion.Cliente.From<Estadistica>().Where(x => x.Usuario == uuid).Set(x => x.PuntuacionDiaria, puntosD).Update();
        }

        public async Task UpdatePartidasGanadas(string uuid, int partidasA)
        {
            var p = partidasA + 1;
            await conexion.Cliente
                        .From<Estadistica>()
                        .Where(x => x.Usuario == uuid)
                        .Set(x => x.PartidasGanadas, p)
                        .Update();
        }

        public async Task UpdatePreguntaAcertada(string a, int[] preguntas, Usuario usuario)
        {
            preguntas ??= Array.Empty<int>();
            if (usuario.Id != null)
            {
                var id = (int)usuario.Id;
                await conexion.Cliente
                    .From<RetosRealizados>()
                    .Where(x => x.Usuario == id)
                    .Set(x => x.PreguntasRealizadas, preguntas)
                    .Update();
            }
        }

        public async Task UpdateAhorcadoAcertado(string a, int[] preguntas, Usuario usuario)
        {
            preguntas ??= Array.Empty<int>();
            if (usuario.Id != null)
            {
                var id = (int)usuario.Id;
                await conexion.Cliente
                    .From<RetosRealizados>()
                    .Where(x => x.Usuario == id)
                    .Set(x => x.AhorcadosRealizados, preguntas)
                    .Update();
            }
        }

        public async Task UpdateRetoAcertado(string a, int[] preguntas, Usuario usuario)
        {
            preguntas ??= Array.Empty<int>();
            if (usuario.Id != null)
            {
                var uuid = usuario.Uuid;
                await conexion.Cliente
                    .From<Estadistica>()
                    .Where(x => x.Usuario == uuid)
                    .Set(x => x.Aciertos, preguntas)
                    .Update();
            }
        }

        public async Task UpdateRetoFallado(int[] preguntas, Usuario usuario)
        {
            preguntas ??= Array.Empty<int>();
            if (usuario.Id != null)
            {
                var uuid = usuario.Uuid;
                await conexion.Cliente
                    .From<Estadistica>()
                    .Where(x => x.Usuario == uuid)
                    .Set(x => x.Fallos, preguntas)
                    .Update();
            }
        }

        public async Task UpdateNombre(string uuid, string newNombre)
        {
            await conexion.Cliente
                .From<Usuario>()
                .Where(x => x.Uuid == uuid)
                .Set(x => x.Nombre, newNombre)
                .Update();

            await conexion.Cliente
                .From<Estadistica>()
                .Where(x => x.Usuario == uuid)
                .Set(x => x.Nombre, newNombre)
                .Update();
        }

        public async Task UpdateFoto(string uuid, byte[] foto)
        {
            var fotoT = Convert.ToBase64String(foto);
            await conexion.Cliente
               .From<Usuario>()
               .Where(x => x.Uuid == uuid)
               .Set(x => x.Foto, fotoT)
               .Update();
        }

        public async Task UpdateNivel(string uuid, int nivel)
        {
            var nivelS = nivel + 1;
            await conexion.Cliente
                .From<Usuario>()
                .Where(x => x.Uuid == uuid)
                .Set(x => x.Nivel, nivelS)
                .Update();
        }

        public async Task UpdateVolumenMusica(string uuid, int volumenMusica)
        {
            await conexion.Cliente
                .From<Usuario>()
                .Where(x => x.Uuid == uuid)
                .Set(x => x.Musica, volumenMusica)
                .Update();
        }

        public async Task UpdateVolumenSonidos(string uuid, int volumenSonidos)
        {
            await conexion.Cliente
                .From<Usuario>()
                .Where(x => x.Uuid == uuid)
                .Set(x => x.Sonidos, volumenSonidos)
                .Update();
        }

        public async Task<int[]> GetPreguntasAcertadasAsync(Usuario usuario)
        {
            if (usuario.Id == null) return null;
            var id = (int)usuario.Id;
            var respuesta = await conexion.Cliente.From<RetosRealizados>().Where(x => x.Usuario == id).Single();
            if (respuesta != null)
                return respuesta.PreguntasRealizadas;

            var inser = new RetosRealizados((int)usuario.Id, null, null);
            await conexion.Cliente.From<RetosRealizados>().Insert(inser);
            return null;
        }

        public async Task<int[]> GetAhorcadosAcertadosAsync(Usuario usuario)
        {
            if (usuario.Id == null) return null;
            var id = (int)usuario.Id;
            var respuesta = await conexion.Cliente.From<RetosRealizados>().Where(x => x.Usuario == id).Single();
            if (respuesta != null)
                return respuesta.AhorcadosRealizados;

            var inser = new RetosRealizados((int)usuario.Id, null, null);
            await conexion.Cliente.From<RetosRealizados>().Insert(inser);
            return null;
        }

        public async Task<int[]> GetRetosAcertadosAsync(Usuario usuario)
        {
            if (usuario.Id == null) return null;
            var uuid = usuario.Uuid;
            var respuesta = await conexion.Cliente.From<Estadistica>().Where(x => x.Usuario == uuid).Single();

            return respuesta?.Aciertos;
        }

        public async Task<int[]> GetRetosFalladosAsync(Usuario usuario)
        {
            if (usuario.Id == null) return null;
            var uuid = usuario.Uuid;
            var respuesta = await conexion.Cliente.From<Estadistica>().Where(x => x.Usuario == uuid).Single();

            return respuesta?.Fallos;
        }

        public async Task<Estadistica> GetEstadisticasByUuid(string uuid)
        {
            var response = await conexion.Cliente
                .From<Estadistica>()
                .Where(x => x.Usuario == uuid)
                .Single();
            return response;
        }

        public async Task UpdateTimeUsedAsync(TimeSpan tnew)
        {
            var horas = tnew.TotalSeconds / 3600;
            var id = conexion.UsuarioBD.Id;
            var estadisticas = await GetEstadisticasByUuid(id);
            var told = estadisticas.Tiempo;

            float sumatorio;
            if (told == null)
            {
                sumatorio = (float)horas;
            }
            else
            {
                sumatorio = (float)horas + (float)told;
            }
            await conexion.Cliente
                .From<Estadistica>()
                .Where(x => x.Usuario == id)
                .Set(x => x.Tiempo, sumatorio)
               .Update();
        }
    }
}