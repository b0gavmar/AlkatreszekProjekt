using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlkatreszekProjekt.Models;

public partial class AlkatreszekContext : DbContext
{
    public AlkatreszekContext()
    {
    }

    public AlkatreszekContext(DbContextOptions<AlkatreszekContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alkatreszek> Alkatreszeks { get; set; }

    public virtual DbSet<Beszallitok> Beszallitoks { get; set; }

    public virtual DbSet<Kategoraik> Kategoraiks { get; set; }

    public virtual DbSet<Megrendelesek> Megrendeleseks { get; set; }

    public virtual DbSet<Megrendelok> Megrendeloks { get; set; }

    public virtual DbSet<RendelesTetelek> RendelesTeteleks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=Database\\alkatreszek.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alkatreszek>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("alkatreszek");

            entity.Property(e => e.Ar)
                .HasColumnType("INT")
                .HasColumnName("ar");
            entity.Property(e => e.BeszallitoId)
                .HasColumnType("INT")
                .HasColumnName("beszallito_id");
            entity.Property(e => e.Id)
                .HasColumnType("INT")
                .HasColumnName("id");
            entity.Property(e => e.KategoriaId)
                .HasColumnType("INT")
                .HasColumnName("kategoria_id");
            entity.Property(e => e.LaptopAlkatresz).HasColumnName("laptop_alkatresz");
            entity.Property(e => e.Nev).HasColumnName("nev");
            entity.Property(e => e.Raktaron)
                .HasColumnType("INT")
                .HasColumnName("raktaron");

            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<Beszallitok>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("beszallitok");

            entity.Property(e => e.BeszallitoNev).HasColumnName("beszallito_nev");
            entity.Property(e => e.BeszallitoTelephely).HasColumnName("beszallito_telephely");
            entity.Property(e => e.Id)
                .HasColumnType("INT")
                .HasColumnName("id");

            entity.HasKey(entity => entity.Id);
        });

        modelBuilder.Entity<Kategoraik>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("kategoraik");

            entity.Property(e => e.Id)
                .HasColumnType("INT")
                .HasColumnName("id");
            entity.Property(e => e.KategoriaNev).HasColumnName("kategoria_nev");

            entity.HasKey(entity=> entity.Id);
        });

        modelBuilder.Entity<Megrendelesek>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("megrendelesek");

            entity.Property(e => e.Datum).HasColumnName("datum");
            entity.Property(e => e.Id)
                .HasColumnType("INT")
                .HasColumnName("id");
            entity.Property(e => e.MegrendeloId)
                .HasColumnType("INT")
                .HasColumnName("megrendelo_id");
            entity.Property(e => e.Teljesitve).HasColumnName("teljesitve");

            entity.HasKey(entity=> entity.Id);
        });

        modelBuilder.Entity<Megrendelok>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("megrendelok");

            entity.Property(e => e.Ferfi).HasColumnName("ferfi");
            entity.Property(e => e.Id)
                .HasColumnType("INT")
                .HasColumnName("id");
            entity.Property(e => e.Lakhely).HasColumnName("lakhely");
            entity.Property(e => e.MegrendeloNev).HasColumnName("megrendelo_nev");

            entity.HasKey(entity => entity.Id);
        });

        modelBuilder.Entity<RendelesTetelek>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("rendeles_tetelek");

            entity.Property(e => e.AruId)
                .HasColumnType("INT")
                .HasColumnName("aru_id");
            entity.Property(e => e.AruMennyiseg)
                .HasColumnType("INT")
                .HasColumnName("aru_mennyiseg");
            entity.Property(e => e.MegrendelesId)
                .HasColumnType("INT")
                .HasColumnName("megrendeles_id");

            entity.HasKey(e=>e.Id);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
