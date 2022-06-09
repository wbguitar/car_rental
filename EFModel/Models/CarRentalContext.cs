using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFModel.Models
{
    public partial class CarRentalContext : DbContext
    {
        public CarRentalContext()
        {
        }

        public CarRentalContext(DbContextOptions<CarRentalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accessory> Accessories { get; set; } = null!;
        public virtual DbSet<EngineType> EngineTypes { get; set; } = null!;
        public virtual DbSet<Maintenance> Maintenances { get; set; } = null!;
        public virtual DbSet<Manufacturer> Manufacturers { get; set; } = null!;
        public virtual DbSet<RentVehicle> RentVehicles { get; set; } = null!;
        public virtual DbSet<TransmissionType> TransmissionTypes { get; set; } = null!;
        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;
        public virtual DbSet<VehicleAccessory> VehicleAccessories { get; set; } = null!;
        public virtual DbSet<VehicleCategory> VehicleCategories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=CarRental;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accessory>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<EngineType>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Maintenance>(entity =>
            {
                entity.HasKey(e => e.Vehicle)
                    .HasName("PK_Maintenance");

                entity.Property(e => e.Vehicle).ValueGeneratedNever();

                entity.HasOne(d => d.VehicleNavigation)
                    .WithOne(p => p.Maintenance)
                    .HasForeignKey<Maintenance>(d => d.Vehicle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Maintenances_Vehicles");
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<RentVehicle>(entity =>
            {
                entity.HasKey(e => e.Vehicle)
                    .HasName("PK_RentVehicles_1");

                entity.Property(e => e.Vehicle).ValueGeneratedNever();

                entity.HasOne(d => d.VehicleNavigation)
                    .WithOne(p => p.RentVehicle)
                    .HasForeignKey<RentVehicle>(d => d.Vehicle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RentVehicles_Vehicles");
            });

            modelBuilder.Entity<TransmissionType>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.Category)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicles_VehicleCategories");

                entity.HasOne(d => d.EngineNavigation)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.Engine)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicles_EngineTypes");

                entity.HasOne(d => d.ManufacturerNavigation)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.Manufacturer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicles_Manufacturers");

                entity.HasOne(d => d.TransmissionNavigation)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.Transmission)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicles_TransmissionTypes");
            });

            modelBuilder.Entity<VehicleAccessory>(entity =>
            {
                entity.HasIndex(e => new { e.AccessoryId, e.VehicleId }, "IX_VehicleAccessories_1")
                    .IsUnique();

                entity.Property(e => e.Status).HasDefaultValueSql("((5))");

                entity.HasOne(d => d.Accessory)
                    .WithMany(p => p.VehicleAccessories)
                    .HasForeignKey(d => d.AccessoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleAccessories_Accessories");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleAccessories)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleAccessories_Vehicles");
            });

            modelBuilder.Entity<VehicleCategory>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
