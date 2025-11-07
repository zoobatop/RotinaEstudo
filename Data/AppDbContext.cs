using Microsoft.EntityFrameworkCore;
using RotinaEstudo.Models;

namespace RotinaEstudo.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<TarefaEstudo> TarefasEstudo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração usando Fluent API tradicional
            modelBuilder.Entity<TarefaEstudo>(entity =>
            {

                // Chave primária
                entity.HasKey(e => e.Id);

                // Propriedades
                entity.Property(e => e.Materia)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Tema)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.TempoEstudoMinutos)
                    .IsRequired();

                entity.Property(e => e.Prioridade)
                    .IsRequired()
                    .HasMaxLength(20);

            });
        }
    }
}