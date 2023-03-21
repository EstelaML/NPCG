using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Microsoft.EntityFrameworkCore;
using preguntaods.Entities;
using preguntaods.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace preguntaods.Persistencia
{
    public class UsuarioRepositorio : IRepository<Usuario>
    {
        public readonly DbContext dbContext;
        public UsuarioRepositorio(DbContext dbContext) { 
            this.dbContext= dbContext;
        }
        public async Task Add(Usuario entity)
        {
            await dbContext.Set<Usuario>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await dbContext.Set<Usuario>().ToListAsync();
        }
        public async Task<Usuario> GetById(int id)
        {
            return await dbContext.Set<Usuario>().FindAsync(id);
        }

        public Task Update(Usuario entity)
        {
            throw new NotImplementedException();
        }


        //Especial del usuario
        //public async Task<IEnumerable<Usuario>> GetByUsername(string nombre)
        public Usuario GetByUsername(string nombre)
        {
            Usuario u= dbContext.Set<Usuario>().FirstOrDefault(u => u.nombre == nombre);
            return u;
        }
    }
}