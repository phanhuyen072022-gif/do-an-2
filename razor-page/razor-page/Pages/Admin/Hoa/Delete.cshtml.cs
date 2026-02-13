using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_page.Services;

namespace razor_page.Pages.Admin.Hoa
{
    public class DeleteModel : PageModel
    {
        private readonly ServiceHoa _serviceHoa;

        public DeleteModel(ServiceHoa serviceHoa)
        {
            _serviceHoa = serviceHoa;
        }

        [BindProperty]
        public razor_page.Models.Hoa Hoa { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null) return NotFound();

            var hoa = _serviceHoa.GetById(id.Value);
            if (hoa == null) return NotFound();

            Hoa = hoa;
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null) return NotFound();

            _serviceHoa.Delete(id.Value);

            return RedirectToPage("./Index");
        }
    }
}