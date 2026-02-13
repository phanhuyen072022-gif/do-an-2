using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_page.Services;

namespace razor_page.Pages.Admin.Hoa
{
    public class EditModel : PageModel
    {
        private readonly ServiceHoa _serviceHoa;

        public EditModel(ServiceHoa serviceHoa)
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

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            _serviceHoa.Update(Hoa);

            return RedirectToPage("./Index");
        }
    }
}