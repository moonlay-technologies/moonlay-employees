using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Moonlay.Baas.Employees.Migrations
{
    public partial class AddEmployeeLeaveTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeLeave",
                columns: table => new
                {
                    Identity = table.Column<Guid>(nullable: false),
                    LeaveType = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Purpose = table.Column<string>(nullable: false),
                    Delegation = table.Column<string>(nullable: true),
                    Duration = table.Column<double>(nullable: false),
                    Remaining = table.Column<double>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeave", x => x.Identity);
                    table.ForeignKey(
                        name: "FK_EmployeeLeave_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Identity",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeave_EmployeeId",
                table: "EmployeeLeave",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeLeave");
        }
    }
}