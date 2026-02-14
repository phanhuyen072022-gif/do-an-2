using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_page.Models;
using razor_page.Services;
using Microsoft.AspNetCore.Http;

namespace razor_page.Pages
{
    public class ChiTietSanPhamModel : PageModel
    {
        private readonly ServiceHoa _serviceHoa;
        private readonly ServiceDonHang _serviceDonHang;

        public ChiTietSanPhamModel(ServiceHoa serviceHoa, ServiceDonHang serviceDonHang)
        {
            _serviceHoa = serviceHoa;
            _serviceDonHang = serviceDonHang;
        }

        public Hoa Hoa { get; set; }

        [BindProperty]
        public int SoLuong { get; set; } = 1; // Mặc định số lượng là 1

        [TempData]
        public string ThongBao { get; set; }

        public IActionResult OnGet(int id)
        {
            Hoa = _serviceHoa.GetById(id);

            if (Hoa == null)
            {
                return RedirectToPage("/SanPham");
            }
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            // 1. Kiểm tra đăng nhập từ Session
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                // Nếu chưa đăng nhập, chuyển hướng sang trang đăng nhập
                return RedirectToPage("/DangNhap");
            }

            // 2. Lấy thông tin hoa để tính giá
            var hoa = _serviceHoa.GetById(id);
            if (hoa == null) return NotFound();

            // 3. Tạo đơn hàng mới
            var donHang = new DonHang
            {
                TenDonHang = $"Đơn mua {hoa.TenHoa} - {DateTime.Now:dd/MM/yyyy HH:mm}",
                NguoiDungId = userId.Value,
                HoaId = hoa.HoaId,
                SoLuong = SoLuong,
                GiaDonHang = hoa.GiaBan * SoLuong, // Tính tổng tiền
                ThoiGianDat = DateTime.Now
            };

            // 4. Gọi Service để lưu vào CSDL
            _serviceDonHang.Create(donHang);

            ThongBao = "Đặt hàng thành công! Chúng tôi sẽ sớm liên hệ.";
            return RedirectToPage("/SanPham");
        }
    }
}