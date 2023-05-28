using preguntaods.BusinessLogic.Retos;
using preguntaods.Entities;
using preguntaods.Persistencia;
using preguntaods.Persistencia.Repository.impl;
using Supabase.Gotrue;
using System.Globalization;

namespace preguntaods.BusinessLogic.Services
{
    public class PreguntadosService
    {
        private static PreguntadosService _instance;

        #region atributos
        private readonly object sync = new object();

        private readonly ConexionBD conexion;
        private readonly RepositorioPregunta repositorioPregunta;
        private readonly RepositorioAhorcado repositorioAhorcado;
        private readonly Repository<Estadistica> repositorioEstadisticas;
        private readonly RepositorioUsuario repositorioUser;

        private List<Pregunta> preguntasBajas;
        private List<Pregunta> preguntasMedias;
        private List<Pregunta> preguntasAltas;

        private List<Ahorcado> ahorcadoBajo;
        private List<Ahorcado> ahorcadoMedio;
        private List<Ahorcado> ahorcadoAlto;

        public int volumenMusica;
        public int volumenSonidos;
        #endregion
        private PreguntadosService()
        {
            conexion = ConexionBD.GetInstance();
            repositorioPregunta = new RepositorioPregunta();
            repositorioAhorcado = new RepositorioAhorcado();
            repositorioEstadisticas = new Repository<Estadistica>();
            repositorioUser = new RepositorioUsuario();

            volumenMusica = conexion.UsuarioApp?.Musica ?? 1;
            volumenSonidos = conexion.UsuarioApp?.Sonidos ?? 1;
        }
        public static PreguntadosService GetInstance()
        {
            return _instance ??= new PreguntadosService();
        }

        #region RetoPregunta

        public async Task InitPreguntaList()
        {
            preguntasBajas ??= await repositorioPregunta.GetPreguntasByDificultad(Pregunta.DifBaja);
            preguntasMedias ??= await repositorioPregunta.GetPreguntasByDificultad(Pregunta.DifMedia);
            var p = await repositorioPregunta.GetPreguntasByDificultad(Pregunta.DifAlta);
            lock (sync)
            {
                preguntasAltas ??= p;
            }
        }
    
        public Task<Pregunta> SolicitarPregunta(int dificultad)
        {
            Pregunta respuesta = null;

            switch (dificultad)
            {
                case Pregunta.DifBaja:
                    {
                        var rnd = new Random();
                        var indiceAleatorio = rnd.NextInt64(preguntasBajas.Count);
                        respuesta = preguntasBajas[(int)indiceAleatorio];
                        preguntasBajas.Remove(respuesta);
                        break;
                    }
                case Pregunta.DifMedia:
                    {
                        var rnd = new Random();
                        var indiceAleatorio = rnd.NextInt64(preguntasMedias.Count);
                        respuesta = preguntasMedias[(int)indiceAleatorio];
                        preguntasMedias.Remove(respuesta);

                        break;
                    }
                case Pregunta.DifAlta:
                    {
                        var rnd = new Random();
                        var indiceAleatorio = rnd.NextInt64(preguntasAltas.Count);
                        respuesta = preguntasAltas[(int)indiceAleatorio];
                        preguntasAltas.Remove(respuesta);
                        break;
                    }
            }

            return Task.FromResult(respuesta);
        }

        #endregion RetoPregunta

        #region RetoAhorcado

        public async Task InitAhorcadoList()
        {
            ahorcadoBajo ??= await repositorioAhorcado.GetAhorcadoDificultad(Ahorcado.DifBaja);
            ahorcadoMedio ??= await repositorioAhorcado.GetAhorcadoDificultad(Ahorcado.DifMedia);
            var p = await repositorioAhorcado.GetAhorcadoDificultad(Ahorcado.DifAlta);
            lock (sync)
            {
                ahorcadoAlto ??= p;
            }
        }

        public Task<Ahorcado> SolicitarAhorcado(int dif)
        {
            Ahorcado ahorca = null;

            switch (dif)
            {
                case Ahorcado.DifBaja:
                    {
                        var rnd = new Random();
                        var indiceAleatorio = rnd.NextInt64(ahorcadoBajo.Count);
                        ahorca = ahorcadoBajo[(int)indiceAleatorio];
                        ahorcadoBajo.Remove(ahorca);
                        break;
                    }
                case Ahorcado.DifMedia:
                    {
                        var rnd = new Random();
                        var indiceAleatorio = rnd.NextInt64(ahorcadoMedio.Count);
                        ahorca = ahorcadoMedio[(int)indiceAleatorio];
                        ahorcadoMedio.Remove(ahorca);

                        break;
                    }
                case Ahorcado.DifAlta:
                    {
                        var rnd = new Random();
                        var indiceAleatorio = rnd.NextInt64(ahorcadoAlto.Count);
                        ahorca = ahorcadoAlto[(int)indiceAleatorio];
                        ahorcadoAlto.Remove(ahorca);
                        break;
                    }
            }

            return Task.FromResult(ahorca);
        }

