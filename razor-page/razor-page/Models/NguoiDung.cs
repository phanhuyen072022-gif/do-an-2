using System;
using System.Collections.Generic;

namespace razor_page.Models;

public partial class NguoiDung
{
    public int NguoiDungId { get; set; }

    public string TenNguoiDung { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string Quyen { get; set; } = null!;

    public string? LinkAvatar { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}
