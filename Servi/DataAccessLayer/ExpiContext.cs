using System;
using Servi.Models;
using Microsoft.EntityFrameworkCore;

namespace Servi.DataAccessLayer;

public partial class EXPIContext : DbContext
{
    public EXPIContext()
    {
    }
    public EXPIContext(DbContextOptions<EXPIContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Inventary> Inventaries { get; set; } = null!;


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer( "Data Source=localhost; Initial Catalog=EXPI; Integrated Security=SSPI"
                                       /*"Data Source=dcdwdev1.seachefs.local; User ID=expi; Password=...; Initial Catalog=EXPI; Persist Security Info=True; TrustServerCertificate=True;"*/
                                       );
            
        }
    }

    public int ActivePostCountForBlog(decimal pk)
    => throw new NotSupportedException();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDbFunction(typeof(EXPIContext).GetMethod(nameof(ActivePostCountForBlog), new[] { typeof(decimal) })).HasName("GetStoreName");



        modelBuilder.Entity<Ledger>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("general_ledger");

            entity.Property(e => e.LedgerDescription)
                .HasMaxLength(40)
                .HasColumnName("general_ledger_description");

            entity.Property(e => e.LedgerNo)
                .HasMaxLength(10)
                .HasColumnName("general_ledger_no");

            entity.Property(e => e.LedgerPk)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("general_ledger_pk");

            entity.Property(e => e.LastTouchDate)
                .HasColumnName("last_touch_date");
        });

        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<UserLocation>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("user_locations");

            entity.Property(e => e.ActiveFlag)
                .HasMaxLength(1)
                .HasColumnName("active_flag");

            entity.Property(e => e.CreatorLocationPk)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("creator_location_pk");

            entity.Property(e => e.LastTouchDate)
                .HasColumnName("last_touch_date");

            entity.Property(e => e.LocationPk)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("location_pk");

            entity.Property(e => e.UserLocationPk)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("user_location_pk");

            entity.Property(e => e.UserPk)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("user_pk");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("vendors");

            entity.Property(e => e.LastTouchDate)
                .HasColumnName("last_touch_date");

            entity.Property(e => e.LocationPk)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("location_pk");

            entity.Property(e => e.VendorAccountNumber)
                .HasMaxLength(40)
                .HasColumnName("vendor_account_number");

            entity.Property(e => e.VendorActive)
                .HasMaxLength(1)
                .HasColumnName("vendor_active");

            entity.Property(e => e.VendorAddress1)
                .HasMaxLength(50)
                .HasColumnName("vendor_address_1");

            entity.Property(e => e.VendorAddress2)
                .HasMaxLength(50)
                .HasColumnName("vendor_address_2");

            entity.Property(e => e.VendorCityPk)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("vendor_city_pk");

            entity.Property(e => e.VendorCode)
                .HasMaxLength(6)
                .HasColumnName("vendor_code");

            entity.Property(e => e.VendorCurrencyPk)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("vendor_currency_pk");

            entity.Property(e => e.VendorEMail)
                .HasMaxLength(2000)
                .HasColumnName("vendor_e_mail");

            entity.Property(e => e.VendorFax)
                .HasMaxLength(30)
                .HasColumnName("vendor_fax");

            entity.Property(e => e.VendorName)
                .HasMaxLength(40)
                .HasColumnName("vendor_name");

            entity.Property(e => e.VendorPhone)
                .HasMaxLength(30)
                .HasColumnName("vendor_phone");

            entity.Property(e => e.VendorPk)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("vendor_pk");

            entity.Property(e => e.VendorStatePk)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("vendor_state_pk");

            entity.Property(e => e.VendorZip)
                .HasMaxLength(10)
                .HasColumnName("vendor_zip");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
