using System.Collections.Generic;
using System.Linq;
using razor_page.Models;

namespace razor_page.Services
{
    public class ServiceNguoiDung
    {
        // 1. Get All (Lấy danh sách tất cả người dùng)
        public List<NguoiDung> GetAll()
        {
            using (var context = new AppDbContext())
            {
                // Sắp xếp theo ID giảm dần để thấy người mới nhất (tùy chọn)
                return context.NguoiDungs.OrderByDescending(x => x.NguoiDungId).ToList();
            }
        }

        // 2. Get By Id (Lấy người dùng theo ID)
        public NguoiDung? GetById(int id)
        {
            using (var context = new AppDbContext())
            {
                return context.NguoiDungs.FirstOrDefault(x => x.NguoiDungId == id);
            }
        }

        // 3. Create (Thêm mới người dùng)
        public void Create(NguoiDung nguoiDung)
        {
            using (var context = new AppDbContext())
            {
                // Có thể thêm logic kiểm tra trùng Email ở đây nếu cần
                context.NguoiDungs.Add(nguoiDung);
                context.SaveChanges();
            }
        }

        // 4. Edit (Cập nhật thông tin người dùng)
        public void Edit(NguoiDung nguoiDung)
        {
            using (var context = new AppDbContext())
            {
                // Cách đơn giản nhất: Update toàn bộ object
                // Entity Framework sẽ dựa vào NguoiDungId (Khóa chính) để tìm và cập nhật
                context.NguoiDungs.Update(nguoiDung);
                context.SaveChanges();
            }
        }

        // 5. Delete (Xóa người dùng)
        public void Delete(int id)
        {
            using (var context = new AppDbContext())
            {
                var user = context.NguoiDungs.FirstOrDefault(x => x.NguoiDungId == id);
                if (user != null)
                {
                    context.NguoiDungs.Remove(user);
                    context.SaveChanges();
                }
            }
        }

        // 6. Đăng ký (Trả về true nếu thành công, false nếu email đã tồn tại)
        public bool DangKy(NguoiDung nguoiDung)
        {
            using (var context = new AppDbContext())
            {
                // Kiểm tra email đã tồn tại chưa
                var checkEmail = context.NguoiDungs.FirstOrDefault(x => x.Email == nguoiDung.Email);
                if (checkEmail != null)
                {
                    return false; // Email đã tồn tại
                }

                // Gán quyền mặc định nếu chưa có
                if (string.IsNullOrEmpty(nguoiDung.Quyen))
                {
                    nguoiDung.Quyen = "KhachHang";
                }

                // Lưu ý: Ở đây làm đơn giản nên lưu mật khẩu dạng Text. 
                // Thực tế nên mã hóa MD5 hoặc BCrypt.
                context.NguoiDungs.Add(nguoiDung);
                context.SaveChanges();
                return true;
            }
        }

        // 7. Đăng nhập (Trả về object NguoiDung nếu đúng, null nếu sai)
        public NguoiDung? DangNhap(string email, string matKhau)
        {
            using (var context = new AppDbContext())
            {
                // Tìm người dùng có email và mật khẩu khớp
                var user = context.NguoiDungs
                    .FirstOrDefault(x => x.Email == email && x.MatKhau == matKhau);

                return user;
            }
        }
    }
}