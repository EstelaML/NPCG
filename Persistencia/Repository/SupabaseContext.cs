using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.EntityFrameworkCore;
using preguntaods.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace preguntaods.Persistencia.Repository
{
    public class AppDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("User Id=postgres;Password=vpxJN2KTCEqvpyQ7;Server=db.ilsulfckdfhvgljvvmhb.supabase.co;Port=5432;Database=postgres");

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Entities.ODS> ODSs { get; set; }
        public DbSet<Entities.Reto> Retos { get; set; }
        public DbSet<Entities.RetoPregunta> Preguntas { get; set; }
        public DbSet<Entities.Usuario> Usuarios { get; set; }
        public DbSet<Entities.RetoPorPartida> RetosPorPartidas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de entidades
        }
    }

}