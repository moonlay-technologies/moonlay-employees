using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Moonlay.Baas.Employees.Migrations
{
    public partial class AU_EmployeeLeave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "EmployeeLeave",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_TeamMember_PositionId",
                table: "TeamMember",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMember_Position_PositionId",
                table: "TeamMember",
                column: "PositionId",
                principalTable: "Position",
                principalColumn: "Identity",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMember_Position_PositionId",
                table: "TeamMember");

            migrationBuilder.DropIndex(
                name: "IX_TeamMember_PositionId",
                table: "TeamMember");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "EmployeeLeave");
        }
    }
}
