using Microsoft.EntityFrameworkCore;
using petShop.Models;
using System.Collections.Generic;

namespace petShop.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<FichaMedica> FichasMedicas { get; set; }
        public DbSet<Vacuna> Vacunas { get; set; }
    }
}