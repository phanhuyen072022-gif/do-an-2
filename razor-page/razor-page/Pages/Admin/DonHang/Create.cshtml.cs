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
            NguoiDungList = new SelectList(_service.GetListKhachHang(), "NguoiDungId", "TenNguoiDung");
            HoaList = new SelectList(_service.GetListHoa(), "HoaId", "TenHoa");
        }

        public IActionResult OnPost()
        {
            // Bỏ qua lỗi validate các thuộc tính Navigation (vì form chỉ gửi lên Id)
            ModelState.Remove("DonHang.Hoa");
            ModelState.Remove("DonHang.NguoiDung");
            ModelState.Remove("DonHang.ThanhToans");

            if (!ModelState.IsValid)
            {
                // Phải load lại dropdown list nếu có lỗi, tránh bị crash view
                NguoiDungList = new SelectList(_service.GetListNguoiDung(), "NguoiDungId", "TenNguoiDung");
                HoaList = new SelectList(_service.GetListHoa(), "HoaId", "TenHoa");
                return Page();
            }

            // Tự động tính tổng giá đơn hàng = Giá bán của hoa * Số lượng
            var hoa = _service.GetListHoa().FirstOrDefault(h => h.HoaId == DonHang.HoaId);
            if (hoa != null)
            {
                DonHang.GiaDonHang = hoa.GiaBan * DonHang.SoLuong;
            }

            _service.Create(DonHang);
            return RedirectToPage("./Index");
        }
    }
}