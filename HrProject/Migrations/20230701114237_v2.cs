using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrProject.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Genral_Id",
                table: "EmployeeHolidays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GeneralSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValueExtra = table.Column<int>(type: "int", nullable: false),
                    ValueDiscount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralSettings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHolidays_Genral_Id",
                table: "EmployeeHolidays",
                column: "Genral_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeHolidays_GeneralSettings_Genral_Id",
                table: "EmployeeHolidays",
                column: "Genral_Id",
                principalTable: "GeneralSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeHolidays_GeneralSettings_Genral_Id",
                table: "EmployeeHolidays");

            migrationBuilder.DropTable(
                name: "GeneralSettings");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeHolidays_Genral_Id",
                table: "EmployeeHolidays");

            migrationBuilder.DropColumn(
                name: "Genral_Id",
                table: "EmployeeHolidays");
        }
    }
}
