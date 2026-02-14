using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_page.Models;
using razor_page.Services;

namespace razor_page.Pages.Admin.DonHang
{
    public class ThanhToanModel : PageModel
    {
        private readonly ServiceDonHang _service;
        public ThanhToanModel(ServiceDonHang service) { _service = service; }

        public razor_page.Models.DonHang DonHang { get; set; }

        [BindProperty]
        public decimal SoTienThanhToan { get; set; }
        [BindProperty]
        public string TrangThai { get; set; } = "Đã thanh toán";

        public IActionResult OnGet(int? id)
        {
            if (id == null) return NotFound();
            DonHang = _service.GetById(id.Value);
            if (DonHang == null) return NotFound();

            // Mặc định số tiền thanh toán bằng giá trị đơn hàng
            SoTienThanhToan = DonHang.GiaDonHang;
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _service.ThanhToanDonHang(id, SoTienThanhToan, TrangThai);
            return RedirectToPage("./Index");
        }
    }
}