        public async Task<List<Pregunta>> GetPreguntasByODS(int ods)
        {

            var res = await repositorioPregunta.GetPreguntasByODS(ods);
            return res;

        }

        public async Task<List<Ahorcado>> GetAhorcadoByODS(int ods)
        {

            var res = await repositorioAhorcado.GetAhorcadosByODS(ods);
            return res;

        }

        #endregion RetoAhorcado

        #region Estadisticas

        public async Task<List<Estadistica>> GetAllUsersOrdered()
        {
            var respuesta = await repositorioEstadisticas.GetAll();
            var listaUsuarios = respuesta.Select(estadisticas => new Estadistica { Nombre = estadisticas.Nombre, Puntuacion = estadisticas.Puntuacion })
                .OrderByDescending(estadisticas => estadisticas.Puntuacion)
                .ToList();

            return listaUsuarios;
        }

        public async Task<List<Estadistica>> GetAllUsersOrderedByDay()
        {
            var respuesta = await repositorioEstadisticas.GetAll();
            var listaUsuarios = respuesta.Select(estadisticas => new Estadistica { Nombre = estadisticas.Nombre, PuntuacionDiaria = estadisticas.PuntuacionDiaria })
                .OrderByDescending(estadisticas => estadisticas.PuntuacionDiaria)
                .ToList();

            return listaUsuarios;
        }

        public async Task<List<Estadistica>> GetAllUsersOrderedByWeek()
        {
            var respuesta = await repositorioEstadisticas.GetAll();
            var listaUsuarios = respuesta.Select(estadisticas => new Estadistica { Nombre = estadisticas.Nombre, PuntuacionSemanal = estadisticas.PuntuacionSemanal })
                .OrderByDescending(estadisticas => estadisticas.PuntuacionSemanal)
                .ToList();

            return listaUsuarios;
        }

        public async void PonerPuntuacionDiaria()
        {
            // coges todas las estadisticas de todos los usuarios
            var respuesta = await repositorioEstadisticas.GetAll();

            // pones a 0 aquellas que no sean de hoy
            foreach (Estadistica estadistica in respuesta)
            {
                // compruebas si la fecha es diferente a la actual
                if (estadistica.FechaDiaria == null || ((DateTime)estadistica.FechaDiaria).Date != DateTime.Now.Date)
                {
                    // ponemos la PuntuacionDiaria en 0
                    estadistica.PuntuacionDiaria = 0;
                    estadistica.FechaDiaria = DateTime.Now;
                    // lo guardamos
                    await repositorioEstadisticas.Update(estadistica);
                }
            }
        }

        public async void PonerPuntuacionSemanal()
        {
            // coges todas las estadisticas de todos los usuarios
            var respuesta = await repositorioEstadisticas.GetAll();

            // pones a 0 aquellas que no sean de hoy
            foreach (Estadistica estadistica in respuesta)
            {
                CultureInfo culture = CultureInfo.CurrentCulture; // Cultura actual del sistema
                Calendar calendar = culture.Calendar; // Calendario de la cultura actual
                int numeroSemana = calendar.GetWeekOfYear(((DateTime)estadistica.FechaDiaria), culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);
                int numeroSemanaActual = calendar.GetWeekOfYear(DateTime.Now, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);

                // compruebas si la fecha es diferente a la actual
                if (estadistica.FechaDiaria == null || numeroSemana != numeroSemanaActual)
                {
                    // ponemos la PuntuacionDiaria en 0
                    estadistica.PuntuacionSemanal = 0;
                    estadistica.FechaDiaria = DateTime.Now;
                    // lo guardamos
                    await repositorioEstadisticas.Update(estadistica);
                }
            }
        }

        public async Task CrearEstadisticas(Usuario user)
        {
            var aux = Array.Empty<int>();
            var a = new Estadistica(user.Uuid, 0, aux, aux, user.Nombre, 0, DateTime.Now, 0);
            await repositorioEstadisticas.Add(a);
        }

        public async Task<Estadistica> PedirEstadisticas(string uuid)
        {
            var respuesto = await repositorioUser.GetEstadisticasByUuid(uuid);
            return respuesto;
        }

        public async Task GuardarTiempo()
        {
            // calculas el tiempo que lleva esta vez
            var s = conexion.Cliente.Auth.CurrentSession;
            if (s != null)
            {
                var created = s.CreatedAt;
                var start = created.TimeOfDay;
                var now = DateTime.Now.TimeOfDay;
                var dif = now - start;
                await repositorioUser.UpdateTimeUsedAsync(dif);
            }
        }

