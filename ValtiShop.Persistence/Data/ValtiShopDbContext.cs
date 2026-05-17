using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ValtiShop.Persistence.ScaffoldModels;

namespace ValtiShop.Persistence.Data;

public partial class ValtiShopDbContext : DbContext
{
    public ValtiShopDbContext(DbContextOptions<ValtiShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Issuer> Issuers { get; set; }

    public virtual DbSet<License> Licenses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Issuer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_issuers");

            entity
                .ToTable("issuers", "licenses")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("issuers_history", "licenses");
                        ttb
                            .HasPeriodStart("sys_start")
                            .HasColumnName("sys_start");
                        ttb
                            .HasPeriodEnd("sys_end")
                            .HasColumnName("sys_end");
                    }));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(255)
                .HasColumnName("contact_email");
            entity.Property(e => e.ContactPhone)
                .HasMaxLength(50)
                .HasColumnName("contact_phone");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.PhysicalAddress)
                .HasMaxLength(500)
                .HasColumnName("physical_address");
        });

        modelBuilder.Entity<License>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_licenses");

            entity
                .ToTable("licenses", "licenses")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("licenses_history", "licenses");
                        ttb
                            .HasPeriodStart("sys_start")
                            .HasColumnName("sys_start");
                        ttb
                            .HasPeriodEnd("sys_end")
                            .HasColumnName("sys_end");
                    }));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IssuerId).HasColumnName("issuer_id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.ValidFrom).HasColumnName("valid_from");
            entity.Property(e => e.ValidTo).HasColumnName("valid_to");

            entity.HasOne(d => d.Issuer).WithMany(p => p.Licenses)
                .HasForeignKey(d => d.IssuerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_licenses_issuers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
