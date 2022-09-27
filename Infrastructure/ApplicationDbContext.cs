using Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)                         // configruation for giving us the acces for the yo DbSet
            : base(options)
        {

        }

        public virtual DbSet<RatesUpdate> RatesUpdates { get; set; } = null!;                               //gives us the acces to the entities inside the RatesUpdates class
        public virtual DbSet<Update> Updates { get; set; } = null!;                                         // gives is the acces to the entities inside the Update class


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RatesUpdate>(entity =>
            {
                entity.HasKey(e => e.IdRate);
                entity.HasKey(e => e.IdRate);

                entity.ToTable("Rates_Update");

                entity.Property(e => e.IdRate).HasColumnName("id_rate");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 6)")
                    .HasColumnName("amount");

                entity.Property(e => e.Currency)
                    .HasMaxLength(50)
                    .HasColumnName("currency");

                entity.Property(e => e.IdUpdate).HasColumnName("id_update");

                entity.HasOne(d => d.IdUpdateNavigation)
                    .WithMany(p => p.RatesUpdates)
                    .HasForeignKey(d => d.IdUpdate)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rates_Update_Update");
            });
            modelBuilder.Entity<Update>(entity =>
            {
                entity.HasKey(e => e.IdUpdate);

                entity.ToTable("Update");

                entity.Property(e => e.IdUpdate).HasColumnName("id_update");

                entity.Property(e => e.Base)
                    .HasMaxLength(50)
                    .HasColumnName("base");

                entity.Property(e => e.DateUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("date_update");

                entity.Property(e => e.Timestamp)
                    .HasMaxLength(50)
                    .HasColumnName("timestamp");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

} 



   
