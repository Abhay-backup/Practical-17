using Microsoft.EntityFrameworkCore;
using Practical_17.Models;
using Practical_17.SeedingData;
using Practical_17.ViewDataModel;

namespace Practical_17.Data
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
		{

		}

		public DbSet<User> Users { get; set; }

		public DbSet<Role> Roles { get; set; }

		public DbSet<UserRole> UserRoles { get; set; }

		public DbSet<Student> Students { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserRole>()
				.HasKey(x => new { x.UserId, x.RoleId });

			modelBuilder.Entity<UserRole>()
				.HasOne(x => x.Role)
				.WithMany(x => x.UserRoles)
				.HasForeignKey(x => x.RoleId);

			modelBuilder.Entity<UserRole>()
				.HasOne(x => x.User)
				.WithMany(x => x.UserRoles)
				.HasForeignKey(x => x.UserId);

			modelBuilder.SeedRole();
			modelBuilder.SeedAdmin();
			modelBuilder.SeedUserRole();

		}

		public DbSet<Practical_17.ViewDataModel.RegisterViewModel> RegisterViewModel { get; set; } = default!;

		public DbSet<Practical_17.ViewDataModel.LoginViewModel> LoginViewModel { get; set; } = default!;

	}
}
