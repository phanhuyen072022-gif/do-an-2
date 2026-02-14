using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_page.Models;
using razor_page.Services;

namespace razor_page.Pages.Admin.ThanhToan
{
    public class IndexModel : PageModel
    {
        private readonly ServiceThanhToan _service;

        public IndexModel(ServiceThanhToan service)
        {
            _service = service;
        }

        public List<razor_page.Models.ThanhToan> ThanhToans { get; set; } = new();

        public void OnGet()
        {
            ThanhToans = _service.GetAll();
        }
    }
}