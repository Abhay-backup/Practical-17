using System.ComponentModel.DataAnnotations;

namespace Practical_17.Models
{

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public required string LastName { get; set; }


        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [Required]
        //[StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(\\+\\d{1,2}\\s?)?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}$", ErrorMessage = "Phone Must be Number")]
        public required string MobileNumber { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
