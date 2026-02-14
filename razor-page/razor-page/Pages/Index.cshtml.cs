using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_page.Models;
using razor_page.Services;

namespace razor_page.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ServiceHoa _serviceHoa;

        // Danh sách hoa để truyền ra ngoài View
        public List<Hoa> DanhSachHoaNoiBat { get; set; } = new List<Hoa>();

        public IndexModel(ILogger<IndexModel> logger, ServiceHoa serviceHoa)
        {
            _logger = logger;
            _serviceHoa = serviceHoa;
        }

        public void OnGet()
        {
            // Lấy 5 sản phẩm đầu tiên hoặc mới nhất từ CSDL cho phần nổi bật
            DanhSachHoaNoiBat = _serviceHoa.GetAll().Take(5).ToList();
        }
    }
}