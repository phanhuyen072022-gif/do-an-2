using System;
using System.Collections.Generic;

namespace razor_page.Models;

public partial class Hoa
{
    public int HoaId { get; set; }

    public string TenHoa { get; set; } = null!;

    public string? MoTa { get; set; }

    public string? LinkHinhAnh { get; set; }

    public decimal GiaBan { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}
