using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace razor_page.Pages
{
    public class DangXuatModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Xóa toàn bộ session
            HttpContext.Session.Clear();

            // Chuyển hướng về trang chủ
            return RedirectToPage("/Index");
        }
    }
}