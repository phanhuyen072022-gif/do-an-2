using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_page.Models;
using razor_page.Services;

namespace razor_page.Pages
{
    public class SanPhamModel : PageModel
    {
        private readonly ServiceHoa _serviceHoa;

        public SanPhamModel(ServiceHoa serviceHoa)
        {
            _serviceHoa = serviceHoa;
        }

        public List<Hoa> DanhSachHoa { get; set; } = new List<Hoa>();

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MinPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MaxPrice { get; set; }

        public void OnGet()
        {
            // Lấy danh sách từ service
            var query = _serviceHoa.GetAll().AsEnumerable();

            // 1. Lọc theo tên (Search)
            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                query = query.Where(h => h.TenHoa.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));
            }

            // 2. Lọc theo giá (Filter)
            if (MinPrice.HasValue)
            {
                query = query.Where(h => h.GiaBan >= MinPrice.Value);
            }
            if (MaxPrice.HasValue)
            {
                query = query.Where(h => h.GiaBan <= MaxPrice.Value);
            }

            // 3. Sắp xếp (Sort)
            switch (SortOrder)
            {
                case "price_asc":
                    query = query.OrderBy(h => h.GiaBan);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(h => h.GiaBan);
                    break;
                case "name_asc":
                    query = query.OrderBy(h => h.TenHoa);
                    break;
                case "name_desc":
                    query = query.OrderByDescending(h => h.TenHoa);
                    break;
                default:
                    // Mặc định hiển thị hoa mới nhất (đảo ngược ID)
                    query = query.OrderByDescending(h => h.HoaId);
                    break;
            }

            DanhSachHoa = query.ToList();
        }
    }
}