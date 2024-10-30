namespace ProjectCelicious_WebClient.DTOs
{

    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public string Description { get; set; } = null!;
        public string? Image { get; set; } = null!;
        public float Rating { get; set; }
        public DateTime CreateTime { get; set; }

    }

}
