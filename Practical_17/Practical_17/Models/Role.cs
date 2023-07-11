using System.ComponentModel.DataAnnotations;

namespace Practical_17.Models
{

	public class Role
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string RoleName { get; set; }

		public ICollection<UserRole> UserRoles { get; set; }

	}
}
