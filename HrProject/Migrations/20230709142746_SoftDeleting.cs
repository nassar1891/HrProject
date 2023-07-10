using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrProject.Migrations
{
    /// <inheritdoc />
    public partial class SoftDeleting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelelted",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelelted",
                table: "Employees");
        }
    }
}
