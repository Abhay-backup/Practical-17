using System.ComponentModel.DataAnnotations;

namespace Practical_17.ViewDataModel
{
    public class RegisterViewModel
    {
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
        [RegularExpression("^(\\+\\d{1,2}\\s?)?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}$", ErrorMessage = "Phone Number is Only Contains the Number")]
        public required string MobileNumber { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Both Password Must be same")]
        public required string ConfirmPassword { get; set; }


    }
}
