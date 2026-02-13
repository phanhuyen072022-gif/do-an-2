using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_page.Models;
using razor_page.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace razor_page.Pages.Admin
{
    public class DanhSachNguoiDungModel : PageModel
    {
        public List<NguoiDung> DanhSachUsers { get; set; } = new List<NguoiDung>();

        public IActionResult OnGet()
        {
            // 1. Kiểm tra bảo mật: Phải đăng nhập VÀ phải là Admin
            var role = HttpContext.Session.GetString("UserRole");
            if (string.IsNullOrEmpty(role) || role != "Admin")
            {
                // Nếu không phải Admin, đá về trang chủ hoặc trang lỗi
                return RedirectToPage("/Index");
            }

            // 2. Lấy dữ liệu
            ServiceNguoiDung service = new ServiceNguoiDung();
            DanhSachUsers = service.GetAll(); // Hàm này bạn đã có sẵn trong ServiceNguoiDung.cs

            return Page();
        }
    }
}