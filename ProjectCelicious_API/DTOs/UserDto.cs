namespace ProjectCelicious_API.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public int? Phone { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Gender { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }
        public int? RestaurantId { get; set; }
    }
}
