using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APISuperandote.Models
{
    public partial class db_CMSuperandoteContext : DbContext
    {
        public db_CMSuperandoteContext()
        {
        }

        public db_CMSuperandoteContext(DbContextOptions<db_CMSuperandoteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actividad> Actividads { get; set; } = null!;
        public virtual DbSet<Educadore> Educadores { get; set; } = null!;
        public virtual DbSet<Estudiante> Estudiantes { get; set; } = null!;
        public virtual DbSet<EstudianteRealizaActividad> EstudianteRealizaActividads { get; set; } = null!;
        public virtual DbSet<Modificacione> Modificaciones { get; set; } = null!;
        public virtual DbSet<ReportesProgreso> ReportesProgresos { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DAFNE;Database=db_CMSuperandote;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actividad>(entity =>
            {
                entity.ToTable("Actividad");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<Educadore>(entity =>
            {
                entity.HasIndex(e => e.Ci, "UQ__Educador__32149A7A153B978F")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Apellidos).HasMaxLength(50);

                entity.Property(e => e.Ci)
                    .IsRequired()
                    .HasColumnName("CI");

                entity.Property(e => e.Nombres).HasMaxLength(50);
            });

            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.HasIndex(e => e.Ci, "UQ__Estudian__32149A7AAF686B22")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Apellidos).HasMaxLength(50);

                entity.Property(e => e.Ci)
                    .IsRequired()
                    .HasColumnName("CI");

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.GradoAutismo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres).HasMaxLength(50);
            });

            modelBuilder.Entity<EstudianteRealizaActividad>(entity =>
            {
                entity.ToTable("EstudianteRealizaActividad");

                entity.Property(e => e.Ciestudiante).HasColumnName("CIestudiante");

                entity.Property(e => e.FechaActividad)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaActividad");

                entity.Property(e => e.Tiempo).HasMaxLength(50);

                entity.HasOne(d => d.CiestudianteNavigation)
                    .WithMany(p => p.EstudianteRealizaActividads)
                    .HasForeignKey(d => d.Ciestudiante)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Estudiant__CIest__2EDAF651");

                entity.HasOne(d => d.IdActividadNavigation)
                    .WithMany(p => p.EstudianteRealizaActividads)
                    .HasForeignKey(d => d.IdActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Estudiant__IdAct__2DE6D218");
            });

            modelBuilder.Entity<Modificacione>(entity =>
            {
                entity.Property(e => e.CiUsuario).HasColumnName("CI_Usuario");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Modificacion).HasMaxLength(500);

                entity.HasOne(d => d.CiUsuarioNavigation)
                    .WithMany(p => p.Modificaciones)
                    .HasPrincipalKey(p => p.Ci)
                    .HasForeignKey(d => d.CiUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Modificac__CI_Us__3D2915A8");
            });

            modelBuilder.Entity<ReportesProgreso>(entity =>
            {
                entity.ToTable("ReportesProgreso");

                entity.Property(e => e.Cieducador).HasColumnName("CIeducador");

                entity.Property(e => e.Ciestudiante).HasColumnName("CIestudiante");

                entity.Property(e => e.FechaReporte).HasColumnType("date");

                entity.Property(e => e.Observaciones).HasMaxLength(500);

                entity.HasOne(d => d.CieducadorNavigation)
                    .WithMany(p => p.ReportesProgresos)
                    .HasPrincipalKey(p => p.Ci)
                    .HasForeignKey(d => d.Cieducador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ReportesP__CIedu__3864608B");

                entity.HasOne(d => d.CiestudianteNavigation)
                    .WithMany(p => p.ReportesProgresos)
                    .HasPrincipalKey(p => p.Ci)
                    .HasForeignKey(d => d.Ciestudiante)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ReportesP__CIest__395884C4");

                entity.HasOne(d => d.IdActividadNavigation)
                    .WithMany(p => p.ReportesProgresos)
                    .HasForeignKey(d => d.IdActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ReportesP__IdAct__3A4CA8FD");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.NombreRol).HasMaxLength(50);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.HasIndex(e => e.Ci, "UQ__Usuario__32149A7A5AAE9E9D")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Ci).HasColumnName("CI");

                entity.Property(e => e.Contraseña).HasMaxLength(255);

                entity.Property(e => e.IdRol).HasColumnName("ID_Rol");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuario__ID_Rol__3587F3E0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
