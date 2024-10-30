namespace ProjectCelicious_API.DTOs
{
    public class LoginRequest
    {
        public string Emmail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