        public async Task UpdatePartidasGanadas()
        {
            if (conexion.UsuarioBD != null)
            {
                var a = conexion.UsuarioBD.Id;
                var estadisticas = await repositorioUser.GetEstadisticasByUuid(a);
                await repositorioUser.UpdatePartidasGanadas(a, estadisticas.PartidasGanadas);
            }
        }

        #endregion Estadisticas

        #region Usuario

        public async Task LoginAsync(string correo, string password)
        {
            var session = await conexion.Cliente.Auth.SignIn(correo, password);
            conexion.UsuarioBD = session?.User;
            conexion.UsuarioApp = await repositorioUser.GetUserByUUid(session?.User?.Id);

            volumenMusica = conexion.UsuarioApp.Musica;
            volumenSonidos = conexion.UsuarioApp.Sonidos;
        }

        public async Task LogoutAsync()
        {
            await GuardarTiempo();
            await conexion.Cliente.Auth.SignOut();
        }

        public async Task<User> SignUpAsync(string correo, string password)
        {
            var session = await conexion.Cliente.Auth.SignUp(correo, password);
            if (session?.User?.AppMetadata == null) return null;
            conexion.UsuarioBD = session.User;
            return session.User;
        }

        public async Task<Usuario> GetUsuarioLogged()
        {
            if (conexion.UsuarioBD == null) return null;

            var a = conexion.UsuarioBD.Id;
            var respuesta = await repositorioUser.GetUserByUUid(a);
            return respuesta;
        }

        public async Task UpdatePuntos(int puntos)
        {
            if (conexion.UsuarioBD != null)
            {
                var a = conexion.UsuarioBD.Id;
                var estadisticas = await repositorioUser.GetEstadisticasByUuid(a);
                await repositorioUser.UpdatePuntosUsuario(a, estadisticas.Puntuacion, puntos, estadisticas.PuntuacionDiaria, estadisticas.PuntuacionSemanal);
            }
        }

        public async Task UpdateNivel(int nivel)
        {
            if (conexion.UsuarioBD != null)
            {
                var a = conexion.UsuarioBD.Id;
                await repositorioUser.UpdateNivel(a, nivel);
            }
        }

        public async Task UpdateVolumenMusica(int volumenMusica)
        {
            var a = conexion.UsuarioBD.Id;
            this.volumenMusica = volumenMusica;
            await repositorioUser.UpdateVolumenMusica(a, volumenMusica);
        }

        public async Task UpdateVolumenSonidos(int volumenSonidos)
        {
            var a = conexion.UsuarioBD.Id;
            this.volumenSonidos = volumenSonidos;
            await repositorioUser.UpdateVolumenSonidos(a, volumenSonidos);
        }

        public async Task UpdateVolumenActivado(int[] volumenActivado)
        {
            var a = conexion.UsuarioBD.Id;
            await repositorioUser.UpdateVolumenActivado(a, volumenActivado);
        }

        public async Task CambiarNombre(string nombre)
        {
            var a = conexion.UsuarioBD.Id;
            await repositorioUser.UpdateNombre(a, nombre);
        }

        public async Task CambiarFoto(string uuid, byte[] foto)
        {
            await repositorioUser.UpdateFoto(uuid, foto);
        }

        public async Task NewUsuario(Usuario user)
        {
            await repositorioUser.Add(user);
        }

        public async Task GuardarPregunta(IReto reto)
        {
            switch (reto.Type)
            {
                // añado a la BD ese reto
                case IReto.TypePregunta:
                    await repositorioPregunta.AñadirPreguntaRealizada(((RetoPre)reto).GetPregunta());
                    break;

                case IReto.TypeAhorcado:
                    await repositorioAhorcado.AñadirAhorcadoRealizado(((RetoAhorcado)reto).GetAhorcado());
                    break;
            }
        }

        public async Task GuardarPreguntaFallada(IReto reto)
        {
            switch (reto.Type)
            {
                // añado a la BD ese reto
                case IReto.TypePregunta:
                    await repositorioPregunta.AñadirPreguntaFallada(((RetoPre)reto).GetPregunta());
                    break;

                case IReto.TypeAhorcado:
                    await repositorioAhorcado.AñadirAhorcadoFallado(((RetoAhorcado)reto).GetAhorcado());
                    break;
            }
        }

        public async Task<bool> ComprobarUsuario(string nombre)
        {
            var respuesta = await repositorioUser.GetAll();

            return respuesta.All(u => !u.Nombre.Equals(nombre));
        }

        #endregion Usuario


    }
}