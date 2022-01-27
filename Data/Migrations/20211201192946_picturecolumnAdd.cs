using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeInformationSystem.Data.Migrations
{
    public partial class picturecolumnAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeImage",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeImage",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
