using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_page.Models;
using razor_page.Services;

namespace razor_page.Pages
{
    public class DonHangCuaToiModel : PageModel
    {
        private readonly AppDbContext _context;

        public DonHangCuaToiModel(AppDbContext context)
        {
            _context = context;
        }

        public List<DonHang> DanhSachDonHang { get; set; } = new List<DonHang>();

        public IActionResult OnGet()
        {
            // Kiểm tra xem khách hàng đã đăng nhập chưa bằng Session
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                // Nếu chưa đăng nhập, đá về trang đăng nhập
                return RedirectToPage("/DangNhap");
            }

            // Khởi tạo service và lấy danh sách đơn hàng của User này
            var service = new ServiceDonHang(_context);
            DanhSachDonHang = service.GetByUserId(userId.Value);

            return Page();
        }
    }
}