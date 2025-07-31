using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Money_Management.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Net.Mail;
using System.Net;

namespace Money_Management.Controllers
{
    public class AccountController : Controller
    {
        private readonly MoneyManagementContext _context;

        public AccountController(MoneyManagementContext context)
        {
            _context = context;
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (user != null && user.Kichhoat == true && VerifyPassword(password, user.Password))
                {
                    // Update last login
                    user.Lastlogin = DateTime.Now;
                    await _context.SaveChangesAsync();

                    // Set authentication cookie
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, user.Hoten),
                        new Claim(ClaimTypes.NameIdentifier, user.Iduser.ToString())
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Email hoặc mật khẩu không hợp lệ.");
            }
            return View();
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string hoten, string email, string password, string confirmPassword, string sdt)
        {
            if (ModelState.IsValid)
            {
                if (password != confirmPassword)
                {
                    ModelState.AddModelError("", "Mật khẩu xác nhận không khớp.");
                    return View();
                }

                if (await _context.Users.AnyAsync(u => u.Email == email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại.");
                    return View();
                }

                // Generate random 5-character code
                string randomCode = GenerateRandomCode(5);

                // Hash password
                string hashedPassword = HashPassword(password);

                var user = new User
                {
                    Hoten = hoten,
                    Email = email,
                    Password = hashedPassword,
                    Sdt = sdt,
                    Random = randomCode,
                    Kichhoat = false,
                    Lastlogin = DateTime.Now
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Send verification email
                await SendVerificationEmail(email, randomCode);

                return RedirectToAction("VerifyEmail", new { email });
            }
            return View();
        }

        // GET: Account/VerifyEmail
        public IActionResult VerifyEmail(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        // POST: Account/VerifyEmail
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyEmail(string email, string code)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null && user.Random == code)
            {
                user.Kichhoat = true;
                user.Random = null; // Clear random code after verification
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "Mã xác nhận không hợp lệ.");
            ViewBag.Email = email;
            return View();
        }

        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            var inputHash = HashPassword(password);
            return inputHash == hashedPassword;
        }

        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task SendVerificationEmail(string email, string code)
        {
            var fromAddress = new MailAddress("your-email@example.com", "Money Management");
            var toAddress = new MailAddress(email);
            const string subject = "Xác Nhận Địa Chỉ Email";
            string body = $"Vui lòng xác nhận email của bạn bằng cách nhập mã này: {code}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("your-email@example.com", "your-app-password")
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                await smtp.SendMailAsync(message);
            }
        }
    }
}