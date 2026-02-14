using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_page.Services;

namespace razor_page.Pages.Admin.ThanhToan
{
    public class DetailsModel : PageModel
    {
        private readonly ServiceThanhToan _service;

        public DetailsModel(ServiceThanhToan service)
        {
            _service = service;
        }

        public razor_page.Models.ThanhToan ThanhToan { get; set; }

        public IActionResult OnGet(int id)
        {
            ThanhToan = _service.GetById(id);

            if (ThanhToan == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}