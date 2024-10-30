
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace BusinessObjects.Models
{
    public class Dish
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DishId { get; set; }

        public string DishName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public double Price { get; set; }

        public string? LinkToShoppe { get; set; } = null!;

        public string? Image { get; set; } = null!;

        public virtual ICollection<DishImage>? DishImages { get; set; } = new List<DishImage>();
        [ForeignKey("DishCategory")]
        public int DishCategoryId { get; set; }
        public virtual DishCategory DishCategory { get; set; } = null!;
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; } = null!;

    }
}
