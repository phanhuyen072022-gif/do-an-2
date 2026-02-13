using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_page.Services;
using Microsoft.AspNetCore.Http; // Cần thiết để dùng Session

namespace razor_page.Pages
{
    public class DangNhapModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string MatKhau { get; set; }

        public string ThongBaoLoi { get; set; }

        public void OnGet()
        {
            // Nếu đã đăng nhập rồi thì đá về trang chủ
            if (HttpContext.Session.GetString("UserEmail") != null)
            {
                Response.Redirect("/Index");
            }
        }

        public IActionResult OnPost()
        {
            ServiceNguoiDung service = new ServiceNguoiDung();
            var user = service.DangNhap(Email, MatKhau);

            if (user != null)
            {
                // Lưu thông tin vào Session
                HttpContext.Session.SetInt32("UserId", user.NguoiDungId);
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserName", user.TenNguoiDung);
                HttpContext.Session.SetString("UserRole", user.Quyen);

                return RedirectToPage("/Index");
            }
            else
            {
                ThongBaoLoi = "Email hoặc mật khẩu không chính xác!";
                return Page();
            }
        }
    }
}