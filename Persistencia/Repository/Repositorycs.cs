using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pruebasEF.Persistencia.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context = null;
        protected DbSet<T> table = null;

        public Repository(DbContext context)
        {
            this._context = context;
            table = _context.Set<T>();
        }

        public async Task<T> Get(int id)
        {
            return table.Find(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return table.ToList();
        }

        public async Task Add(T entity)
        {
            table.Add(entity);
        }

        public async Task Update(T entity)
        {
            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            T entity = table.Find(id);
            table.Remove(entity);
        }
    }
}