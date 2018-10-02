using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MonitoringIT.DAL.Models
{
    public partial class MonitoringContext : DbContext
    {
        public MonitoringContext()
        {
        }

        public MonitoringContext(DbContextOptions<MonitoringContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Languages> Languages { get; set; }
        public virtual DbSet<ProfileModels> ProfileModels { get; set; }
        public virtual DbSet<Repositories> Repositories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Monitoring;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Languages>(entity =>
            {
                entity.HasIndex(e => e.RepositoryId)
                    .HasName("IX_Repository_Id");

                entity.Property(e => e.RepositoryId).HasColumnName("Repository_Id");

                entity.HasOne(d => d.Repository)
                    .WithMany(p => p.Languages)
                    .HasForeignKey(d => d.RepositoryId)
                    .HasConstraintName("FK_dbo.Languages_dbo.Repositories_Repository_Id");
            });
        }
    }
}
