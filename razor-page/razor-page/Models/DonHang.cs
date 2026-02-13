using System;
using System.Collections.Generic;

namespace razor_page.Models;

public partial class DonHang
{
    public int DonHangId { get; set; }

    public string TenDonHang { get; set; } = null!;

    public int NguoiDungId { get; set; }

    public int HoaId { get; set; }

    public int SoLuong { get; set; }

    public decimal GiaDonHang { get; set; }

    public DateTime? ThoiGianDat { get; set; }

    public virtual Hoa Hoa { get; set; } = null!;

    public virtual NguoiDung NguoiDung { get; set; } = null!;

    public virtual ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();
}
