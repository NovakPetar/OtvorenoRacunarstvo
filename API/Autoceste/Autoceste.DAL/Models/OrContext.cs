using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Autoceste.DAL.Models;

public partial class OrContext : DbContext
{
    public OrContext()
    {
    }

    public OrContext(DbContextOptions<OrContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autoceste> Autocestes { get; set; }

    public virtual DbSet<Naplatnepostaje> Naplatnepostajes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autoceste>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("autoceste_pkey");

            entity.ToTable("autoceste");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Dionica)
                .HasMaxLength(255)
                .HasColumnName("dionica");
            entity.Property(e => e.Duljina).HasColumnName("duljina");
            entity.Property(e => e.Neformalninaziv)
                .HasMaxLength(255)
                .HasColumnName("neformalninaziv");
            entity.Property(e => e.Oznaka)
                .HasMaxLength(255)
                .HasColumnName("oznaka");
        });

        modelBuilder.Entity<Naplatnepostaje>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("naplatnepostaje_pkey");

            entity.ToTable("naplatnepostaje");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Autocestaid).HasColumnName("autocestaid");
            entity.Property(e => e.Geoduzina).HasColumnName("geoduzina");
            entity.Property(e => e.Geosirina).HasColumnName("geosirina");
            entity.Property(e => e.Imaenc).HasColumnName("imaenc");
            entity.Property(e => e.Kontakt).HasColumnName("kontakt");
            entity.Property(e => e.Naziv)
                .HasMaxLength(255)
                .HasColumnName("naziv");

            entity.HasOne(d => d.Autocesta).WithMany(p => p.Naplatnepostajes)
                .HasForeignKey(d => d.Autocestaid)
                .HasConstraintName("naplatnepostaje_autocestaid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
