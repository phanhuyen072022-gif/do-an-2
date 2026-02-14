using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_page.Services;

namespace razor_page.Pages.Admin.NguoiDung
{
    public class DeleteModel : PageModel
    {
        private readonly ServiceNguoiDung _serviceNguoiDung;

        public DeleteModel(ServiceNguoiDung serviceNguoiDung)
        {
            _serviceNguoiDung = serviceNguoiDung;
        }

        [BindProperty]
        public razor_page.Models.NguoiDung NguoiDung { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null) return NotFound();

            var user = _serviceNguoiDung.GetById(id.Value);
            if (user == null) return NotFound();

            NguoiDung = user;
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null) return NotFound();

            // Gọi hàm Delete từ ServiceNguoiDung
            _serviceNguoiDung.Delete(id.Value);

            return RedirectToPage("./Index");
        }
    }
}