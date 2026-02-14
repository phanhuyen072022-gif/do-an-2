using razor_page.Models;
using Microsoft.EntityFrameworkCore;

namespace razor_page.Services
{
    public class ServiceDonHang
    {
        private readonly AppDbContext _context;

        public ServiceDonHang(AppDbContext context)
        {
            _context = context;
        }

        // 1. Lấy danh sách đơn hàng (kèm thông tin Hoa và Người dùng)
        public List<DonHang> GetAll()
        {
            return _context.DonHangs
                .Include(d => d.Hoa)
                .Include(d => d.NguoiDung)
                .OrderByDescending(d => d.ThoiGianDat)
                .ToList();
        }

        // 2. Lấy đơn hàng theo ID
        public DonHang? GetById(int id)
        {
            return _context.DonHangs
                .Include(d => d.Hoa)
                .Include(d => d.NguoiDung)
                .Include(d => d.ThanhToans) // Lấy lịch sử thanh toán
                .FirstOrDefault(d => d.DonHangId == id);
        }

        // 3. Tạo đơn hàng mới
        public void Create(DonHang donHang)
        {
            // Tự động gán thời gian nếu chưa có
            if (donHang.ThoiGianDat == null) donHang.ThoiGianDat = DateTime.Now;

            _context.DonHangs.Add(donHang);
            _context.SaveChanges();
        }

        // 4. Cập nhật đơn hàng
        public void Update(DonHang donHang)
        {
            var existing = _context.DonHangs.FirstOrDefault(d => d.DonHangId == donHang.DonHangId);
            if (existing != null)
            {
                existing.TenDonHang = donHang.TenDonHang;
                existing.NguoiDungId = donHang.NguoiDungId;
                existing.HoaId = donHang.HoaId;
                existing.SoLuong = donHang.SoLuong;
                existing.GiaDonHang = donHang.GiaDonHang;
                // Giữ nguyên thời gian đặt cũ hoặc cập nhật tùy logic

                _context.SaveChanges();
            }
        }

        // 5. Xóa đơn hàng
        public void Delete(int id)
        {
            var donHang = _context.DonHangs.FirstOrDefault(d => d.DonHangId == id);
            if (donHang != null)
            {
                // Cần xóa hoặc xử lý các bản ghi ThanhToan liên quan nếu chưa cấu hình Cascade Delete trong DB
                var thanhToans = _context.ThanhToans.Where(t => t.DonHangId == id).ToList();
                _context.ThanhToans.RemoveRange(thanhToans);

                _context.DonHangs.Remove(donHang);
                _context.SaveChanges();
            }
        }

        // 6. Xử lý Thanh toán (Tạo bản ghi vào bảng ThanhToan)
        public void ThanhToanDonHang(int donHangId, decimal soTien, string trangThai)
        {
            var donHang = _context.DonHangs.Find(donHangId);
            if (donHang != null)
            {
                var thanhToan = new ThanhToan
                {
                    DonHangId = donHangId,
                    GiaDonHang = donHang.GiaDonHang,
                    TienDaThanhToan = soTien,
                    TrangThai = trangThai,
                    ThoiGianThanhToan = DateTime.Now
                };
                _context.ThanhToans.Add(thanhToan);
                _context.SaveChanges();
            }
        }

        // Helper: Lấy danh sách Hoa và User cho Dropdown
        public List<Hoa> GetListHoa() => _context.Hoas.ToList();
        public List<NguoiDung> GetListNguoiDung() => _context.NguoiDungs.ToList();
    }
}