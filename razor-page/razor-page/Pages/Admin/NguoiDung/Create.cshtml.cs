using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_page.Models;
using razor_page.Services;

namespace razor_page.Pages.Admin.NguoiDung
{
    public class CreateModel : PageModel
    {
        private readonly ServiceNguoiDung _serviceNguoiDung;

        public CreateModel(ServiceNguoiDung serviceNguoiDung)
        {
            _serviceNguoiDung = serviceNguoiDung;
        }

        [BindProperty]
        public razor_page.Models.NguoiDung NguoiDung { get; set; } = default!;

        public void OnGet()
        {
            // Có thể khởi tạo giá trị mặc định nếu cần
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Gọi hàm Create từ ServiceNguoiDung
            _serviceNguoiDung.Create(NguoiDung);

            return RedirectToPage("./Index");
        }
    }
}