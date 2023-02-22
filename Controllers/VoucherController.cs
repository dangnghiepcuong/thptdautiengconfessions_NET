using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using THPTDTCFS.Models;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace THPTDTCFS.Controllers
{
    public class VoucherController : Controller
    {
        public IActionResult Index(string Code)
        {
            if (Code != null && Code != "")
            {
                Code = Code.ToUpper();
                CheckVoucher(Code);
            }
            return View();
        }

        public IActionResult CheckVoucher(string Code)
        {
            DataContext dataContext= new DataContext();
            SqlConnection conn = new SqlConnection(dataContext.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Activation, S.Name, Description from SHOP S, VOUCHER V " +
                                            " where Code = @code and S.Id = V.ShopId", conn);
            cmd.Parameters.AddWithValue("code", Code);
            var reader = cmd.ExecuteReader();

            if (reader.HasRows == false) 
            {
                ViewBag.Message = "Không tìm thấy Voucher!";
                return View("Voucher");
                /*return Content("Không tìm thấy Voucher!", "text/html");*/
            }

            reader.Read();
            Voucher v = new Voucher();
            v.ActiveStatus = reader.GetBoolean(reader.GetOrdinal("Activation"));
            if (v.ActiveStatus == false)
            {
                ViewBag.Message = "Voucher đã hết hạn/lượt sử dụng!";
                return View("Voucher");
                /*return Content("Voucher đã hết hạn/lượt sử dụng!", "text/html");*/
            }

            /*string html = "<br>"
                            + "<h2 class='shop-name'>" + reader["Name"] as string + "</h2>"
                            + "<div class='code'>" + Code + "</div>"
                            + "<div class='image'><img src='#' alt='Hình ảnh goes here'></div>"
                            + "<div class='description'>" + reader["Description"] as string + "</div>"
                            + "<div class='cover-btn-apply'>"
                                + "<button class='btn btn-apply'>Dùng voucher</button>"
                            + "</div>";
            return Content(html, "text/html");*/
            
            ViewBag.Message = "";
            ViewBag.ShopName = reader["Name"] as string;
            ViewBag.VoucherCode = Code;
            ViewBag.Description = reader["Description"] as string;
            return View("Voucher");
        }
    }
}
