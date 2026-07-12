//01101001
using System;
using System.Collections.Generic;
using IoTAssetManagement2.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoTAssetManagement2.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DeviceTypes> DeviceTypes { get; set; }

    public virtual DbSet<Devices> Devices { get; set; }

    public virtual DbSet<Firmware> Firmware { get; set; }

    public virtual DbSet<Groups> Groups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Devices>(entity =>
        {
            entity.HasOne(d => d.DeviceType).WithMany(p => p.Devices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Devices_DeviceTypes");

            entity.HasOne(d => d.Firmware).WithMany(p => p.Devices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Devices_Firmware");

            entity.HasOne(d => d.Group).WithMany(p => p.Devices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Devices_Groups");
        });

        modelBuilder.Entity<Groups>(entity =>
        {
            entity.HasOne(d => d.ParentGroup).WithMany(p => p.InverseParentGroup).HasConstraintName("FK_Groups_ParentGroup");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
