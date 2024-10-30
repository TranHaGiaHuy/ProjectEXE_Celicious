using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class RestaurantImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResImageID { get; set; }

        [ForeignKey("Restaurant")]

        public int RestaurantId { get; set; }

        public string ImagePath { get; set; } = null!;

        public virtual Restaurant Restaurant { get; set; }
    }
}
