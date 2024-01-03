using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TareasMVC.Entidades;

namespace TareasMVC
{
    // Aqui se pueden poner todas las configuracion relacionadas a la BD, si se quieren hacer querys contra la BD, hay que usar la clase ApplicationDbContext. Pienza central de EF Core
    public class ApplicationDbContext : IdentityDbContext // IdentityDbContext: Ya trae unas tablas por defecto para usarios, roles
    {
        // DbContextOptions: Configuraciones que podemos pasar al DBContext como el motor de BD a usar, el connectionString, etc. Se pasan a traves del parametro options. Llama a a la clase base y le pasa las opciones
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        // API Fluenty
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Tarea>().Property(d => d.Descripcion).HasMaxLength(500).IsRequired(); // Hace lo mismo que usar DataAnnotations en el modelo
        }


        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Paso> Pasos { get; set; }
        public DbSet<ArchivoAdjunto> ArchivosAdjuntos { get; set; }
    }
}
