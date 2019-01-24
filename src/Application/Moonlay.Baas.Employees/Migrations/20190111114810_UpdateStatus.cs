using Microsoft.EntityFrameworkCore.Migrations;

namespace Moonlay.Baas.Employees.Migrations
{
    public partial class UpdateStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "EmployeeLeave",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "EmployeeLeave",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
