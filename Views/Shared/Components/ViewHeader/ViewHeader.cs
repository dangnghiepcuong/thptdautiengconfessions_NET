using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyViewComponent
{

    // Tên lớp ViewProductViewComponent thì không cần thuộc tính [ViewComponent]
    [ViewComponent]
    public class ViewHeader : ViewComponent
    {
        // product là sản phẩm hiện thị, dùng dynamic cho nhanh ở ví dụ này,
        // thống nhất nó có cấu trúc gồm các thuộc tính: Name, Description, Price
        // Nếu khởi tạo có tham số, thì nó là dịch vụ cần được Inject và
        // Dịch vụ Inject vào cũng cần đăng ký ở ConfigureServices trong Startup
        public ViewHeader()
        {
        }
        // Dùng async Task<IViewComponentResult> InvokeAsync
        // nếu dùng kỹ thuật bất đồng bộ
        public IViewComponentResult Invoke()
        {
            return View(); // Nếu khác Default.cshtml thì trả về View("abc", product) cho abc.cshtml
        }
    }
}