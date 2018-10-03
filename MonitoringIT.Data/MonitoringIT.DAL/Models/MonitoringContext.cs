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
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<Profiles> Profiles { get; set; }
        public virtual DbSet<Proxies> Proxies { get; set; }
        public virtual DbSet<Repositories> Repositories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Monitoring;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Languages>(entity =>
            {
                entity.HasIndex(e => e.RepositoryId)
                    .HasName("IX_RepositoryId");

                entity.HasOne(d => d.Repository)
                    .WithMany(p => p.Languages)
                    .HasForeignKey(d => d.RepositoryId)
                    .HasConstraintName("FK_dbo.Languages_dbo.Repositories_RepositoryId");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey });

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Profiles>(entity =>
            {
                entity.Property(e => e.ImageUrl).IsUnicode(false);
            });

            modelBuilder.Entity<Repositories>(entity =>
            {
                entity.HasIndex(e => e.ProfileId)
                    .HasName("IX_ProfileId");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.Repositories)
                    .HasForeignKey(d => d.ProfileId)
                    .HasConstraintName("FK_dbo.Repositories_dbo.Profiles_ProfileId");
            });
        }
    }
}
