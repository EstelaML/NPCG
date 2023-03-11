using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.EntityFrameworkCore;
using pruebasEF.Entities;
using Supabase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pruebasEF.Persistencia.Repository
{
    public class UserRepository : Repository<Usuario>, IPersonRepository
    {
        private readonly SupabaseContext _supabaseClient;

        public UserRepository(DbContext context) : base(context)
        {




        }

        public Task<Usuario> Create(Usuario person)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Usuario>> GetPersonsByAge(String nombre)
        {
            return await _context.Set<Usuario>().Where(p => p.nombre == nombre).ToListAsync();
        }

        Task<List<Usuario>> IPersonRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<Usuario> IPersonRepository.Update(Usuario person)
        {
            throw new NotImplementedException();
        }
    }

    internal interface IPersonRepository : IRepository<Usuario>
    {
        Task<List<Usuario>> GetAll();
        Task<Usuario> GetById(int id);
        Task<Usuario> Create(Usuario person);
        Task<Usuario> Update(Usuario person);
        Task Delete(int id);
    }
}