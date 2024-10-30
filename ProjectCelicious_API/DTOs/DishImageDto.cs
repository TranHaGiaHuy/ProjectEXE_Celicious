namespace ProjectCelicious_API.DTOs
{

    public class DishImageDto
    {
        public int DishImageID { get; set; }
        public string ImagePath { get; set; } = null!;
        public int DishId { get; set; }

    }

}
