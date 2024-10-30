namespace ProjectCelicious_WebClient.DTOs
{
    public class RestaurantImageDto
    {
        public int ResImageID { get; set; }
        public int RestaurantId { get; set; }
        public string ImagePath { get; set; } = null!;
    }
}
