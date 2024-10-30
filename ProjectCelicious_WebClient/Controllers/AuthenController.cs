// Controllers/AccountController.cs
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ProjectCelicious_WebClient.Models;
using ProjectCelicious_WebClient.DTOs;
using Newtonsoft.Json;

namespace ProjectCelicious_WebClient.Controllers
{
    public class AuthenController : Controller
    {
        private readonly HttpClient _httpClient;

        public AuthenController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(loginRequest);
            }
            Console.WriteLine(JsonConvert.SerializeObject(loginRequest));
            var response = await _httpClient.PostAsJsonAsync(AppUrl.BaseUrl+"/Accounts/login", loginRequest);
            Console.WriteLine("XONG R");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TokenResponse>(); // Tạo class TokenResponse
                var token = result.Token;
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {errorContent}");

                if (loginRequest.RememberMe)
                {
                    // Lưu token vào cookie
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = DateTimeOffset.UtcNow.AddHours(1) // Hoặc thời gian bạn muốn
                    };
                    Response.Cookies.Append("jwt", token, cookieOptions);
                }
                else
                {
                    Console.WriteLine("Looi R");
                    // Lưu token vào session
                    HttpContext.Session.SetString("jwt", token);
                }
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(loginRequest);
        }

        public IActionResult Logout()
        {
            // Xóa token khỏi cookie và session
            Response.Cookies.Delete("jwt");
            HttpContext.Session.Remove("jwt");

            return RedirectToAction("Login");
        }
    }
}
