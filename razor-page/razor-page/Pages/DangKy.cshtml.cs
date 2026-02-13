using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_page.Models;
using razor_page.Services;

namespace razor_page.Pages
{
    public class DangKyModel : PageModel
    {
        [BindProperty]
        public NguoiDung NguoiDung { get; set; } = new NguoiDung();

        [BindProperty]
        public string XacNhanMatKhau { get; set; }

        public string ThongBaoLoi { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (NguoiDung.MatKhau != XacNhanMatKhau)
            {
                ThongBaoLoi = "Mật khẩu xác nhận không khớp!";
                return Page();
            }

            ServiceNguoiDung service = new ServiceNguoiDung();
            bool ketQua = service.DangKy(NguoiDung);

            if (ketQua)
            {
                // Đăng ký thành công, chuyển sang trang đăng nhập
                return RedirectToPage("/DangNhap");
            }
            else
            {
                ThongBaoLoi = "Email này đã được sử dụng!";
                return Page();
            }
        }
    }
}