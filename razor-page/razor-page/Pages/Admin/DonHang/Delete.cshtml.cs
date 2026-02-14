using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_page.Services;

namespace razor_page.Pages.Admin.DonHang
{
    public class DeleteModel : PageModel
    {
        private readonly ServiceDonHang _service;
        public DeleteModel(ServiceDonHang service) { _service = service; }

        [BindProperty]
        public razor_page.Models.DonHang DonHang { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null) return NotFound();
            DonHang = _service.GetById(id.Value);
            if (DonHang == null) return NotFound();
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null) return NotFound();
            _service.Delete(id.Value);
            return RedirectToPage("./Index");
        }
    }
}