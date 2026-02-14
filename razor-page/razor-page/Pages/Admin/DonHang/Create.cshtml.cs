using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using razor_page.Models;
using razor_page.Services;

namespace razor_page.Pages.Admin.DonHang
{
    public class CreateModel : PageModel
    {
        private readonly ServiceDonHang _service;
        public CreateModel(ServiceDonHang service) { _service = service; }

        [BindProperty]
        public razor_page.Models.DonHang DonHang { get; set; } = default!;

        public SelectList NguoiDungList { get; set; }
        public SelectList HoaList { get; set; }

        public void OnGet()
        {
            NguoiDungList = new SelectList(_service.GetListNguoiDung(), "NguoiDungId", "TenNguoiDung");
            HoaList = new SelectList(_service.GetListHoa(), "HoaId", "TenHoa");
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page(); // Hoặc bỏ qua check strict nếu model phức tạp

            // Tính giá đơn hàng tự động (Option)
            var hoa = _service.GetListHoa().FirstOrDefault(h => h.HoaId == DonHang.HoaId);
            if (hoa != null && DonHang.GiaDonHang == 0)
            {
                DonHang.GiaDonHang = hoa.GiaBan * DonHang.SoLuong;
            }

            _service.Create(DonHang);
            return RedirectToPage("./Index");
        }
    }
}