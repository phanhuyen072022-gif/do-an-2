using razor_page.Models;
using Microsoft.EntityFrameworkCore;

namespace razor_page.Services
{
    public class ServiceHoa
    {
        private readonly AppDbContext _context;

        public ServiceHoa(AppDbContext context)
        {
            _context = context;
        }

        // 1. Lấy danh sách tất cả hoa
        public List<Hoa> GetAll()
        {
            return _context.Hoas.ToList(); // Giả sử trong AppDbContext bạn đã có DbSet<Hoa> Hoas
        }

        // 2. Lấy hoa theo ID
        public Hoa? GetById(int id)
        {
            return _context.Hoas.FirstOrDefault(h => h.HoaId == id);
        }

        // 3. Tạo mới hoa
        public void Create(Hoa hoa)
        {
            _context.Hoas.Add(hoa);
            _context.SaveChanges();
        }

        // 4. Chỉnh sửa hoa
        public void Update(Hoa hoa)
        {
            var existingHoa = _context.Hoas.FirstOrDefault(h => h.HoaId == hoa.HoaId);
            if (existingHoa != null)
            {
                existingHoa.TenHoa = hoa.TenHoa;
                existingHoa.GiaBan = hoa.GiaBan;
                existingHoa.MoTa = hoa.MoTa;
                existingHoa.LinkHinhAnh = hoa.LinkHinhAnh;

                _context.SaveChanges();
            }
        }

        // 5. Xóa hoa
        public void Delete(int id)
        {
            var hoa = _context.Hoas.FirstOrDefault(h => h.HoaId == id);
            if (hoa != null)
            {
                _context.Hoas.Remove(hoa);
                _context.SaveChanges();
            }
        }
    }
}