using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_page.Models;
using razor_page.Services;
using System.Collections.Generic;

namespace razor_page.Pages.Admin.NguoiDung
{
    public class IndexModel : PageModel
    {
        private readonly ServiceNguoiDung _serviceNguoiDung;

        // Constructor để inject ServiceNguoiDung
        public IndexModel(ServiceNguoiDung serviceNguoiDung)
        {
            _serviceNguoiDung = serviceNguoiDung;
        }

        // Property để chứa danh sách người dùng hiển thị ra view
        public List<razor_page.Models.NguoiDung> NguoiDungs { get; set; } = new List<razor_page.Models.NguoiDung>();

        public void OnGet()
        {
            // Lấy toàn bộ danh sách người dùng từ Service
            NguoiDungs = _serviceNguoiDung.GetAll();
        }
    }
}