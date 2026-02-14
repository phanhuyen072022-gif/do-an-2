using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using razor_page.Models;
using razor_page.Services;

namespace razor_page.Pages.Admin.DonHang
{
    public class EditModel : PageModel
    {
        private readonly ServiceDonHang _service;
        public EditModel(ServiceDonHang service) { _service = service; }

        [BindProperty]
        public razor_page.Models.DonHang DonHang { get; set; } = default!;
        public SelectList NguoiDungList { get; set; }
        public SelectList HoaList { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null) return NotFound();

            var donhang = _service.GetById(id.Value);
            if (donhang == null) return NotFound();

            DonHang = donhang;
            NguoiDungList = new SelectList(_service.GetListNguoiDung(), "NguoiDungId", "TenNguoiDung");
            HoaList = new SelectList(_service.GetListHoa(), "HoaId", "TenHoa");
            return Page();
        }

        public IActionResult OnPost()
        {
            _service.Update(DonHang);
            return RedirectToPage("./Index");
        }
    }
}