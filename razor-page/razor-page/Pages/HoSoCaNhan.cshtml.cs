using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_page.Models;
using razor_page.Services;
using System.IO;

namespace razor_page.Pages
{
    public class HoSoCaNhanModel : PageModel
    {
        private readonly ServiceNguoiDung _serviceNguoiDung;
        private readonly IWebHostEnvironment _env;

        public HoSoCaNhanModel(IWebHostEnvironment env)
        {
            _serviceNguoiDung = new ServiceNguoiDung(); // Khởi tạo Service của bạn
            _env = env; // Dùng để lấy đường dẫn lưu file ảnh
        }

        // Dữ liệu dùng để hiển thị ra View
        public NguoiDung ThongTinNguoiDung { get; set; } = default!;

        // Các trường cho phép người dùng nhập/sửa
        [BindProperty]
        public string TenNguoiDung { get; set; } = "";

        [BindProperty]
        public string? MatKhauMoi { get; set; }

        [BindProperty]
        public IFormFile? AvatarUpload { get; set; } // Dùng để nhận file ảnh upload

        public string ThongBao { get; set; } = "";

        public IActionResult OnGet()
        {
            // Lấy ID người dùng từ Session (giả sử lúc đăng nhập bạn lưu "UserId" vào Session)
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/DangNhap"); // Chưa đăng nhập thì đẩy về trang đăng nhập
            }

            // Dùng hàm GetById trong ServiceNguoiDung.cs của bạn
            var user = _serviceNguoiDung.GetById(userId.Value);
            if (user == null) return RedirectToPage("/DangNhap");

            ThongTinNguoiDung = user;
            TenNguoiDung = user.TenNguoiDung; // Gán sẵn tên để hiện lên form

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToPage("/DangNhap");

            var user = _serviceNguoiDung.GetById(userId.Value);
            if (user == null) return RedirectToPage("/DangNhap");

            // 1. Cập nhật Tên người dùng
            user.TenNguoiDung = TenNguoiDung;

            // 2. Cập nhật Mật khẩu (nếu người dùng có nhập mật khẩu mới)
            if (!string.IsNullOrEmpty(MatKhauMoi))
            {
                user.MatKhau = MatKhauMoi;
            }

            // 3. Xử lý lưu ảnh Avatar (nếu có chọn file)
            if (AvatarUpload != null && AvatarUpload.Length > 0)
            {
                // Tạo thư mục "uploads" trong wwwroot nếu chưa có
                var folderPath = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Tạo tên file ngẫu nhiên (chống trùng lặp) + đuôi mở rộng của file
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(AvatarUpload.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                // Lưu file vào server
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await AvatarUpload.CopyToAsync(fileStream);
                }

                // Cập nhật LinkAvatar vào database
                user.LinkAvatar = "/uploads/" + fileName;
            }

            // Gọi hàm Edit() trong ServiceNguoiDung của bạn để lưu toàn bộ vào DB
            _serviceNguoiDung.Edit(user);

            // Cập nhật lại giao diện và thông báo thành công
            ThongBao = "Cập nhật hồ sơ cá nhân thành công!";
            ThongTinNguoiDung = user;

            return Page();
        }
    }
}