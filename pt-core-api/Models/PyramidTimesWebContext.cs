using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace pt_core_api.Models
{
    public partial class PyramidTimesWebContext : DbContext
    {
        public IConfiguration _configuration { get; }
        public PyramidTimesWebContext()
        {
        }
        public PyramidTimesWebContext(DbContextOptions<PyramidTimesWebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminEvent> AdminEvents { get; set; }
        public virtual DbSet<ContactDatum> ContactData { get; set; }
        public virtual DbSet<EventRegistration> EventRegistrations { get; set; }
        public virtual DbSet<PastEvent> PastEvents { get; set; }
        public virtual DbSet<SpeakerRegistration> SpeakerRegistrations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(_configuration.GetConnectionString("ptWebConnectionString"));
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Data Source=DESKTOP-M9CEA62\\SQLEXPRESS;Initial Catalog=PyramidTimesWeb; Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminEvent>(entity =>
            {
                entity.Property(e => e.CauroselPicture).IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Descrption).IsUnicode(false);

                entity.Property(e => e.EventName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EventTime).HasColumnType("datetime");

                entity.Property(e => e.PromoPicture).IsUnicode(false);

                entity.Property(e => e.PromoVideo)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ContactDatum>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EventRegistration>(entity =>
            {
                entity.ToTable("EventRegistration");

                entity.Property(e => e.Country)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Speaker)
                    .WithMany(p => p.EventRegistrations)
                    .HasForeignKey(d => d.SpeakerId)
                    .HasConstraintName("FK_EventRegistration_SpeakerRegistration");
            });

            modelBuilder.Entity<PastEvent>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PastEventUrl).IsUnicode(false);
            });

            modelBuilder.Entity<SpeakerRegistration>(entity =>
            {
                entity.ToTable("SpeakerRegistration");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OneLineProfile)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProfilePicture);


                //entity.Property(e => e.ProfilePicture).IsRequired();

                entity.Property(e => e.Theme)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Topic)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
