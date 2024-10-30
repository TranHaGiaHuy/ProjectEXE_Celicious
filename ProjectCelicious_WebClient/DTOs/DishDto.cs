namespace ProjectCelicious_WebClient.DTOs
{
    public class DishDto
    {
        public int DishId { get; set; }
        public string DishName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public string? LinkToShoppe { get; set; }
        public string? Image { get; set; }
        public int DishCategoryId { get; set; }
        public string DishCategoryName { get; set; } = null!; // New property
        public int RestaurantId { get; set; }
    }
}
