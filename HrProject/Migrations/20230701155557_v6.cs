using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrProject.Migrations
{
    /// <inheritdoc />
    public partial class v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeHolidays_Employees_Emp_Id",
                table: "EmployeeHolidays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeHolidays",
                table: "EmployeeHolidays");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeHolidays_Emp_Id",
                table: "EmployeeHolidays");

            migrationBuilder.DropColumn(
                name: "Emp_Id",
                table: "EmployeeHolidays");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeHolidays",
                table: "EmployeeHolidays",
                columns: new[] { "Holiday", "Genral_Id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeHolidays",
                table: "EmployeeHolidays");

            migrationBuilder.AddColumn<int>(
                name: "Emp_Id",
                table: "EmployeeHolidays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeHolidays",
                table: "EmployeeHolidays",
                columns: new[] { "Holiday", "Emp_Id" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHolidays_Emp_Id",
                table: "EmployeeHolidays",
                column: "Emp_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeHolidays_Employees_Emp_Id",
                table: "EmployeeHolidays",
                column: "Emp_Id",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
