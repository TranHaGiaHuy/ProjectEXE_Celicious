using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public enum RestaurentStatus
    {
        Active = 0,
        Inactive = 1,
        Closed = 2
    }
    public class Restaurant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RestaurantId { get; set; }

        public string RestaurantName { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Description { get; set; } = null!;

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public string Logo { get; set; } = null!;

        public string Background { get; set; } = null!;

        public RestaurentStatus Status { get; set; }

        [ForeignKey("RestaurantCategory")]

        public int? RestaurantCategoryId { get; set; }

        [ForeignKey("RestaurantAddress")]

        public int RestaurantAddressId { get; set; }
        public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>();

        public virtual ICollection<Review> Review { get; set; } = new List<Review>();
        public virtual ICollection<RestaurantImage> RestaurantImages { get; set; } = new List<RestaurantImage>();

        public virtual RestaurantAddress RestaurantAddress { get; set; } = null!;

        public virtual RestaurantCategory? RestaurantCategory { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual User? Owner { get; set; }
    }
}
