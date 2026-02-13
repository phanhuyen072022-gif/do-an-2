using Microsoft.EntityFrameworkCore;
using razor_page.Models;
using razor_page.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Thêm dòng này để đăng ký dịch vụ Session
builder.Services.AddSession();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ServiceHoa>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapRazorPages();

app.UseSession();


app.Run();
