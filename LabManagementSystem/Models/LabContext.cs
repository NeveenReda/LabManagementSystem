using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Models;

public partial class LabContext : DbContext
{
    public LabContext()
    {
    }

    public LabContext(DbContextOptions<LabContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Audit> Audits { get; set; }

    public virtual DbSet<BaseExternalLab> BaseExternalLabs { get; set; }

    public virtual DbSet<ExternalLab> ExternalLabs { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Lab> Labs { get; set; }

    public virtual DbSet<LabCategory> LabCategorys { get; set; }

    public virtual DbSet<LabParameter> LabParameters { get; set; }

    public virtual DbSet<LabParameterCategory> LabParameterCategories { get; set; }

    public virtual DbSet<Need> Needs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategorys { get; set; }

    public virtual DbSet<ProductUnit> ProductUnits { get; set; }

    public virtual DbSet<ProductUsage> ProductUsages { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }

    public virtual DbSet<VisitLab> VisitLabs { get; set; }

    public virtual DbSet<VisitLabResult> VisitLabResults { get; set; }

    public virtual DbSet<VisitLabStatus> VisitLabStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=db43945.public.databaseasp.net;Database=db43945;User Id=db43945;Password=8y@LwB9!6+Hd;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Audit>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Audits_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.Audits)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ExternalLab>(entity =>
        {
            entity.HasIndex(e => e.BaseExternalLabId, "IX_ExternalLabs_BaseExternalLabId");

            entity.HasIndex(e => e.VistLabId, "IX_ExternalLabs_VistLabId");

            entity.HasOne(d => d.BaseExternalLab).WithMany(p => p.ExternalLabs)
                .HasForeignKey(d => d.BaseExternalLabId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.VistLab).WithMany(p => p.ExternalLabs)
                .HasForeignKey(d => d.VistLabId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Lab>(entity =>
        {
            entity.HasIndex(e => e.LabCategoryId, "IX_Labs_LabCategoryId");

            entity.Property(e => e.Days).HasDefaultValue(1);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.LabCategory).WithMany(p => p.Labs)
                .HasForeignKey(d => d.LabCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<LabParameter>(entity =>
        {
            entity.HasIndex(e => e.GenderId, "IX_LabParameters_GenderId");

            entity.HasIndex(e => e.LabId, "IX_LabParameters_LabId");

            entity.HasIndex(e => e.LabParametersCategoryId, "IX_LabParameters_LabParametersCategoryId");

            entity.Property(e => e.NormalRangeMax).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NormalRangeMin).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Gender).WithMany(p => p.LabParameters).HasForeignKey(d => d.GenderId);

            entity.HasOne(d => d.Lab).WithMany(p => p.LabParameters).HasForeignKey(d => d.LabId);

            entity.HasOne(d => d.LabParametersCategory).WithMany(p => p.LabParameters)
                .HasForeignKey(d => d.LabParametersCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<LabParameterCategory>(entity =>
        {
            entity.ToTable("LabParameterCategory");

            entity.HasIndex(e => e.LabId, "IX_LabParameterCategory_LabId");

            entity.HasOne(d => d.Lab).WithMany(p => p.LabParameterCategories)
                .HasForeignKey(d => d.LabId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Need>(entity =>
        {
            entity.Property(e => e.Notes).HasColumnName("notes");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.NetPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasIndex(e => e.OrderId, "IX_OrderDetails_OrderId");

            entity.HasIndex(e => e.ProductId, "IX_OrderDetails_ProductId");

            entity.HasIndex(e => e.UnitId, "IX_OrderDetails_UnitId");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Unit).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasIndex(e => e.GenderId, "IX_Patients_GenderId");

            entity.HasOne(d => d.Gender).WithMany(p => p.Patients)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasIndex(e => e.VisitId, "IX_Payments_VisitId");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Visit).WithMany(p => p.Payments)
                .HasForeignKey(d => d.VisitId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.BaseUnitId, "IX_Products_BaseUnitId");

            entity.HasIndex(e => e.ProductCategoryId, "IX_Products_ProductCategoryId");

            entity.Property(e => e.MinAmount).HasDefaultValue(2);

            entity.HasOne(d => d.BaseUnit).WithMany(p => p.Products)
                .HasForeignKey(d => d.BaseUnitId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasIndex(e => e.ParentCategoryId, "IX_ProductCategorys_ParentCategoryId");

            entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory).HasForeignKey(d => d.ParentCategoryId);
        });

        modelBuilder.Entity<ProductUnit>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_ProductUnits_ProductId");

            entity.HasIndex(e => e.UnitId, "IX_ProductUnits_UnitId");

            entity.Property(e => e.ConversionToBase)
                .HasDefaultValue(1.0m)
                .HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductUnits)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Unit).WithMany(p => p.ProductUnits)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ProductUsage>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_ProductUsages_ProductId");

            entity.HasIndex(e => e.UnitId, "IX_ProductUsages_UnitId");

            entity.HasIndex(e => e.VisitId, "IX_ProductUsages_VisitId");

            entity.Property(e => e.ProductAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductUsages)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Unit).WithMany(p => p.ProductUsages)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Visit).WithMany(p => p.ProductUsages).HasForeignKey(d => d.VisitId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.HasIndex(e => e.PatientId, "IX_Visits_PatientId");

            entity.Property(e => e.DiscountPercent).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DiscountValue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NetPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Patient).WithMany(p => p.Visits)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<VisitLab>(entity =>
        {
            entity.HasIndex(e => e.LabId, "IX_VisitLabs_LabId");

            entity.HasIndex(e => e.VisitId, "IX_VisitLabs_VisitId");

            entity.HasIndex(e => e.VisitLabStatusId, "IX_VisitLabs_VisitLabStatusId");

            entity.Property(e => e.LabPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Lab).WithMany(p => p.VisitLabs)
                .HasForeignKey(d => d.LabId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Visit).WithMany(p => p.VisitLabs)
                .HasForeignKey(d => d.VisitId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.VisitLabStatus).WithMany(p => p.VisitLabs)
                .HasForeignKey(d => d.VisitLabStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<VisitLabResult>(entity =>
        {
            entity.HasIndex(e => e.LabParameterId, "IX_VisitLabResults_LabParameterId");

            entity.HasIndex(e => e.VisitLabId, "IX_VisitLabResults_VisitLabId");

            entity.HasOne(d => d.LabParameter).WithMany(p => p.VisitLabResults)
                .HasForeignKey(d => d.LabParameterId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.VisitLab).WithMany(p => p.VisitLabResults)
                .HasForeignKey(d => d.VisitLabId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<VisitLabStatus>(entity =>
        {
            entity.ToTable("VisitLabStatus");

            entity.Property(e => e.Name).HasDefaultValue("Pending");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
