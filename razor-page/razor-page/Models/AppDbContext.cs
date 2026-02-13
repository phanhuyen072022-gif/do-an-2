using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace razor_page.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<Hoa> Hoas { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<ThanhToan> ThanhToans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-OSBCV3J\\HUYEN;Database=BAN_HOA_DB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.DonHangId).HasName("PK__don_hang__B81FB4F36A7A4B13");

            entity.ToTable("don_hang");

            entity.Property(e => e.DonHangId).HasColumnName("don_hang_id");
            entity.Property(e => e.GiaDonHang)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("gia_don_hang");
            entity.Property(e => e.HoaId).HasColumnName("hoa_id");
            entity.Property(e => e.NguoiDungId).HasColumnName("nguoi_dung_id");
            entity.Property(e => e.SoLuong).HasColumnName("so_luong");
            entity.Property(e => e.TenDonHang)
                .HasMaxLength(150)
                .HasColumnName("ten_don_hang");
            entity.Property(e => e.ThoiGianDat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("thoi_gian_dat");

            entity.HasOne(d => d.Hoa).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.HoaId)
                .HasConstraintName("FK_donhang_hoa");

            entity.HasOne(d => d.NguoiDung).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.NguoiDungId)
                .HasConstraintName("FK_donhang_nguoidung");
        });

        modelBuilder.Entity<Hoa>(entity =>
        {
            entity.HasKey(e => e.HoaId).HasName("PK__hoa__C002B290559EC715");

            entity.ToTable("hoa");

            entity.Property(e => e.HoaId).HasColumnName("hoa_id");
            entity.Property(e => e.GiaBan)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("gia_ban");
            entity.Property(e => e.LinkHinhAnh)
                .HasMaxLength(255)
                .HasColumnName("link_hinh_anh");
            entity.Property(e => e.MoTa).HasColumnName("mo_ta");
            entity.Property(e => e.TenHoa)
                .HasMaxLength(150)
                .HasColumnName("ten_hoa");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.NguoiDungId).HasName("PK__nguoi_du__3F7CE07722262F56");

            entity.ToTable("nguoi_dung");

            entity.HasIndex(e => e.Email, "UQ__nguoi_du__AB6E61646430359C").IsUnique();

            entity.Property(e => e.NguoiDungId).HasColumnName("nguoi_dung_id");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.LinkAvatar)
                .HasMaxLength(255)
                .HasColumnName("link_avatar");
            entity.Property(e => e.MatKhau)
                .HasMaxLength(255)
                .HasColumnName("mat_khau");
            entity.Property(e => e.Quyen)
                .HasMaxLength(50)
                .HasDefaultValue("Khach hang")
                .HasColumnName("quyen");
            entity.Property(e => e.TenNguoiDung)
                .HasMaxLength(100)
                .HasColumnName("ten_nguoi_dung");
        });

        modelBuilder.Entity<ThanhToan>(entity =>
        {
            entity.HasKey(e => e.ThanhToanId).HasName("PK__thanh_to__42817B504BC42D92");

            entity.ToTable("thanh_toan");

            entity.Property(e => e.ThanhToanId).HasColumnName("thanh_toan_id");
            entity.Property(e => e.DonHangId).HasColumnName("don_hang_id");
            entity.Property(e => e.GiaDonHang)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("gia_don_hang");
            entity.Property(e => e.ThoiGianThanhToan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("thoi_gian_thanh_toan");
            entity.Property(e => e.TienDaThanhToan)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("tien_da_thanh_toan");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasColumnName("trang_thai");

            entity.HasOne(d => d.DonHang).WithMany(p => p.ThanhToans)
                .HasForeignKey(d => d.DonHangId)
                .HasConstraintName("FK_thanhtoan_donhang");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
