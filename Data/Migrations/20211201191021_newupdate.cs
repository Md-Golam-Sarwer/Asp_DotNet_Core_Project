using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeInformationSystem.Data.Migrations
{
    public partial class newupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchVMBranchID",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchVMBranchID",
                table: "Departments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BranchVM",
                columns: table => new
                {
                    BranchID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchName = table.Column<string>(nullable: true),
                    BranchLocation = table.Column<string>(nullable: true),
                    Division = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchVM", x => x.BranchID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BranchVMBranchID",
                table: "Employees",
                column: "BranchVMBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_BranchVMBranchID",
                table: "Departments",
                column: "BranchVMBranchID");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_BranchVM_BranchVMBranchID",
                table: "Departments",
                column: "BranchVMBranchID",
                principalTable: "BranchVM",
                principalColumn: "BranchID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_BranchVM_BranchVMBranchID",
                table: "Employees",
                column: "BranchVMBranchID",
                principalTable: "BranchVM",
                principalColumn: "BranchID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_BranchVM_BranchVMBranchID",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BranchVM_BranchVMBranchID",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "BranchVM");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BranchVMBranchID",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Departments_BranchVMBranchID",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "BranchVMBranchID",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BranchVMBranchID",
                table: "Departments");
        }
    }
}
