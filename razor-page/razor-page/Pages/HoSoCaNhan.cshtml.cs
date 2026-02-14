using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_page.Models;
using razor_page.Services;

namespace razor_page.Pages
{
    public class HoSoCaNhanModel : PageModel
    {
        private readonly ServiceNguoiDung _serviceNguoiDung;

        public HoSoCaNhanModel()
        {
            _serviceNguoiDung = new ServiceNguoiDung();
        }

        // Dữ liệu dùng để hiển thị ra View
        public NguoiDung ThongTinNguoiDung { get; set; } = default!;

        // Các trường cho phép người dùng nhập/sửa
        [BindProperty]
        public string TenNguoiDung { get; set; } = "";

        [BindProperty]
        public string? MatKhauCu { get; set; } // Thêm trường mật khẩu cũ

        [BindProperty]
        public string? MatKhauMoi { get; set; }

        [BindProperty]
        public string? LinkAvatarMoi { get; set; } // Thay thế IFormFile bằng chuỗi link

        public string ThongBao { get; set; } = "";
        public string LoiMatKhau { get; set; } = ""; // Dùng để hiện lỗi nếu sai pass cũ

        public IActionResult OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/DangNhap");
            }

            var user = _serviceNguoiDung.GetById(userId.Value);
            if (user == null) return RedirectToPage("/DangNhap");

            ThongTinNguoiDung = user;
            TenNguoiDung = user.TenNguoiDung;
            LinkAvatarMoi = user.LinkAvatar; // Gán sẵn link hiện tại lên form

            return Page();
        }

        public IActionResult OnPost()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToPage("/DangNhap");

            var user = _serviceNguoiDung.GetById(userId.Value);
            if (user == null) return RedirectToPage("/DangNhap");

            // 1. Cập nhật Tên người dùng và Cập nhật ngay lập tức Session để _Layout đổi tên
            user.TenNguoiDung = TenNguoiDung;
            HttpContext.Session.SetString("UserName", user.TenNguoiDung);

            // 2. Cập nhật Mật khẩu (có xác minh mật khẩu cũ)
            if (!string.IsNullOrEmpty(MatKhauMoi))
            {
                if (string.IsNullOrEmpty(MatKhauCu) || MatKhauCu != user.MatKhau)
                {
                    // Nếu nhập sai mật khẩu cũ
                    LoiMatKhau = "Mật khẩu cũ không chính xác!";
                    ThongTinNguoiDung = user; // Load lại dữ liệu để trang không bị lỗi
                    return Page();
                }

                // Nếu đúng mật khẩu cũ thì đổi sang mật khẩu mới
                user.MatKhau = MatKhauMoi;
            }

            // 3. Cập nhật Link Ảnh (Dùng link text thay vì upload file)
            user.LinkAvatar = LinkAvatarMoi;

            // Gọi hàm Edit() lưu toàn bộ vào DB
            _serviceNguoiDung.Edit(user);

            // Cập nhật lại giao diện và thông báo thành công
            ThongBao = "Cập nhật hồ sơ cá nhân thành công!";
            ThongTinNguoiDung = user;

            return Page();
        }
    }
}