using BrCitizenRegistry.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrCitizenRegistry.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Citizen> Citizens => Set<Citizen>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Citizen>(entity =>
        {
            entity.ToTable("citizens");

            entity.HasKey(citizen => citizen.Id);

            entity.Property(citizen => citizen.Id)
                .HasColumnName("id")
                .IsRequired();

            entity.Property(citizen => citizen.FullName)
                .HasColumnName("full_name")
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(citizen => citizen.Cpf)
                .HasColumnName("cpf")
                .HasMaxLength(11)
                .IsRequired();

            entity.Property(citizen => citizen.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            entity.HasIndex(citizen => citizen.Cpf)
                .IsUnique();
        });
    }
}