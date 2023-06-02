using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UniversidadAPI.DBModels;

namespace UniversidadAPI;

public partial class DBConnection : DbContext
{
    public DBConnection()
    {
    }

    public DBConnection(DbContextOptions<DBConnection> options)
        : base(options)
    {
    }

    public virtual DbSet<U_ALUMNO> U_ALUMNO { get; set; }

    public virtual DbSet<U_CARRERA> U_CARRERA { get; set; }

    public virtual DbSet<U_HISTORIA_ACADEMICA> U_HISTORIA_ACADEMICA { get; set; }

    public virtual DbSet<U_MATERIA> U_MATERIA { get; set; }

    public virtual DbSet<VW_ALUMNO_MATERIAS> VW_ALUMNO_MATERIAS { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("DATA SOURCE=localhost:1521/xe;PASSWORD=matiymilla;USER ID=system;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<U_ALUMNO>(entity =>
        {
            entity.HasKey(e => e.ID_ALUMNO).HasName("U_ALUMNO_PK");

            entity.Property(e => e.ID_ALUMNO).HasPrecision(6);
            entity.Property(e => e.APELLIDO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DNI).HasPrecision(8);
            entity.Property(e => e.EMAIL)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.NOMBRE)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<U_CARRERA>(entity =>
        {
            entity.HasKey(e => e.ID_CARRERA).HasName("U_CARRERA_PK");

            entity.Property(e => e.ID_CARRERA).HasPrecision(6);
            entity.Property(e => e.DURACION).HasPrecision(2);
            entity.Property(e => e.NIVEL)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NOMBRE_CARRERA)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.TITULO)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<U_HISTORIA_ACADEMICA>(entity =>
        {
            entity.HasKey(e => e.ID_HISTORIA).HasName("U_HISTORIA_ACADEMICA_PK");

            entity.Property(e => e.ID_HISTORIA).HasPrecision(6);
            entity.Property(e => e.ESTADO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FECHA_FIN).HasColumnType("DATE");
            entity.Property(e => e.FECHA_INICIO).HasColumnType("DATE");
            entity.Property(e => e.ID_ALUMNO).HasPrecision(6);
            entity.Property(e => e.ID_CARRERA).HasPrecision(6);
            entity.Property(e => e.ID_MATERIA).HasPrecision(6);
            entity.Property(e => e.NOTA).HasPrecision(2);

            entity.HasOne(d => d.ALUMNO).WithMany(p => p.U_HISTORIA_ACADEMICA)
                .HasForeignKey(d => d.ID_ALUMNO)
                .HasConstraintName("U_HISTORIA_ACADEMICA_FK1");

            entity.HasOne(d => d.CARRERA).WithMany(p => p.U_HISTORIA_ACADEMICA)
                .HasForeignKey(d => d.ID_CARRERA)
                .HasConstraintName("U_HISTORIA_ACADEMICA_FK3");

            entity.HasOne(d => d.MATERIA).WithMany(p => p.U_HISTORIA_ACADEMICA)
                .HasForeignKey(d => d.ID_MATERIA)
                .HasConstraintName("U_HISTORIA_ACADEMICA_FK2");
        });

        modelBuilder.Entity<U_MATERIA>(entity =>
        {
            entity.HasKey(e => e.ID_MATERIA).HasName("U_MATERIA_PK");

            entity.Property(e => e.ID_MATERIA).HasPrecision(6);
            entity.Property(e => e.ID_CARRERA).HasPrecision(6);
            entity.Property(e => e.NOMBRE_MATERIA)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.CARRERA).WithMany(p => p.U_MATERIA)
                .HasForeignKey(d => d.ID_CARRERA)
                .HasConstraintName("U_MATERIA_FK1");
        });

        modelBuilder.Entity<VW_ALUMNO_MATERIAS>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_ALUMNO_MATERIAS");

            entity.Property(e => e.APELLIDO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CANTIDAD_MATERIAS).HasColumnType("NUMBER");
            entity.Property(e => e.DURACION).HasPrecision(2);
            entity.Property(e => e.MATERIAS_APROBADAS).HasColumnType("NUMBER");
            entity.Property(e => e.NOMBRE)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NOMBRE_CARRERA)
                .HasMaxLength(40)
                .IsUnicode(false);
        });
        modelBuilder.HasSequence("DEPARTMENTS_SEQ").IncrementsBy(10);
        modelBuilder.HasSequence("EMPLOYEES_SEQ");
        modelBuilder.HasSequence("LOCATIONS_SEQ").IncrementsBy(100);
        modelBuilder.HasSequence("LOGMNR_DIDS$");
        modelBuilder.HasSequence("LOGMNR_EVOLVE_SEQ$");
        modelBuilder.HasSequence("LOGMNR_SEQ$");
        modelBuilder.HasSequence("LOGMNR_UIDS$").IsCyclic();
        modelBuilder.HasSequence("MVIEW$_ADVSEQ_GENERIC");
        modelBuilder.HasSequence("MVIEW$_ADVSEQ_ID");
        modelBuilder.HasSequence("ROLLING_EVENT_SEQ$");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
