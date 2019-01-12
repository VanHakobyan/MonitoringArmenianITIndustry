using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Database.MonitoringIT.DB.EfCore.Models
{
    public partial class MonitoringContext : DbContext
    {
        public string ConnectionString { get; private set; }
        public MonitoringContext()
        {
        }


        public MonitoringContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public MonitoringContext(DbContextOptions<MonitoringContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GithubLinkedinCrossTable> GithubLinkedinCrossTable { get; set; }
        public virtual DbSet<GithubLanguage> Languages { get; set; }
        public virtual DbSet<LinkedinEducation> LinkedinEducation { get; set; }
        public virtual DbSet<LinkedinExperience> LinkedinExperience { get; set; }
        public virtual DbSet<LinkedinInterest> LinkedinInterest { get; set; }
        public virtual DbSet<LinkedinLanguage> LinkedinLanguage { get; set; }
        public virtual DbSet<LinkedinProfile> LinkedinProfile { get; set; }
        public virtual DbSet<LinkedinSkill> LinkedinSkill { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<GithubProfile> Profiles { get; set; }
        public virtual DbSet<Proxy> Proxies { get; set; }
        public virtual DbSet<GithubRepository> Repositories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Monitoring");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GithubLanguage>(entity =>
            {
                entity.HasIndex(e => e.RepositoryId)
                    .HasName("IX_RepositoryId");

                entity.HasOne(d => d.GithubRepository)
                    .WithMany(p => p.Languages)
                    .HasForeignKey(d => d.RepositoryId)
                    .HasConstraintName("FK_dbo.Languages_dbo.Repositories_RepositoryId");
            });

            modelBuilder.Entity<LinkedinEducation>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Time).IsUnicode(false);

                entity.Property(e => e.Title).IsUnicode(false);

                entity.HasOne(d => d.LinkedinProfile)
                    .WithMany(p => p.LinkedinEducation)
                    .HasForeignKey(d => d.LinkedinProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LinkedinEducation_LinkedinProfile");
            });

            modelBuilder.Entity<LinkedinExperience>(entity =>
            {
                entity.Property(e => e.Company).IsUnicode(false);

                entity.Property(e => e.Location).IsUnicode(false);

                entity.Property(e => e.Time).IsUnicode(false);

                entity.Property(e => e.Title).IsUnicode(false);

                entity.HasOne(d => d.LinkedinProfile)
                    .WithMany(p => p.LinkedinExperience)
                    .HasForeignKey(d => d.LinkedinProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LinkedinProfile_LinkedinExperience");
            });

            modelBuilder.Entity<LinkedinInterest>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.LinkedinProfile)
                    .WithMany(p => p.LinkedinInterest)
                    .HasForeignKey(d => d.LinkedinProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Interest_LinkedinProfile");
            });

            modelBuilder.Entity<LinkedinLanguage>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.LinkedinProfile)
                    .WithMany(p => p.LinkedinLanguage)
                    .HasForeignKey(d => d.LinkedinProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LinkedinLanguage_LinkedinProfile");
            });

            modelBuilder.Entity<LinkedinProfile>(entity =>
            {
                entity.HasIndex(e => e.Username)
                    .HasName("UQ__Linkedin__536C85E415D72EF5")
                    .IsUnique();

                entity.Property(e => e.Birthday).IsUnicode(false);

                entity.Property(e => e.Company).IsUnicode(false);

                entity.Property(e => e.Connected).HasColumnType("datetime");

                entity.Property(e => e.Education).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl).IsUnicode(false);

                entity.Property(e => e.Location).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.Property(e => e.Specialty).IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Website).IsUnicode(false);
            });

            modelBuilder.Entity<LinkedinSkill>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.LinkedinProfile)
                    .WithMany(p => p.LinkedinSkill)
                    .HasForeignKey(d => d.LinkedinProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LinkedinSkill_LinkedinProfile");
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

            modelBuilder.Entity<GithubProfile>(entity =>
            {
                entity.Property(e => e.ImageUrl).IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GithubRepository>(entity =>
            {
                entity.HasIndex(e => e.ProfileId)
                    .HasName("IX_ProfileId");

                entity.HasOne(d => d.GithubProfile)
                    .WithMany(p => p.Repositories)
                    .HasForeignKey(d => d.ProfileId)
                    .HasConstraintName("FK_dbo.Repositories_dbo.Profiles_ProfileId");
            });
        }
    }
}
