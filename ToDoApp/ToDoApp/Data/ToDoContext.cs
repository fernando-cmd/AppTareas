using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Data
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options)
            : base(options)
        { }

        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura la relación entre Tarea y Usuario
            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.Usuario)
                .WithMany(u => u.Tareas)  // Esto asume que Usuario tiene una lista de Tareas (uno a muchos)
                .HasForeignKey(t => t.UsuarioId);
        }
    }
}
