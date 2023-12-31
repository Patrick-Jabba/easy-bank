using EasyBank.Api.Data.Mappings;
using EasyBank.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyBank.Api.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}

        public DbSet<Usuario> Usuarios {get; set;}
        public DbSet<NaturezaDeLancamento> NaturezaDeLancamentos {get; set;}
        public DbSet<APagar> APagarContext {get; set;}
        public DbSet<AReceber> AReceberContext {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new NaturezaDeLancamentoMap());
            modelBuilder.ApplyConfiguration(new APagarMap());
            modelBuilder.ApplyConfiguration(new AReceberMap());
        }
    }
}