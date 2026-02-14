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

            // Tính số tiền CÒN LẠI thay vì lấy nguyên giá trị ban đầu
            decimal tongDaThanhToan = DonHang.ThanhToans.Sum(t => t.TienDaThanhToan);
            decimal soTienConLai = DonHang.GiaDonHang - tongDaThanhToan;

            // Gợi ý số tiền cần thanh toán là phần còn lại
            SoTienThanhToan = soTienConLai > 0 ? soTienConLai : 0;

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var result = _service.ThanhToanDonHang(id, SoTienThanhToan, TrangThai);

            // Nếu kiểm tra logic thấy lỗi (thanh toán lố tiền)
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message); // Đẩy lỗi ra màn hình

                // Phải load lại DonHang để hiển thị lên View
                DonHang = _service.GetById(id);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}