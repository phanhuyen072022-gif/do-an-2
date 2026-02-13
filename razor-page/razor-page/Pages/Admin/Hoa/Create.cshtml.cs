using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_page.Models;
using razor_page.Services;

namespace razor_page.Pages.Admin.Hoa
{
    public class CreateModel : PageModel
    {
        private readonly ServiceHoa _serviceHoa;

        public CreateModel(ServiceHoa serviceHoa)
        {
            _serviceHoa = serviceHoa;
        }

        [BindProperty]
        public razor_page.Models.Hoa Hoa { get; set; } = default!;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _serviceHoa.Create(Hoa);

            return RedirectToPage("./Index");
        }
    }
}