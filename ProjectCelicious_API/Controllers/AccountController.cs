using Azure;
using BusinessObjects.DataContext;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity; // Thêm namespace này
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectCelicious_API.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly CeliciousContext _context;
    private readonly IConfiguration _configuration;
    private readonly PasswordHasher<Account> _passwordHasher;

    public AccountsController(CeliciousContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
        _passwordHasher = new PasswordHasher<Account>();
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Kiểm tra xem username đã tồn tại chưa
        var existingAccount = await _context.Accounts
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.Email == registerDto.Email);

        if (existingAccount != null)
        {
            return Conflict("Emmail already exists.");
        }

        // Kiểm tra mật khẩu
        if (registerDto.Password.Length < 6)
        {
            return BadRequest("Password must be at least 6 characters long.");
        }

        // Kiểm tra thông tin người dùng
        if (string.IsNullOrEmpty(registerDto.Email))
        {
            return BadRequest("Full name is required.");
        }

        // Tạo User
        var user = new User
        {
            CreateDate = DateTime.UtcNow
        };

        // Tạo Account
        var account = new Account
        {
            Email = registerDto.Email,
            Role = Role.User, 
            Status = Status.Active,
            User = user
        };

        account.Password = _passwordHasher.HashPassword(account, registerDto.Password);

        // Lưu vào cơ sở dữ liệu
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        return Ok("Account created successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == request.Emmail);
        var passwordHasher = new PasswordHasher<Account>();
        var hashedPassword = passwordHasher.HashPassword(account, "user123");

        if (account == null)
        {
            return Unauthorized("Invalid username or password");
        }
        if (account.Status == Status.Ban)
        {
            return Unauthorized("You are banned from this website!"); // 403 Forbidden
        }
        if (account.Status == Status.Inactive)
        {
            return Unauthorized("Your account is  inactivated!"); // 403 Forbidden
        }
        var result = passwordHasher.VerifyHashedPassword(account, account.Password, request.Password);

        if (result != PasswordVerificationResult.Success)
        {
            return Unauthorized("Invalid username or password");
        }
        Console.WriteLine("THANH CONG");
        // Tạo JWT token và trả về
        var token = GenerateJwtToken(account);
        return Ok("Login thanh cong");
    }

    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        return Ok("Logged out successfully");
    }
    [HttpGet("test")]
    [Authorize]
    public IActionResult DecodeJwt()
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (string.IsNullOrEmpty(token))
        {
            return BadRequest("Token is required");
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        // Lấy các claim từ token
        var usernameClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "Email")?.Value;

        if (usernameClaim != null)
        {
            // Sử dụng giá trị usernameClaim
            Console.WriteLine($"Email: {usernameClaim}");
        }
        else
        {
            // Xử lý trường hợp không tìm thấy Email
            Console.WriteLine("Email not found in token.");
        }


        return Ok(new { Username = usernameClaim });
    }
    private string GenerateJwtToken(Account account)
    {
        var claims = new[]
{
    new Claim(JwtRegisteredClaimNames.Sub, account.Email),
    new Claim(JwtRegisteredClaimNames.Jti, account.AccountId.ToString()),
    new Claim(ClaimTypes.Role, account.Role.ToString()),
    new Claim("Email", account.Email), // Claim với tên là "Email"
};

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(3),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("assignOwner/{accountId}")]
    public async Task<IActionResult> AssignOwner(int accountId)
    {
        var account = await _context.Accounts.FindAsync(accountId);
        if (account == null)
        {
            return NotFound("Account not found.");
        }

        account.Role = Role.Owner; // Cấp quyền Owner
        await _context.SaveChangesAsync();

        return Ok("Account has been assigned as Restaurant Owner.");
    }

}
