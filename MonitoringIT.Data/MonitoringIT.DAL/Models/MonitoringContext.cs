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

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<GithubLanguage> GithubLanguage { get; set; }
        public virtual DbSet<GithubLinkedinCrossTable> GithubLinkedinCrossTable { get; set; }
        public virtual DbSet<GithubProfile> GithubProfile { get; set; }
        public virtual DbSet<GithubRepository> GithubRepository { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<LinkedinEducation> LinkedinEducation { get; set; }
        public virtual DbSet<LinkedinExperience> LinkedinExperience { get; set; }
        public virtual DbSet<LinkedinInterest> LinkedinInterest { get; set; }
        public virtual DbSet<LinkedinLanguage> LinkedinLanguage { get; set; }
        public virtual DbSet<LinkedinProfile> LinkedinProfile { get; set; }
        public virtual DbSet<LinkedinSkill> LinkedinSkill { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<Proxy> Proxy { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Monitoring;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");
          

            modelBuilder.Entity<GithubLanguage>(entity =>
            {
                entity.HasIndex(e => e.RepositoryId)
                    .HasName("IX_RepositoryId");

                entity.Property(e => e.Percent).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Repository)
                    .WithMany(p => p.GithubLanguage)
                    .HasForeignKey(d => d.RepositoryId)
                    .HasConstraintName("FK_dbo.Languages_dbo.Repositories_RepositoryId");
            });

            modelBuilder.Entity<GithubProfile>(entity =>
            {
                entity.HasIndex(e => e.UserName)
                    .HasName("UQ__Username")
                    .IsUnique();

                entity.Property(e => e.ImageUrl).IsUnicode(false);

                entity.Property(e => e.LastUpdate).HasColumnType("datetime");

                entity.Property(e => e.UserName).IsRequired();
            });

            modelBuilder.Entity<GithubRepository>(entity =>
            {
                entity.HasIndex(e => e.ProfileId)
                    .HasName("IX_ProfileId");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.GithubRepository)
                    .HasForeignKey(d => d.ProfileId)
                    .HasConstraintName("FK_dbo.Repositories_dbo.Profiles_ProfileId");
            });

           
            modelBuilder.Entity<LinkedinEducation>(entity =>
            {
                entity.HasOne(d => d.LinkedinProfile)
                    .WithMany(p => p.LinkedinEducation)
                    .HasForeignKey(d => d.LinkedinProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LinkedinEducation_LinkedinProfile");
            });

            modelBuilder.Entity<LinkedinExperience>(entity =>
            {
                entity.HasOne(d => d.LinkedinProfile)
                    .WithMany(p => p.LinkedinExperience)
                    .HasForeignKey(d => d.LinkedinProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LinkedinProfile_LinkedinExperience");
            });

            modelBuilder.Entity<LinkedinInterest>(entity =>
            {
                entity.HasOne(d => d.LinkedinProfile)
                    .WithMany(p => p.LinkedinInterest)
                    .HasForeignKey(d => d.LinkedinProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Interest_LinkedinProfile");
            });

            modelBuilder.Entity<LinkedinLanguage>(entity =>
            {
                entity.HasOne(d => d.LinkedinProfile)
                    .WithMany(p => p.LinkedinLanguage)
                    .HasForeignKey(d => d.LinkedinProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LinkedinLanguage_LinkedinProfile");
            });

            modelBuilder.Entity<LinkedinProfile>(entity =>
            {
                entity.HasIndex(e => e.Username)
                    .HasName("UQ__Username")
                    .IsUnique();

                entity.Property(e => e.Connected).HasColumnType("datetime");

                entity.Property(e => e.FullName).IsRequired();

                entity.Property(e => e.LastUpdate).HasColumnType("datetime");

                entity.Property(e => e.Username).IsRequired();
            });

            modelBuilder.Entity<LinkedinSkill>(entity =>
            {
                entity.HasOne(d => d.LinkedinProfile)
                    .WithMany(p => p.LinkedinSkill)
                    .HasForeignKey(d => d.LinkedinProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LinkedinSkill_LinkedinProfile");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Company__737584F638273FA0")
                    .IsUnique();

                entity.Property(e => e.Address).HasMaxLength(350);

                entity.Property(e => e.DateOfFoundation).HasColumnType("date");

                entity.Property(e => e.Facebook)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.GooglePlus)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.Industry).HasMaxLength(150);

                entity.Property(e => e.Linkedin)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Phone)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Twitter)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.Type).HasMaxLength(150);

                entity.Property(e => e.Website)
                    .HasMaxLength(350)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.Property(e => e.AdditionalInformation).HasMaxLength(350);

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(350);

                entity.Property(e => e.Deadline).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(350);

                entity.Property(e => e.Email).HasMaxLength(350);

                entity.Property(e => e.EmploymentTerm)
                    .IsRequired()
                    .HasMaxLength(350);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(350);

                entity.Property(e => e.RequiredQualifications).HasMaxLength(350);

                entity.Property(e => e.Responsibilities)
                    .IsRequired()
                    .HasMaxLength(350);

                entity.Property(e => e.TimeType).HasMaxLength(350);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(350);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Job)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_dbo.Jobs_dbo.Companies_CompanyId");
            });

            modelBuilder.Entity<StaffSkill>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Type).HasMaxLength(10);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.StaffSkill)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StaffSkill_Job");
            });
        }
    }
}
