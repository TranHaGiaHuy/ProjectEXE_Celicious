using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string? FullName { get; set; } = null!;

        public int? Phone { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? Gender { get; set; } = null!;

        public string? Avatar { get; set; }

        public string? Address { get; set; }

        public virtual Account Account { get; set; } = null!;

        [ForeignKey("Restaurant")]
        public int? RestaurantId { get; set; }
        public virtual Restaurant? Restaurant { get; set; }
    }
}
