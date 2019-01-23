using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Moonlay.Baas.Employees.Migrations
{
    public partial class ConfigOnetoManyEmployeeAbsence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Identity = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false),
                    RegisDate = table.Column<DateTime>(nullable: false),
                    ResignDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Identity);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAbsence",
                columns: table => new
                {
                    Identity = table.Column<Guid>(nullable: false),
                    CheckInDate = table.Column<DateTime>(nullable: false),
                    LocationCheckIn = table.Column<int>(nullable: false),
                    CheckOutDate = table.Column<DateTime>(nullable: true),
                    Duration = table.Column<TimeSpan>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAbsence", x => x.Identity);
                    table.ForeignKey(
                        name: "FK_EmployeeAbsence_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Identity",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAbsence_EmployeeId",
                table: "EmployeeAbsence",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeAbsence");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}