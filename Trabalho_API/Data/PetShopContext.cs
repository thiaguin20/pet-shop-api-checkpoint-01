using Microsoft.EntityFrameworkCore;
using Trabalho_API.Models;

namespace Trabalho_API.Data
{
    public class PetShopContext : DbContext
    {
        public PetShopContext(DbContextOptions<PetShopContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasIndex(cliente => cliente.Email)
                .IsUnique();

            modelBuilder.Entity<Pet>()
                .HasOne(pet => pet.Cliente)
                .WithMany(cliente => cliente.Pets)
                .HasForeignKey(pet => pet.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Agendamento>()
                .HasOne(agendamento => agendamento.Pet)
                .WithMany(pet => pet.Agendamentos)
                .HasForeignKey(agendamento => agendamento.PetId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
