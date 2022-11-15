using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MVCMascota.Models
{
    public partial class MVCMascotasContext : DbContext
    {
        public MVCMascotasContext()
        {
        }

        public MVCMascotasContext(DbContextOptions<MVCMascotasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dueno> Duenos { get; set; } = null!;
        public virtual DbSet<Historia> Historias { get; set; } = null!;
        public virtual DbSet<Mascota> Mascotas { get; set; } = null!;
        public virtual DbSet<Medico> Medicos { get; set; } = null!;
        public virtual DbSet<Visita> Visitas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//                optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB; database=MVCMascotas; integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dueno>(entity =>
            {
                entity.Property(e => e.DuenoId).HasColumnName("DuenoID");

                entity.Property(e => e.Apellidos).HasMaxLength(50);

                entity.Property(e => e.Correo).HasMaxLength(65);

                entity.Property(e => e.Direccion).HasMaxLength(80);

                entity.Property(e => e.Nombres).HasMaxLength(50);

                entity.Property(e => e.Telefono).HasMaxLength(21);
            });

            modelBuilder.Entity<Historia>(entity =>
            {
                entity.HasIndex(e => e.MascotaId, "IX_Historias_MascotaID");

                entity.Property(e => e.HistoriaId).HasColumnName("HistoriaID");

                entity.Property(e => e.Diagnostico).HasMaxLength(60);

                entity.Property(e => e.MascotaId).HasColumnName("MascotaID");

                entity.Property(e => e.Medicamentos).HasMaxLength(200);

                entity.HasOne(d => d.Mascota)
                    .WithMany(p => p.Historia)
                    .HasForeignKey(d => d.MascotaId);
            });

            modelBuilder.Entity<Mascota>(entity =>
            {
                entity.HasIndex(e => e.DuenoId, "IX_Mascotas_DuenoID");

                entity.HasIndex(e => e.MedicoId, "IX_Mascotas_MedicoID");

                entity.Property(e => e.MascotaId).HasColumnName("MascotaID");

                entity.Property(e => e.Color).HasMaxLength(50);

                entity.Property(e => e.DuenoId).HasColumnName("DuenoID");

                entity.Property(e => e.Especie).HasMaxLength(50);

                entity.Property(e => e.MedicoId).HasColumnName("MedicoID");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Raza).HasMaxLength(50);

                entity.HasOne(d => d.Dueno)
                    .WithMany(p => p.Mascota)
                    .HasForeignKey(d => d.DuenoId);

                entity.HasOne(d => d.Medico)
                    .WithMany(p => p.Mascota)
                    .HasForeignKey(d => d.MedicoId);
            });

            modelBuilder.Entity<Medico>(entity =>
            {
                entity.Property(e => e.MedicoId).HasColumnName("MedicoID");

                entity.Property(e => e.Apellidos).HasMaxLength(50);

                entity.Property(e => e.Direccion).HasMaxLength(80);

                entity.Property(e => e.Nombres).HasMaxLength(50);

                entity.Property(e => e.Tarjeta).HasMaxLength(10);

                entity.Property(e => e.Telefono).HasMaxLength(21);
            });

            modelBuilder.Entity<Visita>(entity =>
            {
                entity.HasIndex(e => e.HistoriaId, "IX_Visitas_HistoriaID");

                entity.HasIndex(e => e.MedicoId, "IX_Visitas_MedicoID");

                entity.Property(e => e.VisitaId).HasColumnName("VisitaID");

                entity.Property(e => e.HistoriaId).HasColumnName("HistoriaID");

                entity.Property(e => e.MedicoId).HasColumnName("MedicoID");

                entity.Property(e => e.Recomendacion).HasMaxLength(100);

                entity.HasOne(d => d.Historia)
                    .WithMany(p => p.Visita)
                    .HasForeignKey(d => d.HistoriaId);

                entity.HasOne(d => d.Medico)
                    .WithMany(p => p.Visita)
                    .HasForeignKey(d => d.MedicoId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
