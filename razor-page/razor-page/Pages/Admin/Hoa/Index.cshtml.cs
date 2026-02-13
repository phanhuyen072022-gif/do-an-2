using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_page.Models;
using razor_page.Services;

namespace razor_page.Pages.Admin.Hoa
{
    public class IndexModel : PageModel
    {
        private readonly ServiceHoa _serviceHoa;

        public IndexModel(ServiceHoa serviceHoa)
        {
            _serviceHoa = serviceHoa;
        }

        public List<razor_page.Models.Hoa> Hoas { get; set; } = new List<razor_page.Models.Hoa>();

        public void OnGet()
        {
            Hoas = _serviceHoa.GetAll();
        }
    }
}