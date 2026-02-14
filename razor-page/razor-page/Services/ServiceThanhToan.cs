using Microsoft.EntityFrameworkCore;
using razor_page.Models;

namespace razor_page.Services
{
    public class ServiceThanhToan
    {
        private readonly AppDbContext _context;

        public ServiceThanhToan(AppDbContext context)
        {
            _context = context;
        }

        // Lấy danh sách tất cả thanh toán kèm thông tin Đơn hàng
        public List<ThanhToan> GetAll()
        {
            return _context.ThanhToans
                .Include(t => t.DonHang)
                .OrderByDescending(t => t.ThoiGianThanhToan)
                .ToList();
        }

        // Lấy thông tin chi tiết của 1 thanh toán (kèm Đơn hàng, Người dùng và Hoa)
        public ThanhToan? GetById(int id)
        {
            return _context.ThanhToans
                .Include(t => t.DonHang)
                    .ThenInclude(d => d.NguoiDung) // Lấy thêm thông tin Khách hàng từ Đơn hàng
                .Include(t => t.DonHang)
                    .ThenInclude(d => d.Hoa)       // Lấy thêm thông tin Hoa từ Đơn hàng
                .FirstOrDefault(t => t.ThanhToanId == id);
        }
    }
}