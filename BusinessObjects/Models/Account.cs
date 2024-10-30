using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace BusinessObjects.Models
{
    public enum Role
    {
        Admin = 0,
        User = 1,
        Owner = 2
    }
    public enum Status
    {
        Active = 0,
        Inactive = 1,
        Ban = 2
    }
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public Role Role { get; set; }

        public Status? Status { get; set; }

        public virtual User User { get; set; } = null!;

    }
}
