using System.ComponentModel.DataAnnotations;

namespace Practical_17.ViewDataModel
{
	public class LoginViewModel
	{
		[Key]
		[Required]
		[DataType(DataType.EmailAddress)]
		public required string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public required string Password { get; set; }
	}
}
