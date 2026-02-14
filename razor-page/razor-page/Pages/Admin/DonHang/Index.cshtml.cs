using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_page.Models;
using razor_page.Services;

namespace razor_page.Pages.Admin.DonHang
{
    public class IndexModel : PageModel
    {
        private readonly ServiceDonHang _service;
        public IndexModel(ServiceDonHang service) { _service = service; }

        public List<razor_page.Models.DonHang> DonHangs { get; set; } = new();

        public void OnGet()
        {
            DonHangs = _service.GetAll();
        }
    }
}