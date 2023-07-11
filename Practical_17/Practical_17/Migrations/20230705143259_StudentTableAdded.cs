using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practical_17.Migrations
{
	/// <inheritdoc />
	public partial class StudentTableAdded : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Students",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
					Standard = table.Column<int>(type: "int", maxLength: 50, nullable: false),
					Age = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Students", x => x.Id);
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Students");
		}
	}
}
