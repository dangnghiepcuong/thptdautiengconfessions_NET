using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using THPTDTCFS.Models;
using System.ComponentModel.DataAnnotations;

namespace THPTDTCFS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CheckLogin(Account acc)
        {
            if (acc.Username == null)
            {
                ViewBag.Message1 = "Nhập tài khoản!";
                return View("Login");
            }

            if (acc.Password == null)
            {
                ViewBag.Message2 = "Nhập mật khẩu!";
                return View("Login");
            }

            DataContext dataContext = new DataContext();
            SqlConnection conn = new SqlConnection(dataContext.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Password from ACCOUNT where Username = @username", conn);
            cmd.Parameters.AddWithValue("username", acc.Username);
            var reader = cmd.ExecuteReader();

            if (reader.HasRows == false)
            {
                ViewBag.Message1 = "Tài khoản không tồn tại";
                return View("Login");
            }

            reader.Read();
            if (acc.Password != reader["Password"] as string)
            {
                ViewBag.Message2 = "Mật khẩu không chính xác!";
            }
            else
            {
                Response.Redirect("/Home");
                return View("Index");
            }

            return View("Login");
        }

        public IActionResult ForgotPassword() { return View(); }

        public string SendEmail(string SenderName, string ReceiverMail, string ReceiverName, string subject, string content)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("20520418@gm.uit.edu.vn", SenderName);
                    var receiverEmail = new MailAddress(ReceiverMail, ReceiverName);
                    var password = "Cuong214789";
                    var sub = subject;
                    var body = content;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = true,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                }
            }
            catch (Exception)
            {
                return "Error in sending verification email!";
            }

            return "";
        }

        public IActionResult ResetPassword(string Username) {

            DataContext dataContext = new DataContext();
            SqlConnection conn = new SqlConnection(dataContext.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Email from ACCOUNT A, ACCOUNT_INFO A_I " +
                "where A.Username = @username and A.Id = A_I.AccountId", conn);
            cmd.Parameters.AddWithValue("username", Username);
            var reader = cmd.ExecuteReader();

            if (reader.HasRows == false)
            {
                ViewBag.Message1 = "Tài khoản chưa được liên kết với email!";
                return View("ForgotPassword");
            }

            reader.Read();
            string ReceiveMail = reader["Email"] as string;

            string new_password = "";
            Random rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                int unicode = rnd.Next(97, 122);
                char character = (char)unicode;
                new_password += character.ToString();
            }
            for (int i = 0; i < 6; i++)
            {
                int num = rnd.Next(0, 9);
                new_password += num.ToString();
            }
            for (int i = 0; i < 3; i++)
            {
                int unicode = rnd.Next(65, 90);
                char character = (char)unicode;
                new_password += character.ToString();
            }

            string message = SendEmail("THPT Dầu Tiếng Confessions", ReceiveMail, Username, "CẤP LẠI MẬT KHẨU", new_password);
            if (message != "")
            {
                ViewBag.Message1 = message;
                return View("ForgotPassword");
            }
            
            int indexOfAddr = ReceiveMail.IndexOf("@");
            string HiddenMail = ReceiveMail.Substring(0, 2) + "*" + ReceiveMail.Substring(indexOfAddr);

            ViewBag.Message1 = "Mật khẩu mới đã được gửi đến email " + HiddenMail + " liên kết của tài khoản!";
            return View("ForgotPassword");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
