using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BusinessObjects.Models
{
    public class DishCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DishCategoryId { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>();

    }
}
