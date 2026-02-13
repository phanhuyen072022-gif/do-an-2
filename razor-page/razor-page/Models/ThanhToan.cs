using System;
using System.Collections.Generic;

namespace razor_page.Models;

public partial class ThanhToan
{
    public int ThanhToanId { get; set; }

    public int DonHangId { get; set; }

    public decimal TienDaThanhToan { get; set; }

    public decimal GiaDonHang { get; set; }

    public string TrangThai { get; set; } = null!;

    public DateTime? ThoiGianThanhToan { get; set; }

    public virtual DonHang DonHang { get; set; } = null!;
}
