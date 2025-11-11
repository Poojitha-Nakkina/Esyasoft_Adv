using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AMI_ProjectAPI.Models;

public partial class AmiContext : DbContext
{
    public AmiContext()
    {
    }

    public AmiContext(DbContextOptions<AmiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BillReading> BillReadings { get; set; }

    public virtual DbSet<Consumer> Consumers { get; set; }

    public virtual DbSet<Dtr> Dtrs { get; set; }

    public virtual DbSet<Feeder> Feeders { get; set; }

    public virtual DbSet<Meter> Meters { get; set; }

    public virtual DbSet<MeterReading> MeterReadings { get; set; }

    public virtual DbSet<Substation> Substations { get; set; }

    public virtual DbSet<Tariff> Tariffs { get; set; }

    public virtual DbSet<TariffSlab> TariffSlabs { get; set; }

    public virtual DbSet<Zone> Zones { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-U25QJMLJ;Initial Catalog=AMI;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BillReading>(entity =>
        {
            entity.HasKey(e => e.BillId).HasName("PK__BillRead__11F2FC4A5265D80A");

            entity.Property(e => e.BillId).HasColumnName("BillID");
            entity.Property(e => e.BillingMonth)
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.ConsumerId).HasColumnName("ConsumerID");
            entity.Property(e => e.EnergyCharge).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.GeneratedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MeterId).HasColumnName("MeterID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Unpaid");
            entity.Property(e => e.TariffId).HasColumnName("TariffID");
            entity.Property(e => e.TariffType).HasMaxLength(50);
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalBill).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalConsumption).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Consumer).WithMany(p => p.BillReadings)
                .HasForeignKey(d => d.ConsumerId)
                .HasConstraintName("FK__BillReadi__Consu__59063A47");

            entity.HasOne(d => d.Meter).WithMany(p => p.BillReadings)
                .HasForeignKey(d => d.MeterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BillReadi__Meter__59FA5E80");

            entity.HasOne(d => d.Tariff).WithMany(p => p.BillReadings)
                .HasForeignKey(d => d.TariffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BillReadi__Tarif__5AEE82B9");
        });

        modelBuilder.Entity<Consumer>(entity =>
        {
            entity.HasKey(e => e.ConsumerId).HasName("PK__Consumer__63BBE99A0D8BD71A");

            entity.ToTable("Consumer");

            entity.Property(e => e.ConsumerId).HasColumnName("ConsumerID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Active");
            entity.Property(e => e.TariffId).HasColumnName("TariffID");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);

            entity.HasOne(d => d.Tariff).WithMany(p => p.Consumers)
                .HasForeignKey(d => d.TariffId)
                .HasConstraintName("FK_Consumer_Tariff");
        });

        modelBuilder.Entity<Dtr>(entity =>
        {
            entity.HasKey(e => e.Dtrid).HasName("PK__DTR__F865635F4A7B6DFE");

            entity.ToTable("DTR");

            entity.Property(e => e.Dtrid).HasColumnName("DTRID");
            entity.Property(e => e.Dtrname)
                .HasMaxLength(100)
                .HasColumnName("DTRName");
            entity.Property(e => e.FeederId).HasColumnName("FeederID");

            entity.HasOne(d => d.Feeder).WithMany(p => p.Dtrs)
                .HasForeignKey(d => d.FeederId)
                .HasConstraintName("FK__DTR__FeederID__3F466844");
        });

        modelBuilder.Entity<Feeder>(entity =>
        {
            entity.HasKey(e => e.FeederId).HasName("PK__Feeder__9B20B0FC892440F1");

            entity.ToTable("Feeder");

            entity.Property(e => e.FeederId).HasColumnName("FeederID");
            entity.Property(e => e.FeederName).HasMaxLength(100);
            entity.Property(e => e.SubstationId).HasColumnName("SubstationID");

            entity.HasOne(d => d.Substation).WithMany(p => p.Feeders)
                .HasForeignKey(d => d.SubstationId)
                .HasConstraintName("FK__Feeder__Substati__3C69FB99");
        });

        modelBuilder.Entity<Meter>(entity =>
        {
            entity.HasKey(e => e.MeterId).HasName("PK__Meter__59223B8CB9BA3F57");

            entity.ToTable("Meter");

            entity.HasIndex(e => e.MeterSerialNo, "IX_Meter_Serial").IsUnique();

            entity.Property(e => e.MeterId).HasColumnName("MeterID");
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.ConsumerId).HasColumnName("ConsumerID");
            entity.Property(e => e.Dtrid).HasColumnName("DTRID");
            entity.Property(e => e.Firmware).HasMaxLength(20);
            entity.Property(e => e.Iccid)
                .HasMaxLength(50)
                .HasColumnName("ICCID");
            entity.Property(e => e.Imsi)
                .HasMaxLength(50)
                .HasColumnName("IMSI");
            entity.Property(e => e.InstallDate).HasPrecision(3);
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .HasColumnName("IPAddress");
            entity.Property(e => e.Manufacturer).HasMaxLength(50);
            entity.Property(e => e.MeterSerialNo).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Active");

            entity.HasOne(d => d.Consumer).WithMany(p => p.Meters)
                .HasForeignKey(d => d.ConsumerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Meter__ConsumerI__4BAC3F29");

            entity.HasOne(d => d.Dtr).WithMany(p => p.Meters)
                .HasForeignKey(d => d.Dtrid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Meter__DTRID__4CA06362");
        });

        modelBuilder.Entity<MeterReading>(entity =>
        {
            entity.HasKey(e => e.ReadingId).HasName("PK__MeterRea__C80F9C6E83566202");

            entity.Property(e => e.ReadingId).HasColumnName("ReadingID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(3)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.MeterId).HasColumnName("MeterID");
            entity.Property(e => e.ReadingDate).HasPrecision(3);
            entity.Property(e => e.ReadingValue).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Meter).WithMany(p => p.MeterReadings)
                .HasForeignKey(d => d.MeterId)
                .HasConstraintName("FK_MeterReadings_Meter");
        });

        modelBuilder.Entity<Substation>(entity =>
        {
            entity.HasKey(e => e.SubstationId).HasName("PK__Substati__BB479C6F1C7BB3F7");

            entity.ToTable("Substation");

            entity.Property(e => e.SubstationId).HasColumnName("SubstationID");
            entity.Property(e => e.SubstationName).HasMaxLength(100);
            entity.Property(e => e.ZoneId).HasColumnName("ZoneID");

            entity.HasOne(d => d.Zone).WithMany(p => p.Substations)
                .HasForeignKey(d => d.ZoneId)
                .HasConstraintName("FK__Substatio__ZoneI__398D8EEE");
        });

        modelBuilder.Entity<Tariff>(entity =>
        {
            entity.HasKey(e => e.TariffId).HasName("PK__Tariff__EBAF9D93525474E3");

            entity.ToTable("Tariff");

            entity.Property(e => e.TariffId).HasColumnName("TariffID");
            entity.Property(e => e.BaseRate).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.TaxRate).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<TariffSlab>(entity =>
        {
            entity.HasKey(e => e.SlabId).HasName("PK__TariffSl__D61699213A5AE8EA");

            entity.ToTable("TariffSlab");

            entity.Property(e => e.FromKwh).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RatePerKwh).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ToKwh).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Tariff).WithMany(p => p.TariffSlabs)
                .HasForeignKey(d => d.TariffId)
                .HasConstraintName("FK_TariffSlab_Tariff");
        });

        modelBuilder.Entity<Zone>(entity =>
        {
            entity.HasKey(e => e.ZoneId).HasName("PK__Zone__60166795CC1B3EEA");

            entity.ToTable("Zone");

            entity.Property(e => e.ZoneId).HasColumnName("ZoneID");
            entity.Property(e => e.ZoneName).HasMaxLength(100);
        });

        modelBuilder.Entity<Payment>()
      .HasOne(p => p.Consumer)
      .WithMany()
      .HasForeignKey(p => p.ConsumerId)
      .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.BillReading)
            .WithMany()
            .HasForeignKey(p => p.BillId)
            .OnDelete(DeleteBehavior.Cascade);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
