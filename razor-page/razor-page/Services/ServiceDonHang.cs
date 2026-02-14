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
        public (bool Success, string Message) ThanhToanDonHang(int donHangId, decimal soTien, string trangThai)
        {
            // Sử dụng GetById để lấy kèm cả danh sách ThanhToans (lịch sử thanh toán)
            var donHang = GetById(donHangId);
            if (donHang != null)
            {
                // Tính tổng tiền đã thanh toán trước đó
                decimal tongDaThanhToan = donHang.ThanhToans.Sum(t => t.TienDaThanhToan);
                decimal soTienConLai = donHang.GiaDonHang - tongDaThanhToan;

                // Chặn nếu đơn đã thanh toán đủ
                if (soTienConLai <= 0)
                {
                    return (false, "Đơn hàng này đã được thanh toán đủ. Không thể thanh toán thêm.");
                }

                // Chặn nếu nhập số tiền thanh toán lớn hơn số cần phải trả
                if (soTien > soTienConLai)
                {
                    return (false, $"Số tiền vượt quá giới hạn. Đơn hàng chỉ còn nợ: {soTienConLai:N0}");
                }

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

                return (true, "Thanh toán thành công");
            }
            return (false, "Không tìm thấy đơn hàng");
        }

        // Helper: Lấy danh sách Hoa và User cho Dropdown
        public List<Hoa> GetListHoa() => _context.Hoas.ToList();
        public List<NguoiDung> GetListNguoiDung() => _context.NguoiDungs.ToList();
        public List<NguoiDung> GetListKhachHang()
        {
            return _context.NguoiDungs.Where(n => n.Quyen == "KhachHang").ToList();
        }
    }
}